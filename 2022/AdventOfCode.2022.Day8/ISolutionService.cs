namespace AdventOfCode._2022.Day8;

public interface ISolutionService
{
    public int RunPart1(string[] input);
    public int[,] ParseInput(string[] input);
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
        _logger.LogInformation("Solving day 8");
        _logger.LogInformation("Input contains {Input} values", input.Length);

        var grid = ParseInput(input);


        throw new NotImplementedException();
    }

    public int[,] ParseInput(string[] input)
    {
        var result = new int[input.Length, input.Length];

        for (var x = 0; x < input.Length; x++)
        {
            for (var y = 0; y < input[x].Length; y++)
            {
                result[y, x] = int.Parse(input[x][y].ToString());
            }
        }

        return result;
    }

    public int RunPart2(string[] input)
    {
        _logger.LogInformation("Solving day 8 - Part 2");
        _logger.LogInformation("Input contains {Input} values", input.Length);

        throw new NotImplementedException();
    }
}