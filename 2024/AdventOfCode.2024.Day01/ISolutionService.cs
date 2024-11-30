namespace AdventOfCode._2024.Day01;

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
        _logger.LogInformation("Solving - {Year} - Day {Day} - Part 1", GetYear(), GetDay());
        _logger.LogInformation("Input contains {Input} values", input.Length);

        throw new NotImplementedException();
    }

    public long RunPart2(string[] input)
    {
        _logger.LogInformation("Solving - {Year} - Day {Day} - Part 2", GetYear(), GetDay());
        _logger.LogInformation("Input contains {Input} values", input.Length);

        throw new NotImplementedException();
    }
    
    private int GetYear()
    {
        var ns = GetType().Namespace;
        return int.Parse(ns?.Split('.').ElementAt(1).Replace("_", "")!);
    }
    
    private int GetDay()
    {
        var ns = GetType().Namespace;
        return int.Parse(ns?.Split('.').Last().Replace("Day", "")!);
    }
}