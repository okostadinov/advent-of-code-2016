using System.Linq.Expressions;

string? line;
string message = "";

Dictionary<int, Dictionary<char, int>> columnLetterTracker = [];

try
{
    StreamReader sr = new StreamReader("./input.txt");
    line = sr.ReadLine();

    while (line != null)
    {
        ProcessLine(line);
        line = sr.ReadLine();
    }

    if (args.Length == 0 || args[0] == "-first")
        message = RetrieveMessage(true);
    else if (args[0] == "-second")
        message = RetrieveMessage(false);

    System.Console.WriteLine(message);
}
catch (Exception e)
{
    System.Console.WriteLine(e.Message);
}

void ProcessLine(string line)
{
    char[] letters = line.ToCharArray();

    for (int i = 0; i < letters.Length; i++)
        UpdateTrackerColumnEntries(letters[i], i);
}

void UpdateTrackerColumnEntries(char letter, int index)
{
    if (!columnLetterTracker.ContainsKey(index))
        columnLetterTracker.Add(index, []);

    if (!columnLetterTracker[index].ContainsKey(letter))
        columnLetterTracker[index].Add(letter, 1);
    else
        columnLetterTracker[index][letter]++;
}

string RetrieveMessage(bool trackMostCommonLetters)
{
    string message = "";

    foreach (int index in columnLetterTracker.Keys)
    {
        if (trackMostCommonLetters)
            message += columnLetterTracker[index].Aggregate((x, y) => x.Value > y.Value ? x : y).Key;
        else
            message += columnLetterTracker[index].Aggregate((x, y) => x.Value < y.Value ? x : y).Key;
    }

    return message;
}