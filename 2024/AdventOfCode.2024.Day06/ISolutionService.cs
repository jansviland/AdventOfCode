namespace AdventOfCode._2024.Day06;

public interface ISolutionService
{
    public long RunPart1(string[] input);
    public long RunPart2(string[] input);
}

public class SolutionService : ISolutionService
{
    private readonly ILogger<ISolutionService> _logger;
    private readonly Helper _helper = new();

    public SolutionService(ILogger<SolutionService> logger)
    {
        _logger = logger;
    }

    public long RunPart1(string[] input)
    {
        _logger.LogInformation("Solving - {Year} - Day {Day} - Part 1", _helper.GetYear(), _helper.GetDay());
        _logger.LogInformation("Input contains {Input} values", input.Length);

        // Dimensions of the grid
        int rows = input.Length;
        int cols = input[0].Length;

        // Create a 2D grid
        char[,] grid = new char[rows, cols];

        // Populate the grid
        for (int y = 0; y < rows; y++) // Loop through rows
        {
            for (int x = 0; x < cols; x++) // Loop through columns
            {
                grid[y, x] = input[y][x];
            }
        }

        // pad the grid with #
        var paddedGrid = new char[rows + 2, cols + 2];
        for (var y = 0; y < grid.GetLength(1) + 2; y++)
        {
            for (var x = 0; x < grid.GetLength(0) + 2; x++)
            {
                if (y == 0 || y == grid.GetLength(1) + 1 || x == 0 || x == grid.GetLength(0) + 1)
                {
                    paddedGrid[y, x] = '+';
                }
                else
                {
                    paddedGrid[y, x] = grid[y - 1, x - 1];
                }
            }
        }

        printGrid(paddedGrid);

        // 1. Apply padding with #
        // 2. Find the starting point
        // 3. start moving up until we hit a wall
        // 4. turn right
        // 5. repeat until we can not move anymore
        // 6. print updated grid for each step, X for visited cells, # for walls, ^ for starting point


        var start = Find(paddedGrid, '^');

        // var current = grid[start.Item1, start.Item2];
        var steps = 0;
        var visited = 1;

        var up = new int[] { -1, 0 };
        var down = new int[] { 1, 0 };
        var left = new int[] { 0, -1 };
        var right = new int[] { 0, 1 };

        var direction = up;
        var printCar = '|';

        var currentPos = new int[] { start[0], start[1] };
        while (true)
        {
            var nextPos = new int[] { currentPos[0] + direction[0], currentPos[1] + direction[1] };
            var nextChar = paddedGrid[nextPos[0], nextPos[1]];

            if (nextChar == '+')
            {
                // we hit a wall
                break;
            }

            if (nextChar == '#')
            {
                // turn right
                if (direction == up)
                {
                    direction = right;
                    printCar = '-';
                }
                else if (direction == right)
                {
                    direction = down;
                    printCar = '|';
                }
                else if (direction == down)
                {
                    direction = left;
                    printCar = '-';
                }
                else if (direction == left)
                {
                    direction = up;
                    printCar = '|';
                }
            }
            else
            {
                // move forward
                currentPos = nextPos;

                // update the grid
                if (paddedGrid[currentPos[0], currentPos[1]] == '.')
                {
                    visited++;
                }

                paddedGrid[currentPos[0], currentPos[1]] = printCar;
            }

            // printGrid(paddedGrid);

            steps++;
        }


        printGrid(paddedGrid);

        return visited;
    }

    private int[] Find(Char[,] grid, char c)
    {
        for (var y = 0; y < grid.GetLength(1); y++)
        {
            for (var x = 0; x < grid.GetLength(0); x++)
            {
                if (grid[y, x] == c)
                {
                    return [y, x];
                }
            }
        }

        throw new Exception($"Could not find {c} in grid");
    }

    private void printGrid(Char[,] grid)
    {
        var sb = new StringBuilder();

        sb.AppendLine();
        for (var y = 0; y < grid.GetLength(1); y++)
        {
            for (var x = 0; x < grid.GetLength(0); x++)
            {
                sb.Append(grid[y, x]);
            }
            sb.AppendLine();

        }
        _logger.LogInformation(sb.ToString());
    }

    public long RunPart2(string[] input)
    {
        _logger.LogInformation("Solving - {Year} - Day {Day} - Part 2", _helper.GetYear(), _helper.GetDay());
        _logger.LogInformation("Input contains {Input} values", input.Length);

        throw new NotImplementedException();
    }

}
