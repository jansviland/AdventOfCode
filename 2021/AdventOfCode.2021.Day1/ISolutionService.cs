namespace AdventOfCode._2021.Day1;

public interface ISolutionService
{
    public int Run(string[] input);
    public int RunPart2(string[] input);
    public int GetSlidingWindow(string[] input, int index);
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

        var count = 0;
        var lastValue = int.Parse(input.First());
        for (var i = 1; i < input.Length; i++)
        {
            var currentValue = int.Parse(input[i]);

            if (currentValue > lastValue)
            {
                count++;
            }

            lastValue = currentValue;
        }

        return count;
    }

    public int GetSlidingWindow(string[] input, int index)
    {
        if (index < 2)
        {
            return -1;
        }

        // summarize the last 3 values
        return int.Parse(input[index - 2]) + int.Parse(input[index - 1]) + int.Parse(input[index]);
    }

    public int RunPart2(string[] input)
    {
        _logger.LogInformation("Solving day 1 part 2");
        _logger.LogInformation("Input contains {Input} values", input.Length);

        var count = 0;
        var lastValue = -1;
        for (var i = 0; i < input.Length; i++)
        {
            var currentValue = GetSlidingWindow(input, i);
            if (currentValue == -1) continue;

            if (currentValue > lastValue && lastValue != -1)
            {
                count++;
            }

            lastValue = currentValue;
        }

        return count;
    }
}