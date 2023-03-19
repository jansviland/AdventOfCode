namespace AdventOfCode._2022.Day10;

public interface ISolutionService
{
    public int[] GetRegisterXValuePerCycle(string[] input);
    public string[] GetCrtOutput(string[] input);
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

    public string[] GetCrtOutput(string[] input)
    {
        var xValues = GetRegisterXValuePerCycle(input);

        var result = new List<string>();
        for (var i = 1; i < xValues.Length; i++)
        {
            // X register sets the horizontal position of the middle of that sprite
            // if x is one less, equal or one more than the current cycle, the sprite is visible
            var x = xValues[i];
            var y = (i - 1) % 40;

            var visible = x == y - 1 || x == y || x == y + 1;

            result.Add(visible ? "#" : ".");
        }

        // The CRT is 40 pixels wide and 6 pixels tall.
        // convert to 6 lines of 40 characters
        var output = new List<string>();
        for (var i = 0; i < 6; i++)
        {
            var line = string.Join("", result.Skip(i * 40).Take(40));
            output.Add(line);
        }

        return output.ToArray();
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

        var output = GetCrtOutput(input);

        foreach (var line in output)
        {
            _logger.LogInformation(line);
        }

        return 0;
    }
}