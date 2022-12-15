namespace AdventOfCode._2022.Day8;

public interface ISolutionService
{
    public int RunPart1(string[] input, int debugLevel = 0);
    public int[,] ParseInput(string[] input);
    public bool IsVisible(int[,] grid, int startX, int startY, Direction direction, int debugLevel = 0);
    public int NumberOfVisibleTrees(int[,] grid, int startX, int startY, Direction direction, int debugLevel = 0);
    public int RunPart2(string[] input, int debugLevel = 0);
}

public enum Direction
{
    Left,
    Right,
    Up,
    Down
}

public class SolutionService : ISolutionService
{
    private readonly ILogger<SolutionService> _logger;

    public SolutionService(ILogger<SolutionService> logger)
    {
        _logger = logger;
    }

    public int RunPart1(string[] input, int debugLevel = 0)
    {
        _logger.LogInformation("Solving day 8");
        _logger.LogInformation("Input contains {Input} values", input.Length);

        var grid = ParseInput(input);

        var count = 0;
        for (var x = 0; x < grid.GetLength(0); x++)
        {
            for (var y = 0; y < grid.GetLength(1); y++)
            {
                if (IsVisible(grid, x, y, Direction.Left, debugLevel) ||
                    IsVisible(grid, x, y, Direction.Right, debugLevel) ||
                    IsVisible(grid, x, y, Direction.Up, debugLevel) ||
                    IsVisible(grid, x, y, Direction.Down, debugLevel))
                {
                    count++;
                }
            }
        }

        return count;
    }

    public bool IsVisible(int[,] grid, int startX, int startY, Direction direction, int debugLevel = 0)
    {
        if (startX == 0 || startX == grid.GetLength(0) - 1 || startY == 0 || startY == grid.GetLength(1) - 1)
        {
            return true;
        }

        for (var i = 1; i < grid.GetLength(0); i++)
        {
            int x = startX, y = startY;

            switch (direction)
            {
                case Direction.Left:
                    x -= i;
                    break;
                case Direction.Right:
                    x += i;
                    break;
                case Direction.Up:
                    y -= i;
                    break;
                case Direction.Down:
                    y += i;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
            }

            if (x < 0 || x >= grid.GetLength(0) || y < 0 || y >= grid.GetLength(1))
            {
                break;
            }

            if (debugLevel > 0)
            {
                _logger.LogInformation("Direction {Direction}:: Compare {Value} at {X},{Y} To {Value2} at {X2},{Y2}", direction, grid[startX, startY], startX, startY, grid[x, y], x, y);
            }

            if (grid[x, y] >= grid[startX, startY])
            {
                return false;
            }
        }

        return true;
    }

    public int NumberOfVisibleTrees(int[,] grid, int startX, int startY, Direction direction, int debugLevel = 0)
    {
        // navigate in one direction until you hit a tree, then return the number of trees you hit
        var count = 0;
        for (var i = 1; i < grid.GetLength(0); i++)
        {
            var x = startX;
            var y = startY;

            switch (direction)
            {
                case Direction.Left:
                    x -= i;
                    break;
                case Direction.Right:
                    x += i;
                    break;
                case Direction.Up:
                    y -= i;
                    break;
                case Direction.Down:
                    y += i;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
            }

            if (x < 0 || x >= grid.GetLength(0) || y < 0 || y >= grid.GetLength(1))
            {
                break;
            }

            if (debugLevel > 0)
            {
                _logger.LogInformation("Direction {Direction}:: Compare {Value} at {X},{Y} To {Value2} at {X2},{Y2}", direction, grid[startX, startY], startX, startY, grid[x, y], x, y);
            }

            // tree in the current direction is lower
            if (grid[x, y] < grid[startX, startY])
            {
                count++;
            }
            else
            {
                return count + 1;
            }
        }

        return count;
    }

    public int[,] ParseInput(string[] input)
    {
        var result = new int[input.Length, input.Length];

        for (var x = 0; x < input.Length; x++)
        {
            for (var y = 0; y < input[x].Length; y++)
            {
                result[y, x] = int.Parse(input[x][y].ToString());
            }
        }

        return result;
    }

    public int RunPart2(string[] input, int debugLevel)
    {
        _logger.LogInformation("Solving day 8 - Part 2");
        _logger.LogInformation("Input contains {Input} values", input.Length);

        var grid = ParseInput(input);

        // create a list of coordinates with a value
        var coordinates = new List<(int x, int y, int score)>();

        for (var x = 1; x < grid.GetLength(0) - 1; x++)
        {
            for (var y = 1; y < grid.GetLength(1) - 1; y++)
            {
                var left = NumberOfVisibleTrees(grid, x, y, Direction.Left, debugLevel);
                var right = NumberOfVisibleTrees(grid, x, y, Direction.Right, debugLevel);
                var up = NumberOfVisibleTrees(grid, x, y, Direction.Up, debugLevel);
                var down = NumberOfVisibleTrees(grid, x, y, Direction.Down, debugLevel);

                var score = left * right * up * down;

                coordinates.Add((x, y, score));
            }
        }

        var result = coordinates.OrderByDescending(x => x.score).First();

        _logger.LogInformation("Best view is at {X},{Y} with a score of {Score}", result.x, result.y, result.score);

        return result.score;
    }
}