namespace AdventOfCode._2023.Day19;

public interface ISolutionService
{
    public long RunPart1(string[] input);
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
        _logger.LogInformation("Solving - 2023 - Day 19 - Part 1");
        _logger.LogInformation("Input contains {Input} values", input.Length);

        throw new NotImplementedException();
    }

    public long RunPart2(string[] input)
    {
        _logger.LogInformation("Solving - 2023 - Day 19 - Part 2");
        _logger.LogInformation("Input contains {Input} values", input.Length);

        throw new NotImplementedException();
    }
}