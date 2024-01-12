string? line;
int sumIDs = 0;

try
{
    StreamReader sr = new StreamReader("./input.txt");
    line = sr.ReadLine();

    while (line != null)
    {
        bool decrypt = args.Length > 0 && args[0] == "-second";
        ProcessLine(line, decrypt);
        line = sr.ReadLine();
    }

    if (args.Length == 0 || args[0] == "-first")
        System.Console.WriteLine(sumIDs);
}
catch (Exception e)
{
    System.Console.WriteLine(e.Message);
}

void ProcessLine(string line, bool decrypt)
{
    string[] elements = line.Split("-");

    string idAndChecksum = elements.Last();
    Array.Resize(ref elements, elements.Length - 1);
    string encryptedName = string.Join("", elements);

    string checksum = GetChecksum(idAndChecksum);
    int.TryParse(GetID(idAndChecksum), out int id);
    Dictionary<char, int> lettersCount = GetLettersCount(encryptedName);

    if (ValidateDoor(checksum, id, lettersCount))
    {
        sumIDs += id;

        if (decrypt)
        {
            string name = DecryptName(encryptedName, id);
            if (name.Contains("north"))
                System.Console.WriteLine(id);
        }
    }
}

Dictionary<char, int> GetLettersCount(string encryptedName) {
    var lettersCount = new Dictionary<char, int>();

    foreach (char letter in encryptedName)
    {
        if (lettersCount.ContainsKey(letter))
            lettersCount[letter]++;
        else
            lettersCount.Add(letter, 1);
    }

    return lettersCount;
}

string GetChecksum(string idAndChecksum)
{
    int checksumStart = idAndChecksum.IndexOf("[") + 1;
    int checksumLength = idAndChecksum.Length - checksumStart - 1;
    return idAndChecksum.Substring(checksumStart, checksumLength);
}

string GetID(string idAndChecksum)
{
    return idAndChecksum.Split("[")[0];
}

bool ValidateDoor(string checksum, int id, Dictionary<char, int> lettersCount)
{
    char previousLetter = '~';
    int previousLetterCount = 0;
    bool initialized = false;
    bool validDoor = true;

    foreach (char letter in checksum)
    {
        if (!lettersCount.ContainsKey(letter))
        {
            validDoor = false;
            break;
        }

        char currentLetter = letter;
        int currentLetterCount = lettersCount[currentLetter];

        if (!initialized)
            initialized = true;
        else
            validDoor = CheckConditions(currentLetter, previousLetter, currentLetterCount, previousLetterCount);

        previousLetter = currentLetter;
        previousLetterCount = currentLetterCount;

        if (!validDoor)
            break;
    }

    return validDoor;
}

bool CheckConditions(char cl, char pl, int clc, int plc)
{
    if (clc > plc)
        return false;
    else if (clc == plc && cl < pl)
        return false;

    return true;
}

string DecryptName(string encryptedName, int count)
{
    string decryptedName = "";

    foreach (char letter in encryptedName)
    {
        char decryptedLetter = letter;

        for (int i = 0; i < count; i++)
        {
            if (decryptedLetter == 122)
                decryptedLetter = (char)97;
            else
                decryptedLetter++;
        }

        decryptedName += decryptedLetter;
    }

    return decryptedName;
}