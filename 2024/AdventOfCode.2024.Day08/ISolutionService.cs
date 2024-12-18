namespace AdventOfCode._2024.Day08;

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

    Dictionary<Complex, char> Parse(string[] input)
    {
        var map = (
            from y in Enumerable.Range(0, input.Length)
            from x in Enumerable.Range(0, input[0].Length)
            where input[y][x] != '.'
            select new KeyValuePair<Complex, char>(Complex.ImaginaryOne * y + x, input[y][x])
        ).ToDictionary();

        return map;
    }

    void Print(Dictionary<Complex, char> map)
    {
        var sb = new StringBuilder();
        sb.AppendLine();

        var maxX = map.Keys.Max(c => c.Real);
        var maxY = map.Keys.Max(c => c.Imaginary);

        for (var y = 0; y <= maxY; y++)
        {
            for (var x = 0; x <= maxX; x++)
            {
                var key = new Complex(x, y);
                sb.Append(map.TryGetValue(key, out char value) ? value : '.');
                sb.Append(' ');
            }
            sb.AppendLine();
        }

        _logger.LogInformation(sb.ToString());
    }

    public long RunPart1(string[] input)
    {
        _logger.LogInformation("Solving - {Year} - Day {Day} - Part 1", _helper.GetYear(), _helper.GetDay());
        _logger.LogInformation("Input contains {Input} values", input.Length);

        var map = Parse(input);
        Print(map);
        
        

        throw new NotImplementedException();
    }

    public long RunPart2(string[] input)
    {
        _logger.LogInformation("Solving - {Year} - Day {Day} - Part 2", _helper.GetYear(), _helper.GetDay());
        _logger.LogInformation("Input contains {Input} values", input.Length);

        throw new NotImplementedException();
    }
}
