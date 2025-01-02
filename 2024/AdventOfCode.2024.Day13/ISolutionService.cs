namespace AdventOfCode._2024.Day13;

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
        throw new NotImplementedException();
    }

    public long RunPart2(string[] input)
    {
        throw new NotImplementedException();
    }
}
