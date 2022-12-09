namespace AdventOfCode._2022.Day8;

public interface ISolutionService
{
    public int RunPart1(string[] input);
    public int[,] ParseInput(string[] input);
    public bool IsVisible(int[,] grid, int startX, int startY, Direction direction);
    public int RunPart2(string[] input);
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

    public int RunPart1(string[] input)
    {
        _logger.LogInformation("Solving day 8");
        _logger.LogInformation("Input contains {Input} values", input.Length);

        var grid = ParseInput(input);

        var count = 0;
        for (var x = 0; x < grid.GetLength(0); x++)
        {
            for (var y = 0; y < grid.GetLength(1); y++)
            {
                if (IsVisible(grid, x, y, Direction.Left) ||
                    IsVisible(grid, x, y, Direction.Right) ||
                    IsVisible(grid, x, y, Direction.Up) ||
                    IsVisible(grid, x, y, Direction.Down))
                {
                    count++;
                }
            }
        }

        return count;
    }

    public bool IsVisible(int[,] grid, int startX, int startY, Direction direction)
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

            _logger.LogInformation("Direction {Direction}:: Compare {Value} at {X},{Y} To {Value} at {X},{Y}", direction, grid[startX, startY], startX, startY, grid[x, y], x, y);

            if (grid[x, y] >= grid[startX, startY])
            {
                return false;
            }
        }

        return true;
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

    public int RunPart2(string[] input)
    {
        _logger.LogInformation("Solving day 8 - Part 2");
        _logger.LogInformation("Input contains {Input} values", input.Length);

        throw new NotImplementedException();
    }
}