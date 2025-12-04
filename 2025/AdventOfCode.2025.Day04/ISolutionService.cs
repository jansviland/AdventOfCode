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
    
    // TODO: add the other four directions, TopLeft, TopRight 

    public SolutionService(ILogger<SolutionService> logger)
    {
        _logger = logger;
    }

    Dictionary<Complex, char> Parse(string[] input) => (
        from irow in Enumerable.Range(0, input.Length)
        from icol in Enumerable.Range(0, input[0].Length)
        let pos = new Complex(icol, irow)
        let cell = input[irow][icol]
        select new KeyValuePair<Complex, char>(pos, cell)).ToDictionary();

    int NumberOfNeighboors(Dictionary<Complex, char> grid, Complex pos)
    {
        // go through all 8 possible adjacent posistions
        var directions = new List<Complex> { Up, Down, Left, Right };

        var count = 0;
        foreach (var dir in directions)
        {
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

    public long RunPart1(string[] input)
    {
        _logger.LogInformation("Solving - {Year} - Day {Day} - Part 1", _helper.GetYear(), _helper.GetDay());
        _logger.LogInformation("Input contains {Input} values", input.Length);

        var grid = Parse(input);

        foreach (var c in grid)
        {
            var count = NumberOfNeighboors(grid, c.Key);
        }
        

        return 0;
    }

    public long RunPart2(string[] input)
    {
        _logger.LogInformation("Solving - {Year} - Day {Day} - Part 2", _helper.GetYear(), _helper.GetDay());
        _logger.LogInformation("Input contains {Input} values", input.Length);

        var grid = Parse(input);

        return 0;
    }

}
