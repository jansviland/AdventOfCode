namespace AdventOfCode._2024.Day02;

public interface ISolutionService
{
    public long RunPart1(string[] input);
    public long RunPart2(string[] input);
}

public class SolutionServiceV2 : ISolutionService
{
    private readonly ILogger<ISolutionService> _logger;
    private readonly Helper _helper = new();

    public SolutionServiceV2(ILogger<SolutionService> logger)
    {
        _logger = logger;
    }

    public long RunPart1(string[] input)
    {
        _logger.LogInformation("Solving - {Year} - Day {Day} - Part 1", _helper.GetYear(), _helper.GetDay());
        _logger.LogInformation("Input contains {Input} values", input.Length);
        
        return input.Count(line => IsValid(ConvertToIntArray(line).ToArray()));
    }

    public long RunPart2(string[] input)
    {
        _logger.LogInformation("Solving - {Year} - Day {Day} - Part 2", _helper.GetYear(), _helper.GetDay());
        _logger.LogInformation("Input contains {Input} values", input.Length);
        
        return input.Count(line => GetAllVariations(ConvertToIntArray(line)).Any(IsValid));
    }
    
    private int[] ConvertToIntArray(string line)
    {
        return line.Split(' ').Select(int.Parse).ToArray();
    }

    private bool IsValid(int[] values)
    {
        var pairs = values.Zip(values.Skip(1));
        return pairs.All(p => 1 <= p.First - p.Second && p.First - p.Second <= 3) || pairs.All(p => 1 <= p.Second - p.First && p.Second - p.First <= 3);
    }

    IEnumerable<int[]> GetAllVariations(int[] samples) =>
        from i in Enumerable.Range(0, samples.Length+1)
        let before = samples.Take(i - 1)
        let after = samples.Skip(i)
        select Enumerable.Concat(before, after).ToArray();
    
    private IEnumerable<int[]> GetAllVariations2(int[] values)
    {
        yield return values;

        for (var i = 0; i < values.Length; i++)
        {
            var newValues = values.ToList();
            newValues.RemoveAt(i);
            yield return newValues.ToArray();
        }
    }
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

        var safeLines = 0;

        foreach (var line in input)
        {
            int[] values = line.Split(" ").Select(int.Parse).ToArray();

            if (IsSafe(values))
            {
                safeLines++;
            }
        }

        return safeLines;
    }

    private bool IsSafe(int[] values)
    {
        // _logger.LogInformation("Checking line: {Line}", string.Join(" ", values));

        // check if the values is increasing or decreasing
        bool increasing = true;
        int diff = values[0] - values[1];
        if (diff < 0)
        {
            increasing = false;
        }

        for (var i = 0; i < values.Length - 1; i++)
        {
            var current = values[i];
            var next = values[i + 1];
            var diffToAdjacent = current - next;

            if (diffToAdjacent < 0 && increasing)
            {
                // _logger.LogInformation("Line was increasing and then decreased");
                return false;
            }
            if (diffToAdjacent > 0 && !increasing)
            {
                // _logger.LogInformation("Line was decreasing and then increased");
                return false;
            }

            var absDiff = Math.Abs(diffToAdjacent);
            if (absDiff > 3)
            {
                // _logger.LogInformation("Line had a difference greater than 3");
                return false;
            }

            if (absDiff < 1)
            {
                // _logger.LogInformation("Line had a difference less than 1");
                return false;
            }
        }

        return true;
    }

    public long RunPart2(string[] input)
    {
        _logger.LogInformation("Solving - {Year} - Day {Day} - Part 2", _helper.GetYear(), _helper.GetDay());
        _logger.LogInformation("Input contains {Input} values", input.Length);

        // for each line, check variations of the line where one value is removed
        // example: 1 2 3 4 5
        // if that does not work, attempt 2 3 4 5, then 1 3 4 5, then 1 2 4 5, then 1 2 3 5, then 1 2 3 4

        var safeLines = 0;

        foreach (var line in input)
        {
            var values = line.Split(" ").Select(int.Parse).ToArray();

            if (IsSafe(values))
            {
                safeLines++;
                continue;
            }

            for (var i = 0; i < values.Length; i++)
            {
                var newValues = values.ToList();
                newValues.RemoveAt(i);

                if (IsSafe(newValues.ToArray()))
                {
                    safeLines++;
                    break;
                }
            }

        }

        return safeLines;
    }
}
