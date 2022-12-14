namespace AdventOfCode._2022.Day14;

public interface ISolutionService
{
    public int RunPart1(string[] input);
    public Grid ParseInput(string[] input);

    // TODO: Use this method to create an animation of sand filling the grid
    // TODO: for each "step" where sand moves, save the resuting output as a "frame" in a sequence of animations
    // TODO: then play the animation in the console
    public List<string> CreatePrintableOutput(Grid grid);
    public int RunPart2(string[] input);
}

public class Grid
{
    public int XMin;
    public int XMax;

    public int YMin;
    public int YMax;

    public string[,] values;

    public Grid()
    {
    }
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
        _logger.LogInformation("Solving day 14");
        _logger.LogInformation("Input contains {Input} values", input.Length);

        throw new NotImplementedException();
    }

    public Grid ParseInput(string[] input)
    {
        var result = new Grid()
        {
            XMin = int.MaxValue,
            XMax = int.MinValue,
            YMin = int.MaxValue,
            YMax = int.MinValue,
        };

        var temp = new List<(int x, int y, string value)>();

        for (var i = 0; i < input.Length; i++)
        {
            var coordinates = input[i].Split('-', StringSplitOptions.TrimEntries);

            // TODO: start at line position 0, then get coordinates for next point on the line, 
            // compare to previous point, and move in a straight line until the next point is reached,
            // add X, Y coordinates along the way, to list, the temp list represents the line of rocks.
            // when done. Add X,Y of each rock to the grid.

            for (var linePosition = 0; linePosition < coordinates.Length; linePosition++)
            {
                var coordinate = coordinates[linePosition];
                var split = coordinate.Split(',', StringSplitOptions.TrimEntries);

                var x = int.Parse(split.First().Replace(">", ""));
                var y = int.Parse(split.Last().Replace(">", ""));

                // TODO: draw line of rocks between coordinates and add to grid

                if (x < result.XMin)
                {
                    result.XMin = x;
                }
                else if (x > result.XMax)
                {
                    result.XMax = x;
                }

                if (y < result.YMin)
                {
                    result.YMin = y;
                }
                else if (y > result.YMax)
                {
                    result.YMax = y;
                }
            }
        }

        return result;
    }

    public List<string> CreatePrintableOutput(Grid grid)
    {
        throw new NotImplementedException();
    }

    public int RunPart2(string[] input)
    {
        _logger.LogInformation("Solving day 14 - Part 2");
        _logger.LogInformation("Input contains {Input} values", input.Length);

        throw new NotImplementedException();
    }
}