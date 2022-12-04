namespace AdventOfCode._2022.Day3;

public interface ISolutionService
{
    public int RunPart1(string[] input);
    public Rucksack ParseStringPart1(string input);
    public int GetPriority(char c);
    public int RunPart2(string[] input);
    public char GetCommonChar(string[] input);
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

        int count = 0;
        for (int i = 0; i < input.Length; i++)
        {
            var rucksack = ParseStringPart1(input[i]);
            count += rucksack.Priority;
        }

        return count;
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

        result.Priority = GetPriority(result.WrongItem);

        _logger.LogInformation(result.ToString());

        return result;
    }

    public int GetPriority(char c)
    {
        if (char.IsUpper(c))
        {
            return (int)c - 38;
        }
        else
        {
            return (int)c - 96;
        }
    }

    public int RunPart2(string[] input)
    {
        _logger.LogInformation("Solving day 3 - Part 2");
        _logger.LogInformation("Input contains {Input} values", input.Length);

        // group array by 3
        var grouped = input
            .Select((x, i) => new { Index = i, Value = x })
            .GroupBy(x => x.Index / 3)
            .Select(x => x.Select(v => v.Value).ToArray())
            .ToArray();

        var sum = 0;
        foreach (var g in grouped)
        {
            var commonChar = GetCommonChar(g);
            var priority = GetPriority(commonChar);

            sum += priority;
        }

        return sum;
    }

    public char GetCommonChar(string[] input)
    {
        var orderedByLength = input.OrderBy(x => x.Length).ToArray();

        for (var i = 0; i < orderedByLength[0].Length; i++)
        {
            var lookFor = orderedByLength[0][i];
            var existInArray2 = orderedByLength[1].Contains(lookFor);
            var existInArray3 = orderedByLength[2].Contains(lookFor);

            if (existInArray2 && existInArray3)
            {
                _logger.LogInformation("Found common char {Char}, in group: {X}, {Y}, {Z}",
                    lookFor, orderedByLength[0], orderedByLength[1], orderedByLength[2]);

                return lookFor;
            }
        }

        throw new Exception();
    }
}