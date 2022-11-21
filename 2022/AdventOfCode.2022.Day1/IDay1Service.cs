namespace AdventOfCode._2022.Day1;

public interface IDay1Service
{
    public int Run(int input);
}

public class Day1Service : IDay1Service
{
    private readonly ILogger<Day1Service> _logger;

    public Day1Service(ILogger<Day1Service> logger)
    {
        _logger = logger;
    }

    public int Run(int input)
    {
        _logger.LogInformation("Solving day 1 with input: {Input}", input);

        return 420;
    }
}