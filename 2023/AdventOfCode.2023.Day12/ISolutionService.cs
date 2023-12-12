namespace AdventOfCode._2023.Day12;

public interface ISolutionService
{
    public long RunPart1(string[] input);
    public string[] GetPossibleArrangements(string line);
    // public long GetPossibleArrangementsCount(string line);
    public string UnFold(string line);
    public long RunPart2(string[] input);
}

public class SolutionService : ISolutionService
{
    private readonly ILogger<ISolutionService> _logger;
    private readonly StringBuilder _sb = new StringBuilder();

    private readonly char[] combinationLetters = new[] { '#', '.' };

    public SolutionService(ILogger<SolutionService> logger)
    {
        _logger = logger;
    }

    public long RunPart1(string[] input)
    {
        _logger.LogInformation("Solving - 2023 - Day 12 - Part 1");
        _logger.LogInformation("Input contains {Input} values", input.Length);

        var count = 0;
        foreach (string line in input)
        {
            var possibleArrangements = GetPossibleArrangements(line);
            count += possibleArrangements.Length;
            _logger.LogInformation("Line: {Line}, Possible arrangements: {PossibleArrangements}", line, possibleArrangements.Length);
        }

        return count;
    }

    // TODO: need to improve this, so it filters out more invalid combinations earlier
    // generate all combinations of # and . for any number of wildcards
    public List<string> GenerateCombinations(string current, int length)
    {
        if (current.Length == length)
        {
            return new List<string> { current };
        }

        var combinations = new List<string>();

        foreach (var c in combinationLetters)
        {
            combinations.AddRange(GenerateCombinations(current + c, length));
        }

        return combinations;
    }

    // public string? GetStringCombination(string combination, string pattern, int[] groups)
    // {
    //     _sb.Clear();
    //
    //     var currentCombinationIndex = 0;
    //     for (var i = 0; i < pattern.Length; i++)
    //     {
    //         if (pattern[i] == '?')
    //         {
    //             _sb.Append(combination[currentCombinationIndex]);
    //             currentCombinationIndex++;
    //         }
    //         else if (pattern[i] == '.')
    //         {
    //             _sb.Append('.');
    //
    //             // if . check the groups and see if it's a match
    //             // for example if the pattern is ##. and the groups are 1,1,3,
    //             // then we already know that it is wrong since the first group is length 1, not 2 as we have now
    //             var cg = _sb.ToString()
    //                 .Split('.')
    //                 .Where(x => !string.IsNullOrWhiteSpace(x))
    //                 .Select(x => x.Length).ToArray();
    //
    //             if (cg.Where((t, i) => t != groups[i]).Any())
    //             {
    //                 return null;
    //             }
    //         }
    //         else
    //         {
    //             _sb.Append(pattern[i]);
    //         }
    //     }
    //
    //     return _sb.ToString();
    // }

    // public long GetPossibleArrangementsCount(string line)
    // {
    //     var split = line.Split(' ');
    //     var pattern = split[0];
    //     int[] groups = split[1].Split(',').Select(int.Parse).ToArray();
    //
    //     int numberOfWildcards = pattern.Count(c => c == '?');
    //     var combinations = GenerateCombinations("", numberOfWildcards);
    //
    //     long total = 0;
    //     foreach (var combination in combinations)
    //     {
    //         var currentCombinationIndex = 0;
    //         var updatedString = GetStringCombination(combination, pattern, groups);
    //
    //         if (updatedString == null)
    //         {
    //             continue;
    //         }
    //         
    //         _logger.LogInformation("Line: {Line}, Possible arrangement: {PossibleArrangements}", line, _sb.ToString());
    //
    //         // check if the pattern matches the groups
    //         // split by ., then count the length of each group, so #.#.### becomes #, #, ###, and then we count each one, so 1,1,3
    //         var currentGroups = _sb.ToString()
    //             .Split('.')
    //             .Where(x => !string.IsNullOrWhiteSpace(x))
    //             .Select(x => x.Length).ToArray();
    //
    //         // if the lengths are not the same, then it's not a match
    //         if (currentGroups.Length != groups.Length)
    //         {
    //             continue;
    //         }
    //
    //         // if the lengths are the same, but the values are not the same, then it's not a match
    //         // we use t and i to get the value and index of each group. i is the index we can use this to compare to our goal groups
    //         // and we use t to that count the length of each group and can compare it to the goal groups
    //         if (currentGroups.Where((t, i) => t != groups[i]).Any())
    //         {
    //             continue;
    //         }
    //
    //         _logger.LogInformation("Line: {Line}, Possible arrangement: {PossibleArrangements}", line, _sb.ToString());
    //
    //         total++;
    //     }
    //
    //     return total;
    // }

    public string[] GetPossibleArrangements(string line)
    {
        var split = line.Split(' ');
        var pattern = split[0];
        var groups = split[1].Split(',').Select(int.Parse).ToArray();

        int numberOfWildcards = pattern.Count(c => c == '?');
        var combinations = GenerateCombinations("", numberOfWildcards);

        // var sb = new StringBuilder();

        var possibleArrangements = new List<string>();
        foreach (var combination in combinations)
        {
            _sb.Clear();

            var count = 0;
            for (var i = 0; i < pattern.Length; i++)
            {
                if (pattern[i] == '?')
                {
                    _sb.Append(combination[count]);
                    count++;
                }
                else if (pattern[i] == '.')
                {
                    _sb.Append('.');

                    // if . check the groups and see if it's a match
                    // for example if the pattern is ##. and the groups are 1,1,3,
                    // then we already know that it is wrong since the first group is length 1, not 2 as we have now

                    var cg = _sb.ToString()
                        .Split('.')
                        .Where(x => !string.IsNullOrWhiteSpace(x))
                        .Select(x => x.Length).ToArray();

                    if (cg.Where((t, i) => t != groups[i]).Any())
                    {
                        break;
                    }
                }
                else
                {
                    _sb.Append(pattern[i]);
                }

            }

            // check if the pattern matches the groups
            // split by ., then count the length of each group, so #.#.### becomes #, #, ###, and then we count each one, so 1,1,3
            var currentGroups = _sb.ToString()
                .Split('.')
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Select(x => x.Length).ToArray();

            // if the lengths are not the same, then it's not a match
            if (currentGroups.Length != groups.Length)
            {
                continue;
            }

            // if the lengths are the same, but the values are not the same, then it's not a match
            // we use t and i to get the value and index of each group. i is the index we can use this to compare to our goal groups
            // and we use t to that count the length of each group and can compare it to the goal groups
            if (currentGroups.Where((t, i) => t != groups[i]).Any())
            {
                continue;
            }

            possibleArrangements.Add(_sb.ToString());
        }

        return possibleArrangements.ToArray();
    }

    public string UnFold(string line)
    {
        var sbStart = new StringBuilder();
        var sbEnd = new StringBuilder();
        for (var i = 0; i < 5; i++)
        {
            var split = line.Split(' ');
            sbStart.Append(split[0] + "?");
            sbEnd.Append(split[1] + ",");
        }

        var start = sbStart.ToString();
        var end = sbEnd.ToString();

        return start.TrimEnd('?') + " " + end.TrimEnd(',');
    }

    public long RunPart2(string[] input)
    {
        _logger.LogInformation("Solving - 2023 - Day 12 - Part 2");
        _logger.LogInformation("Input contains {Input} values", input.Length);

        long count = 0;
        foreach (string line in input)
        {
            var unfolded = UnFold(line);
            var possibleArrangements = GetPossibleArrangements(unfolded);
            // count += GetPossibleArrangementsCount(unfolded);

            count += possibleArrangements.Length;
            _logger.LogInformation("Line: {Line}, Possible arrangements: {PossibleArrangements}", unfolded, possibleArrangements.Length);
        }

        return count;
    }
}