using System.Data;
using Common;

namespace AdventOfCode._2024.Day06;

// inspired by encse
public class SolutionService2 : ISolutionService
{
    private readonly ILogger<ISolutionService> _logger;
    private readonly Helper _helper = new();

    // A complex number can be represented as a point in a two-dimensional coordinate system, which is known as the complex plane
    Complex Up = Complex.ImaginaryOne;
    Complex TurnRight = -Complex.ImaginaryOne;

    public SolutionService2(ILogger<SolutionService> logger)
    {
        _logger = logger;
    }

    public long RunPart1(string[] input)
    {
        _logger.LogInformation("Solving - {Year} - Day {Day} - Part 1", _helper.GetYear(), _helper.GetDay());
        _logger.LogInformation("Input contains {Input} values", input.Length);

        var (map, start) = Parse(input);
        
        throw new NotImplementedException();
    }
    
    /// <summary>
    /// Parse input like this:
    ///
    ///     ....#.....
    ///     .........#
    ///     ..........
    ///     ..#.......
    ///     .......#..
    ///     ..........
    ///     .#..^.....
    ///     ........#.
    ///     #.........
    ///     ......#...
    /// </summary>
    /// <returns>Tuple with two types, one Dictonary that has Complex Cooridnate as key and char as value, and the starting position</returns>
    public (Dictionary<Complex, char> map, Complex start) Parse(string[] input)
    {
        var map = (
            from y in Enumerable.Range(0, input.Length)
            from x in Enumerable.Range(0, input[0].Length)
            select new KeyValuePair<Complex, char>(-Up * y + x, input[y][x])
        ).ToDictionary();

        var start = map.First(a => a.Value == '^').Key;

        return (map, start);
    }

    public long RunPart2(string[] input)
    {
        _logger.LogInformation("Solving - {Year} - Day {Day} - Part 2", _helper.GetYear(), _helper.GetDay());
        _logger.LogInformation("Input contains {Input} values", input.Length);

        throw new NotImplementedException();
    }

}
