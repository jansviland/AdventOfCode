namespace AdventOfCode._2025.Day02;

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

    // https://en.wikipedia.org/wiki/Knuth–Morris–Pratt_algorith
    // https://www.geeksforgeeks.org/dsa/kmp-algorithm-for-pattern-searching/
    // KMP Algorithm
    // Create LPS table (Longest Prefix–Suffix table)
    public static List<int> computeLPSArray(string pattern)
    {
        int n = pattern.Length;
        List<int> lps = new int[n].ToList();

        // length of the previous longest prefix suffix
        int len = 0;
        int i = 1;

        while (i < n)
        {
            if (pattern[i] == pattern[len])
            {
                len++;
                lps[i] = len;
                i++;
            }
            else
            {
                if (len != 0)
                {
                    // fall back in the pattern
                    len = lps[len - 1];
                }
                else
                {
                    lps[i] = 0;
                    i++;
                }
            }
        }

        return lps;
    }

    bool IsValid(string str)
    {
        var part1 = str[..(str.Length / 2)];
        var part2 = str[(str.Length / 2)..];

        if (part1 == part2)
        {
            return false;
        }

        return true;
    }

    IEnumerable<long> FilterInvalid(IEnumerable<long[]> ranges)
    {
        foreach (long[] range in ranges)
        {
            for (long i = range[0]; i <= range[1]; i++)
            {
                var str = i.ToString();

                if (!IsValid(str))
                {
                    yield return i;
                }
            }
        }
    }
    
    IEnumerable<long> Repeates(IEnumerable<long[]> ranges)
    {
        foreach (long[] range in ranges)
        {
            for (long i = range[0]; i <= range[1]; i++)
            {
                var str = i.ToString();

                // Create
                // LPS table (Longest Prefix–Suffix table)
                // Example 
                // index: 0 1 2 3 4 5
                // char:  a b a b a b
                // lps:   0 0 1 2 3 4
                List<int> lps = computeLPSArray(str);

                // Decide if it repeats:
                int n = str.Length;     // from example: n = 6 
                int last = lps[n - 1];  // 4
                int period = n - last;  // 6 - 4 = 2

                bool repeats = last > 0 && n % period == 0; // 6 % 2 = 0, so it repeats
                
                if (repeats)
                {
                    yield return i;
                }
            }
        }
    }

    IEnumerable<long[]> Parse1(string[] input) =>
        from range in input[0].Split(',')
        let pair = range.Split('-', StringSplitOptions.RemoveEmptyEntries)
        let start = long.Parse(pair[0])
        let end = long.Parse(pair[1])
        select new[] { start, end };

    public long RunPart1(string[] input)
    {
        _logger.LogInformation("Solving - {Year} - Day {Day} - Part 1", _helper.GetYear(), _helper.GetDay());
        _logger.LogInformation("Input contains {Input} values", input.Length);

        return FilterInvalid(Parse1(input)).Sum();
    }

    public long RunPart2(string[] input)
    {
        _logger.LogInformation("Solving - {Year} - Day {Day} - Part 2", _helper.GetYear(), _helper.GetDay());
        _logger.LogInformation("Input contains {Input} values", input.Length);

        return Repeates(Parse1(input)).Sum();
    }

}
