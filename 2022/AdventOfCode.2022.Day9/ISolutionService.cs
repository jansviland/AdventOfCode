namespace AdventOfCode._2022.Day9;

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
        _logger.LogInformation("Solving day 9");
        _logger.LogInformation("Input contains {Input} values", input.Length);

        throw new NotImplementedException();
    }

    public int RunPart2(string[] input)
    {
        _logger.LogInformation("Solving day 9 - Part 2");
        _logger.LogInformation("Input contains {Input} values", input.Length);

        throw new NotImplementedException();
    }
}