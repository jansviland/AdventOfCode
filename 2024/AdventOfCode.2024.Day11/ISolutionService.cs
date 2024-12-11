using System.Collections;
using System.Globalization;
using System.Runtime.Intrinsics.X86;

namespace AdventOfCode._2024.Day11;

public interface ISolutionService
{
    public long RunPart1(string[] input);
    public long RunPart2(string[] input);
    public List<uint> Blink(List<uint> stones, int times);
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

        var stones = input[0].Split(" ").Select(uint.Parse).ToList();
        var updated = Blink(stones, 25);

        return updated.Count();
    }

    public long RunPart2(string[] input)
    {
        _logger.LogInformation("Solving - {Year} - Day {Day} - Part 2", _helper.GetYear(), _helper.GetDay());
        _logger.LogInformation("Input contains {Input} values", input.Length);

        throw new NotImplementedException();

    }

    public List<uint> ApplyRules(uint stone)
    {

        var result = new List<uint>();
        if (stone == 0)
        {
            result.Add(1);
        }
        else if (stone.ToString().Length % 2 == 0)
        {
            var str = stone.ToString();
            var right = str.Substring(str.Length / 2);
            var left = str.Substring(0, str.Length / 2);

            if (ulong.TryParse(left, out _))
            {
                result.Add(uint.Parse(left));
            }

            if (ulong.TryParse(right, out _))
            {
                result.Add(uint.Parse(right));
            }

        }
        else
        {
            result.Add(stone * 2024);
        }
        
        // _logger.LogInformation("{Stone} -> {Result}", stone, string.Join(", ", result));

        return result;
    }

    public List<uint> Blink(List<uint> stones, int times)
    {
        var current = stones;
        for (var i = 0; i < times; i++)
        {
            var updated = new List<uint>();
            foreach (uint stone in current)
            {
                updated.AddRange(ApplyRules(stone));
            }

            // _logger.LogInformation("After {Current} blink: {Stones}", i, string.Join(", ", updated));
            current = updated;
        }

        return current;
    }

    private static IEnumerable<int> Column(string[] input, int column) =>
        from line in input
        let nums = line.Split("   ").Select(int.Parse).ToArray()
        orderby nums[column]
        select nums[column];
}
