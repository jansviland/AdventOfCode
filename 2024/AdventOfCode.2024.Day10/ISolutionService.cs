using System.Collections;
using System.Globalization;
using System.Runtime.Intrinsics.X86;
using Grid;

namespace AdventOfCode._2024.Day10;

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
        
        ILookup<char, Coor<int>> map = input
            .SelectMany((line, y) => line.Select((c, x) => (c, new Coor<int>(y, x))))
            .ToLookup(t => t.c, t => t.Item2);
        
        ICollection<Coor<int>> map2 = input
            .SelectMany((line, y) => line.Select((c, x) => (c, new Coor<int>(y, x))))
            .Select(t => t.Item2)
            .ToList();

        map2.Visualize(null, null);
        

        throw new NotImplementedException();
    }

    public long RunPart2(string[] input)
    {
        _logger.LogInformation("Solving - {Year} - Day {Day} - Part 2", _helper.GetYear(), _helper.GetDay());
        _logger.LogInformation("Input contains {Input} values", input.Length);

        throw new NotImplementedException();
    }
}