string? line;
int validTriangles = 0;

try
{
    StreamReader sr = new StreamReader("./input.txt");

    line = sr.ReadLine();

    if (args.Length == 0 || args[0] == "-first")
    {
        while (line != null)
        {
            int[] sides = GetTriangleSides(line);
            if (CheckValidTriangle(sides))
                validTriangles++;

            line = sr.ReadLine();
        }
    }
    else if (args[0] == "-second")
    {

        int lineCount = 0;
        int[,] triangles = new int[3, 3];

        while (line != null)
        {
            int[] sides = GetTriangleSides(line);

            triangles[0, lineCount] = sides[0];
            triangles[1, lineCount] = sides[1];
            triangles[2, lineCount] = sides[2];

            line = sr.ReadLine();
            lineCount++;

            if (lineCount > 2)
            {
                for (int i = 0; i < triangles.GetLength(0); i++)
                {
                    if (CheckValidTriangle([triangles[i, 0], triangles[i, 1], triangles[i, 2]]))
                        validTriangles++;
                }

                lineCount = 0;
            }
        }
    }

    System.Console.WriteLine(validTriangles);
}
catch (Exception e)
{
    System.Console.WriteLine(e.Message);
}

int[] GetTriangleSides(string line)
{
    string[] sides = line.Trim().Split(" ", StringSplitOptions.RemoveEmptyEntries);
    int[] lengths = new int[3];

    for (int i = 0; i < 3; i++)
        int.TryParse(sides[i].Trim(), out lengths[i]);

    return lengths;
}

bool CheckValidTriangle(int[] sides)
{
    return sides[0] + sides[1] > sides[2] && sides[1] + sides[2] > sides[0] && sides[0] + sides[2] > sides[1];
}