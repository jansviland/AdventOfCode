namespace AdventOfCode._2025.Day04;

public interface ISolutionService
{
    public long RunPart1(string[] input);
    public long RunPart2(string[] input);
}

public class SolutionService : ISolutionService
{
    private readonly ILogger<ISolutionService> _logger;
    private readonly Helper _helper = new();

    private static readonly Complex Up = -Complex.ImaginaryOne; // - new Complex(0.0, -1.0);
    private static readonly Complex Down = Complex.ImaginaryOne; // new Complex(0.0, 1.0);
    private static readonly Complex Left = -Complex.One; // new Complex(-1.0, 0.0);
    private static readonly Complex Right = Complex.One; // new Complex(1.0, 0.0);
    
    // diagonals
    private static readonly Complex TopLeft = new Complex(-1.0, -1.0);
    private static readonly Complex TopRight = new Complex(1.0, -1.0);
    private static readonly Complex BottomLeft = new Complex(-1.0, 1.0);
    private static readonly Complex BottomRight = new Complex(1.0, 1.0);

    public SolutionService(ILogger<SolutionService> logger)
    {
        _logger = logger;
    }

    Dictionary<Complex, char> Parse(string[] input) => (
        from y in Enumerable.Range(0, input.Length)
        from x in Enumerable.Range(0, input[0].Length)
        let pos = new Complex(y, x)
        let cell = input[y][x]
        select new KeyValuePair<Complex, char>(pos, cell)).ToDictionary();

    int NumberOfNeighboors(Dictionary<Complex, char> grid, Complex pos)
    {
        // go through all 8 possible adjacent posistions
        var directions = new List<Complex> { Up, Down, Left, Right, TopLeft, TopRight, BottomLeft, BottomRight };

        var count = 0;
        foreach (var dir in directions)
        {
            // only check for @
            var current = grid[pos];
            if (current != '@')
            {
                return -1;
            }
            
            var checkPosition = pos + dir;
            if (grid.TryGetValue(checkPosition, out char result))
            {
                if (result == '@')
                {
                    count++;
                }
            }
            
        }

        return count;
    }

    IEnumerable<int> AllNodesNeighboorCount(Dictionary<Complex, char> grid) =>
        from c in grid
        let amount = NumberOfNeighboors(grid, c.Key)
        select amount;
        

    public void Print(Dictionary<Complex, char> grid)
    {
        var maxX = (int)grid.Keys.Max(c => c.Real);
        var maxY = (int)grid.Keys.Max(c => c.Imaginary);
        var sb = new StringBuilder();

        sb.AppendLine();

        for (var y = 0; y <= maxY + 1; y++)
        {
            for (var x = 0; x <= maxX; x++)
            {
                var key = new Complex(y, x);
                var exist = grid.TryGetValue(key, out char value);

                if (exist)
                {
                    var num = NumberOfNeighboors(grid, key);

                    if (grid[key] == '@' && num < 4)
                    {
                        sb.Append('x');
                    }
                    else
                    {
                        sb.Append(value);
                    }
                }
                else
                {
                    sb.Append('.');
                }
            }
            sb.AppendLine();
        }
        
        _logger.LogInformation(sb.ToString());
    }
    
    public long RunPart1(string[] input)
    {
        _logger.LogInformation("Solving - {Year} - Day {Day} - Part 1", _helper.GetYear(), _helper.GetDay());
        _logger.LogInformation("Input contains {Input} values", input.Length);

        // var grid = Parse(input);
        // Print(grid);
        
        return AllNodesNeighboorCount(Parse(input)).Count(x => x < 4 && x != -1);
    }

    public long RunPart2(string[] input)
    {
        _logger.LogInformation("Solving - {Year} - Day {Day} - Part 2", _helper.GetYear(), _helper.GetDay());
        _logger.LogInformation("Input contains {Input} values", input.Length);

        var grid = Parse(input);

        return 0;
    }

}
