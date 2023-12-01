namespace AdventOfCode._2023.Day1;

public interface ISolutionService
{
    public int RunPart1(string[] input);
    public int RunPart2(string[] input);
}

public class SolutionService : ISolutionService
{
    private readonly ILogger<SolutionService> _logger;

    public SolutionService(ILogger<SolutionService> logger)
    {
        _logger = logger;
    }

    public int RunPart1(string[] input)
    {
        _logger.LogInformation("Solving - 2023 - Day 1 - Part 1");
        _logger.LogInformation("Input contains {Input} values", input.Length);

        var sum = 0;
        foreach (var line in input)
        {
            Char? first = null;
            Char? last = null;
            for (var i = 0; i < line.Length; i++)
            {
                Char c = line[i];
                if (c >= '0' && c <= '9')
                {
                    if (first == null)
                    {
                        first = c;
                        last = c;
                    }
                    else
                    {
                        last = c;
                    }
                }
            }


            sum += int.Parse(first + "" + last);
        }

        return sum;
    }

    // TODO: can be improved by skipping ahead when we find a word match, when we find "five", we can skip ahead 4 characters. Now we are checking the same characters multiple times.
    private static char? LookForMatch(string line, int index)
    {
        var c = line[index];
        if (c >= '0' && c <= '9')
        {
            return c;
        }

        var wordDictionary = new Dictionary<string, char> { { "one", '1' }, { "two", '2' }, { "three", '3' }, { "four", '4' }, { "five", '5' }, { "six", '6' }, { "seven", '7' }, { "eight", '8' }, { "nine", '9' } };

        var subString = line.Substring(index);
        foreach (var word in wordDictionary.Keys)
        {
            if (subString.StartsWith(word))
            {
                return wordDictionary[word];
            }
        }

        return null;
    }

    public int RunPart2(string[] input)
    {
        _logger.LogInformation("Solving - 2023 - day 1 - Part 2");
        _logger.LogInformation("Input contains {Input} values", input.Length);

        var sum = 0;
        foreach (var line in input)
        {
            Char? first = null;
            Char? last = null;
            for (var i = 0; i < line.Length; i++)
            {
                Char? c = LookForMatch(line, i);
                if (c == null)
                {
                    continue;
                }

                if (first == null)
                {
                    first = c;
                    last = c;
                }
                else
                {
                    last = c;
                }
            }


            sum += int.Parse(first + "" + last);
        }

        return sum;
    }
}