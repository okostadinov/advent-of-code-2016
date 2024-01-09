using System.Linq;

const string input = "L5, R1, L5, L1, R5, R1, R1, L4, L1, L3, R2, R4, L4, L1, L1, R2, R4, R3, L1, R4, L4, L5, L4, R4, L5, R1, R5, L2, R1, R3, L2, L4, L4, R1, L192, R5, R1, R4, L5, L4, R5, L1, L1, R48, R5, R5, L2, R4, R4, R1, R3, L1, L4, L5, R1, L4, L2, L5, R5, L2, R74, R4, L1, R188, R5, L4, L2, R5, R2, L4, R4, R3, R3, R2, R1, L3, L2, L5, L5, L2, L1, R1, R5, R4, L3, R5, L1, L3, R4, L1, L3, L2, R1, R3, R2, R5, L3, L1, L1, R5, L4, L5, R5, R2, L5, R2, L1, L5, L3, L5, L5, L1, R1, L4, L3, L1, R2, R5, L1, L3, R4, R5, L4, L1, R5, L1, R5, R5, R5, R2, R1, R2, L5, L5, L5, R4, L5, L4, L4, R5, L2, R1, R5, L1, L5, R4, L3, R4, L2, R3, R3, R3, L2, L2, L2, L1, L4, R3, L4, L2, R2, R5, L1, R2";

char facingDirection = 'N';
int[] currentPosition = [0, 0];
List<int[]> visitedLocations = new List<int[]>();

string[] directions = input.Split(", ");
foreach (string direction in directions)
{
    char turn = direction[0];
    UpdateFacingDirection(turn);
    
    int.TryParse(direction.Remove(0, 1), out int moves);

    if (args.Length == 0 || args[0] == "-first")
    {
        CalculateMoves(moves);
    }
    else if (args[0] == "-second")
    {
        if (TrackVisitedLocations(moves))
            break;
    }
}

int distance = Math.Abs(currentPosition[0]) + Math.Abs(currentPosition[1]);
System.Console.WriteLine(distance);

void UpdateFacingDirection(char turn)
{
    switch (facingDirection)
    {
        case 'N':
            facingDirection = turn == 'L' ? 'W' : 'E';
            break;
        case 'E':
            facingDirection = turn == 'L' ? 'N' : 'S';
            break;
        case 'S':
            facingDirection = turn == 'L' ? 'E' : 'W';
            break;
        case 'W':
            facingDirection = turn == 'L' ? 'S' : 'N';
            break;
    }
}

void CalculateMoves(int moves)
{
    switch (facingDirection)
    {
        case 'N':
            currentPosition[0] += moves;
            break;
        case 'E':
            currentPosition[1] += moves;
            break;
        case 'S':
            currentPosition[0] -= moves;
            break;
        case 'W':
            currentPosition[1] -= moves;
            break;
    }
}

bool TrackVisitedLocations(int moves)
{
    for (int i = 0; i < moves; i++)
    {
        switch (facingDirection)
        {
            case 'N':
                currentPosition[0]++;
                if (CheckVisited())
                    return true;
                visitedLocations.Add([currentPosition[0], currentPosition[1]]);
                break;
            case 'E':
                currentPosition[1]++;
                if (CheckVisited())
                    return true;
                visitedLocations.Add([currentPosition[0], currentPosition[1]]);
                break;
            case 'S':
                currentPosition[0]--;
                if (CheckVisited())
                    return true;
                visitedLocations.Add([currentPosition[0], currentPosition[1]]);
                break;
            case 'W':
                currentPosition[1]--;
                if (CheckVisited())
                    return true;
                visitedLocations.Add([currentPosition[0], currentPosition[1]]);
                break;
        }
    }
    return false;
}

bool CheckVisited()
{
    return visitedLocations.Any(loc => loc.SequenceEqual(currentPosition));
}