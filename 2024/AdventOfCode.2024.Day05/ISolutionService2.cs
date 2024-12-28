


namespace AdventOfCode._2024.Day05;

// public interface ISolutionService
// {
//     public long RunPart1(string[] input);
//     public long RunPart2(string[] input);
// }

public static class Parse
{
    public static string[] SplitBy(this string s, string separator)
        => s.Split(separator, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

    public static int[] SplitToNumbers(this string csv, string delimiter = ",") => csv
        .SplitBy(delimiter)
        .Select(int.Parse)
        .ToArray();
} 


public class SolutionService2 : ISolutionService
{
    private readonly ILogger<ISolutionService> _logger;
    private readonly Helper _helper = new();

    public SolutionService2(ILogger<SolutionService> logger)
    {
        _logger = logger;
    }

    // inspired by premun
    public long RunPart1(string[] input)
    {
        _logger.LogInformation("Solving - {Year} - Day {Day} - Part 1", _helper.GetYear(), _helper.GetDay());
        _logger.LogInformation("Input contains {Input} values", input.Length);

        var rules = input
            .TakeWhile(l => l.Contains('|')) // parse rules like this "47|53"
            .Select(l => l.SplitToNumbers("|")) // split into int array
            .Select(numbers => (First: numbers[0], Second: numbers[1]));

        var updates = input
            .SkipWhile(l => l.Contains('|'))
            .SkipWhile(string.IsNullOrEmpty)
            .Select(l => l.SplitToNumbers());
        
        var dependentOn = new Dictionary<int, HashSet<int>>();
        foreach (var (former, latter) in rules)
        {
            if (!dependentOn.TryGetValue(latter, out HashSet<int>? deps))
            {
                deps = [];
                dependentOn[latter] = deps;
            }

            deps.Add(former);
        }

        return GetMiddlePage(false, dependentOn, updates);
    }

    static int GetMiddlePage(bool takeIncorrectlyReordered, Dictionary<int, HashSet<int>> rules, IEnumerable<int[]> updates)
    {
        var result = 0;
        foreach (var update in updates)
        {
            var isCorrect = true;
            for (int i = 0; i < update.Length; i++)
            {
                Again:
                var shouldBeEarlier = rules[update[i]];
                
                // similar to bubblesort, where we compare i and i+1 and then swap
                for (int j = i + 1; j < update.Length; ++j)
                {
                    if (shouldBeEarlier.Contains(update[j]))
                    {
                        isCorrect = false;
                        if (takeIncorrectlyReordered)
                        {
                            (update[i], update[j]) = (update[j], update[i]); // swap
                            i = 0;
                            goto Again;
                        }
                        else
                        {
                            break;
                        }
                    }
                }

                if (!takeIncorrectlyReordered && !isCorrect)
                {
                    break;
                }
            }

            if (takeIncorrectlyReordered != isCorrect)
            {
                result += update[update.Length / 2];
            }
        }

        return result;
    }

    public long RunPart2(string[] input)
    {
        _logger.LogInformation("Solving - {Year} - Day {Day} - Part 2", _helper.GetYear(), _helper.GetDay());
        _logger.LogInformation("Input contains {Input} values", input.Length);
        
        var rules = input
            .TakeWhile(l => l.Contains('|')) // parse rules like this "47|53"
            .Select(l => l.SplitToNumbers("|")) // split into int array
            .Select(numbers => (First: numbers[0], Second: numbers[1]));

        var updates = input
            .SkipWhile(l => l.Contains('|'))
            .SkipWhile(string.IsNullOrEmpty)
            .Select(l => l.SplitToNumbers());
        
        var dependentOn = new Dictionary<int, HashSet<int>>();
        foreach (var (former, latter) in rules)
        {
            if (!dependentOn.TryGetValue(latter, out HashSet<int>? deps))
            {
                deps = [];
                dependentOn[latter] = deps;
            }

            deps.Add(former);
        }

        return GetMiddlePage(true, dependentOn, updates);
    }
}

