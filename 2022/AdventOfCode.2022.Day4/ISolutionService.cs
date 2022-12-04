using System.Globalization;

namespace AdventOfCode._2022.Day4;

public interface ISolutionService
{
    public int RunPart1(string[] input);
    public bool StringsOverlap(string input);
    public int RunPart2(string[] input);
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

        throw new NotImplementedException();
    }

    public bool StringsOverlap(string input)
    {
        var part1 = ConvertToPrintableString(input.Substring(0, 3));
        var part2 = ConvertToPrintableString(input.Substring(4));

        _logger.LogInformation("{Part1}   {Input}", part1, input.Substring(0, 3));
        _logger.LogInformation("{Part2}   {Input}", part2, input.Substring(4));

        throw new NotImplementedException();
    }

    private string ConvertToPrintableString(string input)
    {
        var result = new char[8] { '.', '.', '.', '.', '.', '.', '.', '.' };

        var a = char.GetNumericValue(input[0]);
        var b = char.GetNumericValue(input[2]);

        for (var i = 1; i < 9; i++)
        {
            if (i >= a && i <= b)
            {
                result[i - 1] = (char)(i + 48);
            }
        }

        return new string(result);
    }

    public int RunPart2(string[] input)
    {
        _logger.LogInformation("Solving day 4 - Part 2");
        _logger.LogInformation("Input contains {Input} values", input.Length);

        throw new NotImplementedException();
    }
}