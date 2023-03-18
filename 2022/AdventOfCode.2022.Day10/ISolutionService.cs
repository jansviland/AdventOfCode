namespace AdventOfCode._2022.Day10;

public interface ISolutionService
{
    public int[] GetRegisterXValuePerCycle(string[] input);
    public string[] GetCrtOutput(int[] input);
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

    /// <summary>
    /// index 0 = cycle 0 (always 1)
    /// index 1 = cycle 1
    /// Value = register X value
    /// </summary>
    public int[] GetRegisterXValuePerCycle(string[] input)
    {
        var result = new List<int?>();

        var currentValue = 1;
        result.Add(currentValue);

        for (var i = 0; i < input.Length; i++)
        {
            var command = input[i].Split(' ');
            switch (command.First())
            {
                case "noop":
                    result.Add(currentValue);
                    break;
                case "addx":
                {
                    result.Add(currentValue);
                    result.Add(currentValue);

                    var value = int.Parse(command.Last());
                    currentValue += value;

                    break;
                }
            }
        }

        result.Add(currentValue);

        return result.Select(x => x ?? 0).ToArray();
    }

    public string[] GetCrtOutput(int[] input)
    {
        throw new NotImplementedException();
    }

    public int RunPart1(string[] input)
    {
        _logger.LogInformation("Solving day 10");
        _logger.LogInformation("Input contains {Input} values", input.Length);

        var result = GetRegisterXValuePerCycle(input);

        // Find the signal strength during the 20th, 60th, 100th, 140th, 180th, and 220th cycles.
        // What is the sum of these six signal strengths?
        return 20 * result[20] + 60 * result[60] + 100 * result[100] + 140 * result[140] + 180 * result[180] + 220 * result[220];
    }

    public int RunPart2(string[] input)
    {
        _logger.LogInformation("Solving day 10 - Part 2");
        _logger.LogInformation("Input contains {Input} values", input.Length);

        throw new NotImplementedException();
    }
}