using System.Collections.Concurrent;

namespace AdventOfCode._2024.Day11;

public interface ISolutionService
{
    public long RunPart1(string[] input);
    public long RunPart2(string[] input);
    public long Blink(long n, int blinks, ConcurrentDictionary<(long n, int blinks), long> cache);
}

public class SolutionService : ISolutionService
{
    private readonly ILogger<ISolutionService> _logger;
    private readonly Helper _helper = new();

    public SolutionService(ILogger<SolutionService> logger)
    {
        _logger = logger;
    }

    // add cache to improve preformance
    public long Blink(long n, int blinks, ConcurrentDictionary<(long n, int blinks), long> cache)
    {
        // store result of each calculation, and check if it has been calculated already to avoid repeating the same calculation multiple times. 
        // example: 
        //
        // After 6 blinks:
        // 2097446912 14168 4048 2 0 2 4 40 48 2024 40 48 80 96 2 8 6 7 6 0 3 2
        // 
        // here we can se that we have stone "2" and blink "6", 4 times, we also have "40" twice and "48" twice. By storing these results in a lookup
        // dictionary we only do these calculations once
        return cache.GetOrAdd((n, blinks), key =>
        {
            switch (key)
            {
                case (n: _, blinks: 0): return 1;

                case (n: 0, blinks: _): return Blink(1, blinks - 1, cache);

                case (n: var stone, _) when stone.ToString().Length % 2 == 0:

                    var str = stone.ToString();
                    var part1 = str.Substring(0, str.Length / 2);
                    var part2 = str.Substring(str.Length / 2);

                    return Blink(long.Parse(part1), blinks - 1, cache) + Blink(long.Parse(part2), blinks - 1, cache);

                case (n: var stone, _): return Blink(stone * 2024, blinks - 1, cache);
            }
        });
    }

    public long RunPart1(string[] input)
    {
        _logger.LogInformation("Solving - {Year} - Day {Day} - Part 1", _helper.GetYear(), _helper.GetDay());
        _logger.LogInformation("Input contains {Input} values", input.Length);

        // we use a concurrent dictionary type that is thread safe
        var cache = new ConcurrentDictionary<(long, int), long>();

        return input[0].Split(" ").Select(long.Parse).Select(x => Blink(x, 25, cache)).Sum();
    }

    public long RunPart2(string[] input)
    {
        _logger.LogInformation("Solving - {Year} - Day {Day} - Part 2", _helper.GetYear(), _helper.GetDay());
        _logger.LogInformation("Input contains {Input} values", input.Length);

        // we use a concurrent dictionary type that is thread safe
        var cache = new ConcurrentDictionary<(long, int), long>();

        return input[0].Split(" ").Select(long.Parse).Sum(n => Blink(n, 75, cache));
    }
}
