namespace AdventOfCode._2022.Day1;

public interface ISolutionService
{
    public int Run(string[] input);

    /// <summary>
    /// key: elf
    /// value: sum of callories
    /// </summary>
    public Dictionary<int, int> GroupByElf(string[] input);

    public int RunPart2(string[] input);
}

public class SolutionService : ISolutionService
{
    private readonly ILogger<SolutionService> _logger;

    public SolutionService(ILogger<SolutionService> logger)
    {
        _logger = logger;
    }

    public int Run(string[] input)
    {
        _logger.LogInformation("Solving day 1");
        _logger.LogInformation("Input contains {Input} values", input.Length);

        var groupedByElf = GroupByElf(input);
        var max = groupedByElf.Values.Max();

        return max;
    }

    public Dictionary<int, int> GroupByElf(string[] input)
    {
        var result = new Dictionary<int, int>();

        var index = 0;
        var sum = 0;
        for (int i = 0; i < input.Length; i++)
        {
            if (input[i] == "")
            {
                // new group
                result.Add(index, sum);

                sum = 0;
                index++;
            }
            else
            {
                sum += int.Parse(input[i]);
            }

            if (i == input.Length - 1)
            {
                // last group
                result.Add(index, sum);
            }
        }

        return result;
    }

    public int RunPart2(string[] input)
    {
        _logger.LogInformation("Solving day 1 - Part 2");
        _logger.LogInformation("Input contains {Input} values", input.Length);

        var groupedByElf = GroupByElf(input);
        var orderedByValue = groupedByElf.OrderByDescending(x => x.Value);

        // sum of top 3
        var result = orderedByValue.Take(3).Sum(x => x.Value);

        return result;
    }
}