namespace AdventOfCode._2022.Day5;

public interface ISolutionService
{
    public string RunPart1(string[] input);
    public int RunPart2(string[] input);
    public List<Stack<Crate>> ParseInput(string[] input);
}

public class Crate
{
    public string Name { get; set; }
}

public class SolutionService : ISolutionService
{
    private readonly ILogger<SolutionService> _logger;

    public SolutionService(ILogger<SolutionService> logger)
    {
        _logger = logger;
    }

    public string RunPart1(string[] input)
    {
        _logger.LogInformation("Solving day 4");
        _logger.LogInformation("Input contains {Input} values", input.Length);

        throw new NotImplementedException();
    }

    public int RunPart2(string[] input)
    {
        _logger.LogInformation("Solving day 4 - Part 2");
        _logger.LogInformation("Input contains {Input} values", input.Length);

        throw new NotImplementedException();
    }

    public List<Stack<Crate>> ParseInput(string[] input)
    {
        throw new NotImplementedException();
    }
}