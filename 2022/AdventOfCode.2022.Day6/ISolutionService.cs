namespace AdventOfCode._2022.Day6;

public interface ISolutionService
{
    public int RunPart1(string input);
    public int RunPart2(string input);
}

public class SolutionService : ISolutionService
{
    private readonly ILogger<SolutionService> _logger;

    public SolutionService(ILogger<SolutionService> logger)
    {
        _logger = logger;
    }

    public int RunPart1(string input)
    {
        _logger.LogInformation("Solving day 6");
        _logger.LogInformation("Input contains {Input} values", input.Length);

        return FindIndexAfterNumberOfDistinctValues(input, 4);
    }

    public int RunPart2(string input)
    {
        _logger.LogInformation("Solving day 6 - Part 2");
        _logger.LogInformation("Input contains {Input} values", input.Length);

        return FindIndexAfterNumberOfDistinctValues(input, 14);
    }

    private int FindIndexAfterNumberOfDistinctValues(string input, int numberOfDistinctValues)
    {
        for (var i = 0; i < input.Length; i++)
        {
            if (i + numberOfDistinctValues >= input.Length)
            {
                continue;
            }

            var subString = input.Substring(i, numberOfDistinctValues);
            if (subString.Distinct().Count() == numberOfDistinctValues)
            {
                _logger.LogInformation("Found uniqe values in {Substring}, in {String}, at {Index}", subString, input, i);
                return i + numberOfDistinctValues;
            }
        }

        throw new Exception();
    }
}