namespace AdventOfCode._2022.Day6;

public interface ISolutionService
{
    public int RunPart1(string input);
    public int RunPart2(string[] input);
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

        for (var i = 0; i < input.Length; i++)
        {
            var subString = input.Substring(i, 4);
            if (subString.Distinct().Count() == 4)
            {
                _logger.LogInformation("Found uniqe values in {Substring}, in {String}, at {Index}", subString, input, i);
                return i + 4;
            }
        }

        throw new Exception();
    }

    public int RunPart2(string[] input)
    {
        _logger.LogInformation("Solving day 6 - Part 2");
        _logger.LogInformation("Input contains {Input} values", input.Length);

        throw new NotImplementedException();
    }
}