namespace AdventOfCode._2023.Day21;

public interface ISolutionService
{
    public long RunPart1(string[] input);
    public long RunPart2(string[] input);
}

public class Tile
{
    public char Char { get; set; }
    public int Steps { get; set; }

    public Tile Previous { get; set; }

    public override string ToString()
    {
        return $"{Char} ({Steps})";
    }
}

public class SolutionService : ISolutionService
{
    private readonly StringBuilder _sb = new();
    private readonly ILogger<ISolutionService> _logger;
    private readonly Dictionary<string, string> _workflows = new();

    public SolutionService(ILogger<SolutionService> logger)
    {
        _logger = logger;
    }

    private Complex FindStartingPoint(Dictionary<Complex, Tile> grid)
    {
        return grid.First(x => x.Value.Char == 'S').Key;
    }

    private Dictionary<Complex, Tile> ParseInput(string[] input)
    {
        return (
            from irow in Enumerable.Range(0, input.Length)
            from icol in Enumerable.Range(0, input[0].Length)
            let cell = input[irow][icol]

            // we are setting y,x here instead of x,y since we read rows first, then columns. 
            // later we will get values by x,y instead of y,x.
            let pos = new Complex(icol, irow)
            let tile = new Tile
            {
                Char = cell,
                Steps = 0
            }
            select new KeyValuePair<Complex, Tile>(pos, tile)
        ).ToDictionary();
    }

    private void PrintGrid(Dictionary<Complex, Tile> grid, bool animated = false, int? step = null)
    {
        if (animated)
        {
            Console.Clear();
            Console.SetCursorPosition(0, 0);
        }

        var minX = grid.Keys.Min(c => c.Real);
        var maxX = grid.Keys.Max(c => c.Real);
        var minY = grid.Keys.Min(c => c.Imaginary);
        var maxY = grid.Keys.Max(c => c.Imaginary);

        for (int y = (int)minY; y <= (int)maxY; y++)
        {
            for (int x = (int)minX; x <= (int)maxX; x++)
            {
                var pos = new Complex(x, y);
                if (grid.TryGetValue(pos, out var tile))
                {
                    if (step != null)
                    {
                        if (tile.Steps == step)
                        {
                            _sb.Append(tile.Steps.ToString().PadLeft(3, ' '));
                        }
                        else
                        {
                            _sb.Append(tile.Char.ToString().PadLeft(3, ' '));
                        }
                    }
                    else if (tile.Steps > 0)
                    {
                        _sb.Append(tile.Steps.ToString().PadLeft(3, ' '));
                    }
                    else
                    {
                        _sb.Append(tile.Char.ToString().PadLeft(3, ' '));
                    }
                }
            }

            _sb.AppendLine();
        }

        Console.WriteLine(_sb.ToString());
        _sb.Clear();

        if (animated)
        {
            Thread.Sleep(200);
        }
    }

    public long NumberOfSpacesReached(Dictionary<Complex, Tile> grid, Complex startingPoint, int steps)
    {
        var spacesReached = 0;
        var currentStep = 0;
        // var currentPoint = startingPoint;
        // var currentTile = grid[startingPoint];

        var q = new Queue<Complex>();
        q.Enqueue(startingPoint);

        var seen = new HashSet<Complex>();

        while (q.TryDequeue(out var currentPosition))
        {
            // check the 4 adjacent spaces
            // if the space is a stone, skip it
            // if the space is a space, add it to the list of spaces + increment the step counter
            var up = currentPosition - Complex.ImaginaryOne;
            var down = currentPosition + Complex.ImaginaryOne;
            var left = currentPosition - Complex.One;
            var right = currentPosition + Complex.One;

            var adjacent = new[] { up, down, left, right };
            foreach (var a in adjacent)
            {
                if (grid.ContainsKey(a))
                // if (grid.ContainsKey(a) && !seen.Contains(a))
                {
                    var tile = grid[a];
                    if (tile.Char == '.')
                    {

                        grid[a].Previous = grid[currentPosition];
                        grid[a].Steps = grid[currentPosition].Steps + 1;

                        // if steps is less than the given amount of steps, add it to the queue

                        q.Enqueue(a);
                    }
                }
            }

            seen.Add(currentPosition);
            // grid[currentPosition].Steps++;

            // currentSteps++;
            PrintGrid(grid, true);

            // Console.WriteLine($"Current steps: {currentStep}");
        }

        return seen.Count;
    }

    public long RunPart1(string[] input)
    {
        _logger.LogInformation("Solving - 2023 - Day 21 - Part 1");
        _logger.LogInformation("Input contains {Input} values", input.Length);

        // Create a grid of stones and spaces
        var grid = ParseInput(input);

        // Find S, the starting point of the path
        var startingPoint = FindStartingPoint(grid);

        // given an amount of steps, how many spaces / garden plots could be reached
        // from the starting point
        var spacesReached = NumberOfSpacesReached(grid, startingPoint, 32);

        // create a while loop counting to the given amount of steps
        // for each step, check the 4 adjacent spaces
        // if the space is a stone, skip it
        // if the space is a space, add it to the list of spaces + increment the step counter

        // print out a grid, with the current steps and the spaces reached for each step, animate it

        PrintGrid(grid, true, 6);


        return spacesReached;
    }

    public long RunPart2(string[] input)
    {
        _logger.LogInformation("Solving - 2023 - Day 21 - Part 2");
        _logger.LogInformation("Input contains {Input} values", input.Length);

        throw new NotImplementedException();
    }
}