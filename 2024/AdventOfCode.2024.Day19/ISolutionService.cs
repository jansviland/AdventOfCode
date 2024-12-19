namespace AdventOfCode._2024.Day19;

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

    (string[] patterns, string[] designs) Parse(string[] input)
    {
        var patterns = input
            .TakeWhile(x => x.Contains(',')).First()
            .Split(',')
            .Select(x => x.Trim())
            .ToArray();

        var designs = input
            .SkipWhile(string.IsNullOrWhiteSpace)
            .SkipWhile(x => x.Contains(','))
            .ToArray();

        return (patterns, designs);
    }

    public long RunPart1(string[] input)
    {
        _logger.LogInformation("Solving - {Year} - Day {Day} - Part 1", _helper.GetYear(), _helper.GetDay());
        _logger.LogInformation("Input contains {Input} values", input.Length);

        var (patterns, designs) = Parse(input);

        var patternMinWidth = int.MaxValue;
        var patternMaxWidth = 0;

        for (var i = 0; i < patterns.Length; i++)
        {
            if (patterns[i].Length < patternMinWidth)
            {
                patternMinWidth = patterns[i].Length;
            }
            if (patterns[i].Length > patternMaxWidth)
            {
                patternMaxWidth = patterns[i].Length;
            }
        }

        var count = 0;
        for (var i = 0; i < designs.Length; i++)
        {
            _logger.LogInformation(designs[i]);

            if (designs[i].Length == 0)
            {
                continue;
            }

            if (DesignCanBeMade(designs[i], patterns, patternMinWidth, patternMaxWidth))
            {
                count++;
            }
        }

        return count;
    }
    private bool DesignCanBeMade(string design, string[] patterns, int min, int max)
    {
        // p = current pos
        // i = is to gradualy descrese max width of the pattern we look for

        for (var p = 0; p < design.Length;)
        {
            var currentMaxWidth = max - p;
            
            for (var i = 0; i < currentMaxWidth; i++)
            {
                // create a substring starting from current position in design string
                // starting with looking for the entire string in pattern
                // then a smaller and smaller subset 
                var lookup = design.Substring(p, currentMaxWidth - i).Trim();
                var exist = patterns.Any(x => x.Equals(lookup, StringComparison.Ordinal));
                
                // _logger.LogInformation("Lookup: '{Lookup}', Patterns: {Patterns}", lookup, string.Join(", ", patterns));
                
                if (exist)
                {
                    p += currentMaxWidth - i;
                    
                    _logger.LogInformation("Adding {Pattern} to {Current} ", lookup, design.Substring(0, p));
                }
                // else
                // {
                //     _logger.LogError("Pattern not found. Lookup: '{Lookup}', Patterns: {Patterns}", lookup, string.Join(", ", patterns));
                //     foreach (var pattern in patterns)
                //     {
                //         _logger.LogError("Comparing '{Lookup}' with '{Pattern}': {Result}", lookup, pattern, pattern.Equals(lookup, StringComparison.OrdinalIgnoreCase));
                //     }
                // }

                if (lookup.Length == 1)
                {
                    return false;
                }
                
            }
        }

        return true;
    }

    public long RunPart2(string[] input)
    {
        _logger.LogInformation("Solving - {Year} - Day {Day} - Part 2", _helper.GetYear(), _helper.GetDay());
        _logger.LogInformation("Input contains {Input} values", input.Length);

        throw new NotImplementedException();

    }
}
