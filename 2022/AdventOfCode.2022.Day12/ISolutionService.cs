namespace AdventOfCode._2022.Day12;

public interface ISolutionService
{
    public int RunPart1(string[] input);
    public void Print(string[] input);
    public Result FindPath(string[] input);
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

    /// <summary>
    /// Animate the input, so that it can be seen in the console
    ///
    /// Print the result, then move the cursor up and to the left.
    /// Then the next frame can be printed on top of the previous one.
    /// </summary>
    public void Print(string[] input)
    {
        throw new NotImplementedException();
    }

    public Result FindPath(string[] input)
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