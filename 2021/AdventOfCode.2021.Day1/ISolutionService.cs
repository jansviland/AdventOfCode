namespace AdventOfCode._2021.Day1;

public interface ISolutionService
{
    public int Run(string[] input);
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
        _logger.LogInformation("input contains {Input} values", input.Length);

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

    public int RunPart2(string[] input)
    {
        return 420;
    }
}