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

    public override string ToString()
    {
        return $"Priority: {Priority}, WrongItem: {WrongItem}, Compartment1: {Compartment1}, Compartment2: {Compartment2}";
    }
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

        var mid = input.Length / 2;
        var part1 = input.Substring(0, mid);
        var part2 = input.Substring(mid);

        var result = new Rucksack()
        {
            Compartment1 = part1,
            Compartment2 = part2
        };

        // check if a character exist in both compartments
        foreach (var c in part1)
        {
            if (part2.Contains(c))
            {
                result.WrongItem = c;
                break;
            }
        }

        _logger.LogInformation(result.ToString());

        return result;
    }

    public int RunPart2(string[] input)
    {
        _logger.LogInformation("Solving day 3 - Part 2");
        _logger.LogInformation("Input contains {Input} values", input.Length);

        throw new NotImplementedException();
    }
}