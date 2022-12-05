namespace AdventOfCode._2022.Day4;

public interface ISolutionService
{
    public int RunPart1(string[] input);
    public bool StringsOverlap(string input);
    public int RunPart2(string[] input);
    public List<string> FindOverlappingStrings(string input);
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
        _logger.LogInformation("Solving day 4");
        _logger.LogInformation("Input contains {Input} values", input.Length);

        var sum = 0;
        for (int i = 0; i < input.Length; i++)
        {
            sum += StringsOverlap(input[i]) ? 1 : 0;
        }

        return sum;
    }

    public bool StringsOverlap(string input)
    {
        var split = input.Split(',');

        // find max length
        var part1MaxLength = int.Parse(split[0].Split("-").Last());
        var part2MaxLength = int.Parse(split[1].Split("-").Last());
        var maxLength = Math.Max(part1MaxLength, part2MaxLength);

        var part1 = ConvertToPrintableString(split[0], maxLength);
        var part2 = ConvertToPrintableString(split[1], maxLength);

        _logger.LogInformation("{Part1}   {Input}", string.Join("", part1), split[0]);
        _logger.LogInformation("{Part2}   {Input}", string.Join("", part2), split[1]);

        // check if all values in part1 are in part2
        var result = part1.All(part2.Contains) || part2.All(part1.Contains);

        _logger.LogInformation("Overlap: {Result}", result);

        return result;
    }

    private List<string> ConvertToPrintableString(string input, int length)
    {
        // TODO: find a better max value
        var result = new List<string>(new string[length + 1]);
        var split = input.Split('-');

        var a = int.Parse(split[0]);
        var b = int.Parse(split[1]);

        for (var i = 0; i < result.Count; i++)
        {
            if (i >= a && i <= b)
            {
                result[i] = i.ToString();
            }
            else
            {
                result[i] = ".";
            }
        }

        return result;
    }

    public int RunPart2(string[] input)
    {
        _logger.LogInformation("Solving day 4 - Part 2");
        _logger.LogInformation("Input contains {Input} values", input.Length);

        var sum = 0;
        foreach (var s in input)
        {
            sum += FindOverlappingStrings(s).Any() ? 1 : 0;
        }

        return sum;
    }

    public List<string> FindOverlappingStrings(string input)
    {
        var split = input.Split(',');

        // find max length
        var part1MaxLength = int.Parse(split[0].Split("-").Last());
        var part2MaxLength = int.Parse(split[1].Split("-").Last());
        var maxLength = Math.Max(part1MaxLength, part2MaxLength);

        var part1 = ConvertToPrintableString(split[0], maxLength);
        var part2 = ConvertToPrintableString(split[1], maxLength);

        _logger.LogInformation("{Part1}   {Input}", string.Join("", part1), split[0]);
        _logger.LogInformation("{Part2}   {Input}", string.Join("", part2), split[1]);

        var result = new List<string>();
        for (var i = 0; i < part1.Count; i++)
        {
            if (part1[i] != "." && part1[i] == part2[i])
            {
                result.Add(part1[i]);
            }
        }

        _logger.LogInformation("Overlap: {Result}", result);

        return result;
    }
}