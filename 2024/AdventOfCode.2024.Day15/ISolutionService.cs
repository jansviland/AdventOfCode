using Microsoft.Extensions.Primitives;

namespace AdventOfCode._2024.Day15;

public interface ISolutionService
{
    public long RunPart1(string[] input);
    public long RunPart2(string[] input);
}

public class SolutionService : ISolutionService
{
    private readonly ILogger<ISolutionService> _logger;
    private readonly Helper _helper = new();

    Complex Up = Complex.ImaginaryOne;

    public SolutionService(ILogger<SolutionService> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// Parse map input
    /// </summary>
    /// <returns>Dict with map, complex coordinate as key, char as value. And start pos</returns>
    public (Dictionary<Complex, char>, Complex) ParseMap(string[] input)
    {
        var map = (
            from y in Enumerable.Range(0, input.Length)
            from x in Enumerable.Range(0, input[0].Length)
            select new KeyValuePair<Complex, char>(Up * y + x, input[y][x])
        ).ToDictionary();

        var start = map.First(x => x.Value == '@').Key;

        return (map, start);
    }

    public void PrintMap(Dictionary<Complex, char> map)
    {
        var sb = new StringBuilder();
        sb.Append(Environment.NewLine);
        
        // Determine the bounds of the map
        int minX = (int)map.Keys.Min(c => c.Real);
        int maxX = (int)map.Keys.Max(c => c.Real);
        int minY = (int)map.Keys.Min(c => c.Imaginary);
        int maxY = (int)map.Keys.Max(c => c.Imaginary);
        
        for (var y = minY; y <= maxY; y++)
        {
            for (var x = minX; x <= maxX; x++)
            {
                Complex key = new Complex(x, y);
                if (map.TryGetValue(key, out char value))
                {
                    sb.Append(value);
                }
                else
                {
                    sb.Append(' ');
                }
            }
            sb.Append(Environment.NewLine);
        }

        _logger.LogInformation(sb.ToString());
    }

    public long RunPart1(string[] input)
    {
        _logger.LogInformation("Solving - {Year} - Day {Day} - Part 1", _helper.GetYear(), _helper.GetDay());
        _logger.LogInformation("Input contains {Input} values", input.Length);

        var mapInput = input.TakeWhile(l => l.Contains('#')).ToArray();
        var (map, start) = ParseMap(mapInput);

        PrintMap(map);

        throw new NotImplementedException();
    }

    public long RunPart2(string[] input)
    {
        _logger.LogInformation("Solving - {Year} - Day {Day} - Part 2", _helper.GetYear(), _helper.GetDay());
        _logger.LogInformation("Input contains {Input} values", input.Length);

        throw new NotImplementedException();
    }

}
