namespace AdventOfCode._2023.Day12;

public interface ISolutionService
{
    public long RunPart1(string[] input);
    public string[] GetPossibleArrangements(string line);
    public long RunPart2(string[] input);
}

public class SolutionService : ISolutionService
{
    private readonly ILogger<ISolutionService> _logger;

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

    // generate all combinations of # and . for any number of wildcards
    public List<string> GenerateCombinations(string current, int length)
    {
        if (current.Length == length)
        {
            return new List<string> { current };
        }

        var combinations = new List<string>();

        foreach (var c in new[] { '#', '.' })
        {
            combinations.AddRange(GenerateCombinations(current + c, length));
        }

        return combinations;
    }

    public string[] GetPossibleArrangements(string line)
    {
        // bruteforce?
        // replace all ? # or ., then see if it matches the pattern
        var split = line.Split(' ');
        var pattern = split[0];
        var groups = split[1].Split(',').Select(int.Parse).ToArray();

        // check if the pattern matches the groups

        // ???.### 1,1,3 - 1 arrangement

        int numberOfWildcards = pattern.Count(c => c == '?');
        var combinations = GenerateCombinations("", numberOfWildcards);

        var sb = new StringBuilder();

        var possibleArrangements = new List<string>();
        foreach (var combination in combinations)
        {
            sb.Clear();

            var count = 0;
            for (var i = 0; i < pattern.Length; i++)
            {
                if (pattern[i] == '?')
                {
                    sb.Append(combination[count]);
                    count++;
                }
                else
                {
                    sb.Append(pattern[i]);
                }

            }

            // check if the pattern matches the groups
            // split by ., then count the length of each group, so #.#.### becomes #, #, ###, and then we count each one, so 1,1,3
            var currentGroups = sb.ToString()
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

            possibleArrangements.Add(sb.ToString());
        }

        // possible arrangements:
        // #.#.### 1,1,3 - 1 arrangement

        // .??..??...?##. 1,1,3 - 4 arrangements

        // possible arrangements:

        // .#...#....###. 1,1,3 - 4 arrangements
        // .#....#...###. 1,1,3 - 4 arrangements
        // ..#..#....###. 1,1,3 - 4 arrangements
        // ..#...#...###. 1,1,3 - 4 arrangements

        // ?#?#?#?#?#?#?#? 1,3,1,6 - 1 arrangement
        // ????.#...#... 4,1,1 - 1 arrangement
        // ????.######..#####. 1,6,5 - 4 arrangements
        // ?###???????? 3,2,1 - 10 arrangements


        return possibleArrangements.ToArray();
    }

    public long RunPart2(string[] input)
    {
        _logger.LogInformation("Solving - 2023 - Day 12 - Part 2");
        _logger.LogInformation("Input contains {Input} values", input.Length);

        throw new NotImplementedException();
    }
}