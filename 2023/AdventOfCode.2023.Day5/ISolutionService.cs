namespace AdventOfCode._2023.Day5;

public interface ISolutionService
{
    public int RunPart1(string[] input);
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
        _logger.LogInformation("Solving - 2023 - Day 5 - Part 1");
        _logger.LogInformation("Input contains {Input} values", input.Length);

        var seedToSoilDictionary = new Dictionary<long, long>();
        
        // parse input
        long[] seeds = input[0].Split(':').Last().Split(' ').Select(long.Parse).ToArray();

        for (var i = 0; i < input.Length; i++)
        {
            if (input[i].StartsWith("seed-to-soil"))
            {
                
            }
        }
        

        throw new NotImplementedException();
    }

    public int RunPart2(string[] input)
    {
        _logger.LogInformation("Solving - 2023 - Day 5 - Part 2");
        _logger.LogInformation("Input contains {Input} values", input.Length);

        throw new NotImplementedException();
    }
}