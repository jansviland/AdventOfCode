namespace AdventOfCode._2023.Day2;

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

    public static (int, int, int) GetMaxCubes(string game)
    {
        var red = 0;
        var green = 0;
        var blue = 0;

        var split = game.Split(':');
        var cubes = split[1].Trim().Split(';');

        foreach (var cube in cubes)
        {
            var sets = cube.Trim().Split(',');

            foreach (var colors in sets)
            {
                var splitColors = colors.Trim().Split(' ');
                var count = int.Parse(splitColors[0]);
                var color = splitColors[1];

                switch (color)
                {
                    case "red":
                        red = Math.Max(red, count);
                        break;
                    case "green":
                        green = Math.Max(green, count);
                        break;
                    case "blue":
                        blue = Math.Max(blue, count);
                        break;
                }
            }
        }

        return (red, green, blue);
    }

    public int RunPart1(string[] input)
    {
        _logger.LogInformation("Solving - 2023 - Day 2 - Part 1");
        _logger.LogInformation("Input contains {Input} values", input.Length);

        const int maxRedCubes = 12;
        const int maxGreenCubes = 13;
        const int maxBlueCubes = 14;

        var id = 1;
        var count = 0;
        foreach (var line in input)
        {
            var (red, green, blue) = GetMaxCubes(line);
            if (red <= maxRedCubes && green <= maxGreenCubes && blue <= maxBlueCubes)
            {
                count += id;
            }

            id++;
        }

        return count;
    }

    public int RunPart2(string[] input)
    {
        _logger.LogInformation("Solving - 2023 - Day 2 - Part 2");
        _logger.LogInformation("Input contains {Input} values", input.Length);

        var count = 0;
        foreach (var line in input)
        {
            var (red, green, blue) = GetMaxCubes(line);
            var product = red * green * blue;

            count += product;
        }

        return count;
    }
}