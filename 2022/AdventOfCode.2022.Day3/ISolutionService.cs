namespace AdventOfCode._2022.Day3;

public interface ISolutionService
{
    public int RunPart1(string[] input);
    public Rucksack ParseStringPart1(string input);
    public int RunPart2(string[] input);
}

public class Rucksack
{
    public int Priority { get; set; }
    public char WrongItem { get; set; }
    public string Compartment1 { get; set; }
    public string Compartment2 { get; set; }
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
        _logger.LogInformation("Solving day 3");
        _logger.LogInformation("Input contains {Input} values", input.Length);

        throw new NotImplementedException();
    }

    public Rucksack ParseStringPart1(string input)
    {
        _logger.LogInformation("Parsing {Input}", input);

        throw new NotImplementedException();
    }

    public int RunPart2(string[] input)
    {
        _logger.LogInformation("Solving day 3 - Part 2");
        _logger.LogInformation("Input contains {Input} values", input.Length);

        throw new NotImplementedException();
    }
}