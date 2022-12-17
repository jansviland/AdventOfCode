namespace AdventOfCode._2022.Day14;

public interface ISolutionService
{
    public int RunPart1(string[] input);
    public Grid ParseInput(string[] input);

    // TODO: Use this method to create an animation of sand filling the grid
    // TODO: for each "step" where sand moves, save the resuting output as a "frame" in a sequence of animations
    // TODO: then play the animation in the console
    public List<string> CreatePrintableOutput(Grid grid);
    public List<Grid> CreateSequence(Grid grid);
    public int RunPart2(string[] input);
    List<string?[]> ConvertToStrings(Grid grid);
}

public class Grid
{
    private int snowballCount = 0;

    public int XMin;
    public int XMax;

    public int YMin;
    public int YMax;

    public string?[,] values;
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
            // YMin = int.MaxValue,
            YMin = 0,
            YMax = int.MinValue,
        };

        var temp = new List<(int x, int y, string? value)>();

        for (var i = 0; i < input.Length; i++)
        {
            var coordinates = input[i].Split('-', StringSplitOptions.TrimEntries);

            // TODO: start at line position 0, then get coordinates for next point on the line,
            // compare to previous point, and move in a straight line until the next point is reached,
            // add X, Y coordinates along the way, to list, the temp list represents the line of rocks.
            // when done. Add X,Y of each rock to the grid.

            // sand starting point
            temp.Add((x: 500, y: 0, value: "+"));

            for (var linePosition = 0; linePosition < coordinates.Length; linePosition++)
            {
                var coordinate = coordinates[linePosition];
                var split = coordinate.Split(',', StringSplitOptions.TrimEntries);

                var x = int.Parse(split.First().Replace(">", ""));
                var y = int.Parse(split.Last().Replace(">", ""));

                // TODO: draw line of rocks between coordinates and add to grid
                temp.Add((x, y, "#"));

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
                    // result.YMin = y;
                }
                else if (y > result.YMax)
                {
                    result.YMax = y;
                }
            }
        }

        // TODO: add all the rocks to the grid from temp list

        // result.XMin -= 1;
        // result.XMax += 1;
        // result.YMin -= 1;
        // result.YMax += 1;

        result.values = new string[result.XMax - result.XMin + 1, result.YMax - result.YMin + 1];

        foreach (var t in temp)
        {
            result.values[t.x - result.XMin, t.y - result.YMin] = t.value;
        }

        // for (var x = result.XMin; x <= result.XMax; x++)
        // {
        //     for (var y = result.YMin; y <= result.YMax; y++)
        //     {
        //         result.values[x - result.XMin, y - result.YMin] = ".";
        //     }
        // }

        return result;
    }

    public List<string> CreatePrintableOutput(Grid grid)
    {
        throw new NotImplementedException();
    }

    public List<Grid> CreateSequence(Grid grid)
    {
        throw new NotImplementedException();
    }

    public int RunPart2(string[] input)
    {
        _logger.LogInformation("Solving day 14 - Part 2");
        _logger.LogInformation("Input contains {Input} values", input.Length);

        throw new NotImplementedException();
    }
 
    public List<string?[]> ConvertToStrings(Grid grid)
    {
        // get first row
        var width = grid.XMax - grid.XMin + 1;
        var heigth = grid.YMax - grid.YMin + 1;

        List<string?[]> rows = new();
        for (var y = 0; y < heigth; y++)
        {
            var row = new string?[width];
            for (var x = 0; x < width; x++)
            {
                row[x] = grid.values[x, y];
            }

            rows.Add(row);
        }

        return rows;
    }
}