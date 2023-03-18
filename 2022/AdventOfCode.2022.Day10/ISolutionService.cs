namespace AdventOfCode._2022.Day10;

public interface ISolutionService
{
    public int[] GetRegisterXValuePerCycle(string[] input);
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

        for (var i = 0; i < input.Length; i++)
        {
            var command = input[i].Split(' ');

            var last = result.LastOrDefault() ?? 1;

            switch (command.First())
            {
                case "noop":
                    result.Add(last);
                    break;
                case "addx":
                {
                    result.Add(last);
                    result.Add(last);

                    var value = int.Parse(command.Last());
                    result.Add(last + value);
                    break;
                }
            }
        }

        return result.Select(x => x ?? 0).ToArray();
    }

    public int RunPart1(string[] input)
    {
        _logger.LogInformation("Solving day 10");
        _logger.LogInformation("Input contains {Input} values", input.Length);

        throw new NotImplementedException();
    }

    public int RunPart2(string[] input)
    {
        _logger.LogInformation("Solving day 10 - Part 2");
        _logger.LogInformation("Input contains {Input} values", input.Length);

        throw new NotImplementedException();
    }
}