using System.Collections.Concurrent;

namespace AdventOfCode._2024.Day11;

public interface ISolutionService
{
    public long RunPart1(string[] input);
    public long RunPart2(string[] input);
    public List<uint> Blink(List<uint> stones, int times);
    public long Blink(long n, int blinks);
}

public class SolutionService : ISolutionService
{
    private readonly ILogger<ISolutionService> _logger;
    private readonly Helper _helper = new();

    public SolutionService(ILogger<SolutionService> logger)
    {
        _logger = logger;
    }

    // 1. We can solve the task recusivly
    public long Blink(long n, int blinks)
    {
        switch ((n, blinks))
        {
            case (n: _, blinks: 0): return 1;
            case (n: 0, blinks: _): return Blink(1, blinks - 1);
            case (n: var stone, _) when stone.ToString().Length % 2 == 0:

                var str = stone.ToString();
                var part1 = str.Substring(0, str.Length / 2);
                var part2 = str.Substring(str.Length / 2);

                return Blink(long.Parse(part1), blinks - 1) + Blink(long.Parse(part2), blinks - 1);

            case (n: var stone, _): return Blink(stone * 2024, blinks - 1);
        }
    }

    // 2. We can store the results for "input string + nr of blinks", to a dictinary, so we don't repeat calculations (cache)
    long Blink(long n, int blinks, ConcurrentDictionary<(long n, int blinks), long> cache)
    {
        // store result of each calculation, and check if it has been calculated already to avoid repeating the same calculation multiple times. 
        // example: 
        //
        // After 6 blinks:
        // 2097446912 14168 4048 2 0 2 4 40 48 2024 40 48 80 96 2 8 6 7 6 0 3 2
        // 
        // here we can se that we have stone "2" and blink "6", 4 times, we also have "40" twice and "48" twice. By storing these results in a lookup
        // dictionary we only do these calculations once
            
        // _logger.LogInformation("Test: {n} - {blinks}", n, blinks);

        if (cache.TryGetValue((n, blinks), out long result))
        {
            _logger.LogInformation("Cache hit: {n} - {blinks} - {result}", n, blinks, result);
            // return result;
        }

        return cache.GetOrAdd((n, blinks), key =>
        {
            switch (key)
            {
                case (n: _, blinks: 0): return cache[(n, blinks)] = 1;

                case (n: 0, blinks: _): return cache[(n, blinks)] = Blink(1, blinks - 1);

                case (n: var stone, _) when stone.ToString().Length % 2 == 0:

                    var str = stone.ToString();
                    var part1 = str.Substring(0, str.Length / 2);
                    var part2 = str.Substring(str.Length / 2);

                    cache[(n, blinks)] = Blink(long.Parse(part1), blinks - 1) + Blink(long.Parse(part2), blinks - 1);

                    return cache[(n, blinks)];

                case (n: var stone, _): return cache[(n, blinks)] = Blink(stone * 2024, blinks - 1);
            }
        });
    }

    public long RunPart1(string[] input)
    {
        _logger.LogInformation("Solving - {Year} - Day {Day} - Part 1", _helper.GetYear(), _helper.GetDay());
        _logger.LogInformation("Input contains {Input} values", input.Length);

        return input[0].Split(" ").Select(long.Parse).Select(x => Blink(x, 25)).Sum();
    }

    public long RunPart2(string[] input)
    {
        _logger.LogInformation("Solving - {Year} - Day {Day} - Part 2", _helper.GetYear(), _helper.GetDay());
        _logger.LogInformation("Input contains {Input} values", input.Length);

        // we use a concurrent dictionary type that is thread safe
        var cache = new ConcurrentDictionary<(long, int), long>();

        return input[0].Split(" ").Select(long.Parse).Select(x => Blink(x, 75, cache)).Sum();
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
}
