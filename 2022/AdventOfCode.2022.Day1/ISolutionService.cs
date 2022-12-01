namespace AdventOfCode._2022.Day1;

public interface ISolutionService
{
    public int Run(string[] input);
    
    /// <summary>
    /// key: elf
    /// value: sum of callories
    /// </summary>
    public Dictionary<int, int> GroupByElf(string[] input);
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
        _logger.LogInformation("Solving day 1 with input: {Input}", input);

        return 420;
    }

    public Dictionary<int, int> GroupByElf(string[] input)
    {
        throw new NotImplementedException();
    }
}