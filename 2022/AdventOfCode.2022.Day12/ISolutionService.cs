using AdventOfCode._2022.Day12.Common.Models;

namespace AdventOfCode._2022.Day12;

public interface ISolutionService
{
    public int RunPart1(string[] input);
    public GridElement[,] ParseInput(string[] input);
    public LinkedList<Position> FindPath(string[] input); // each position from the start to the end
    public int RunPart2(string[] input);
}

public class Result
{
    public string[] Path { get; set; } = Array.Empty<string>();
    public int Steps { get; set; }
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
        _logger.LogInformation("Solving day 12");
        _logger.LogInformation("Input contains {Input} values", input.Length);

        throw new NotImplementedException();
    }

    public GridElement[,] ParseInput(string[] input)
    {
        throw new NotImplementedException();
    }

    public LinkedList<Position> FindPath(string[] input)
    {
        throw new NotImplementedException();
    }

    public int RunPart2(string[] input)
    {
        _logger.LogInformation("Solving day 12 - Part 2");
        _logger.LogInformation("Input contains {Input} values", input.Length);

        throw new NotImplementedException();
    }
}