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

    private (int, int) GetCoordinates(string input)
    {
        var split = input.Split(',', StringSplitOptions.TrimEntries);

        var x = int.Parse(split.First().Replace(">", ""));
        var y = int.Parse(split.Last().Replace(">", ""));

        return (x, y);
    }

    public Grid ParseInput(string[] input)
    {
        var result = new Grid()
        {
            XMin = int.MaxValue,
            XMax = int.MinValue,
            YMin = 0,
            YMax = int.MinValue,
        };

        // find min and max values
        foreach (var i in input)
        {
            var coordinates = i.Split('-', StringSplitOptions.TrimEntries);
            for (var linePosition = 0; linePosition < coordinates.Length; linePosition++)
            {
                var point = GetCoordinates(coordinates[linePosition]);

                if (point.Item1 < result.XMin)
                {
                    result.XMin = point.Item1;
                }
                else if (point.Item1 > result.XMax)
                {
                    result.XMax = point.Item1;
                }

                if (point.Item2 < result.YMin)
                {
                    // result.YMin = point.Item2;
                }
                else if (point.Item2 > result.YMax)
                {
                    result.YMax = point.Item2;
                }
            }
        }

        // find all rocks and add to grid
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

            var previousPoint = GetCoordinates(coordinates[0]);
            temp.Add((x: previousPoint.Item1, y: previousPoint.Item2, value: "#"));

            for (var linePosition = 1; linePosition < coordinates.Length; linePosition++)
            {
                var point = GetCoordinates(coordinates[linePosition]);

                // TODO: draw line of rocks between coordinates and add to grid
                temp.Add((point.Item1, point.Item2, "#"));

                while (point.Item1 != previousPoint.Item1)
                {
                    if (point.Item1 > previousPoint.Item1)
                    {
                        previousPoint.Item1++;
                    }
                    else
                    {
                        previousPoint.Item1--;
                    }

                    temp.Add((previousPoint.Item1, previousPoint.Item2, "#"));
                }
            }
        }

        // add all items to grid
        result.values = new string[result.XMax - result.XMin + 1, result.YMax - result.YMin + 1];
        foreach (var coordinate in temp)
        {
            result.values[coordinate.x - result.XMin, coordinate.y - result.YMin] = coordinate.value;
        }

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