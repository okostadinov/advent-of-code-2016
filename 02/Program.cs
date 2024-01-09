string? line;
int[,] keypad1 = new int[3, 3] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } };
char[,] keypad2 = new char[5, 5] { { ' ', ' ', '1', ' ', ' ' }, { ' ', '2', '3', '4', ' ' }, { '5', '6', '7', '8', '9' }, { ' ', 'A', 'B', 'C', ' ' }, { ' ', ' ', 'D', ' ', ' ' } };
int[] currentPosition = [1, 1];
string passcode = "";

try
{
    StreamReader sr = new StreamReader("./input.txt");
    line = sr.ReadLine();

    if (args.Length > 0 && args[0] == "-second")
        currentPosition = [2, 0]; // start at 5 of keypad2

    while (line != null)
    {
        if (args.Length == 0 || args[0] == "-first")
        {
            ReadInstructionsLine(line);
            passcode += keypad1[currentPosition[0], currentPosition[1]];
        }
        else if (args[0] == "-second")
        {
            ReadInstructionsLineSecond(line);
            if (keypad2[currentPosition[0], currentPosition[1]] != ' ')
                passcode += keypad2[currentPosition[0], currentPosition[1]];
        }
        line = sr.ReadLine();
    }

    sr.Close();
    System.Console.WriteLine(passcode);
}
catch (Exception e)
{
    System.Console.WriteLine("Exception: " + e.Message);
}

void ReadInstructionsLine(string line)
{
    foreach (char letter in line)
    {
        switch (letter)
        {
            case 'U':
                if (currentPosition[0] > 0)
                    currentPosition[0]--;
                break;
            case 'D':
                if (currentPosition[0] < 2)
                    currentPosition[0]++;
                break;
            case 'L':
                if (currentPosition[1] > 0)
                    currentPosition[1]--;
                break;
            case 'R':
                if (currentPosition[1] < 2)
                    currentPosition[1]++;
                break;
        }
    }
}

void ReadInstructionsLineSecond(string line)
{
    foreach (char letter in line)
    {
        switch (letter)
        {
            case 'U':
                if (currentPosition[0] > 0 && keypad2[currentPosition[0] - 1, currentPosition[1]] != ' ')
                    currentPosition[0]--;
                break;
            case 'D':
                if (currentPosition[0] < 4 && keypad2[currentPosition[0] + 1, currentPosition[1]] != ' ')
                    currentPosition[0]++;
                break;
            case 'L':
                if (currentPosition[1] > 0 && keypad2[currentPosition[0], currentPosition[1] - 1] != ' ')
                    currentPosition[1]--;
                break;
            case 'R':
                if (currentPosition[1] < 4 && keypad2[currentPosition[0], currentPosition[1] + 1] != ' ')
                    currentPosition[1]++;
                break;
        }
    }
}