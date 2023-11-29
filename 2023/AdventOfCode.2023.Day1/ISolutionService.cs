using Algorithms;

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

        BinarySearch.FindIndex(input, "test");

        throw new NotImplementedException();

    }

    public int RunPart2(string[] input)
    {
        _logger.LogInformation("Solving - 2023 - day 1 - Part 2");
        _logger.LogInformation("Input contains {Input} values", input.Length);

        throw new NotImplementedException();
    }
}