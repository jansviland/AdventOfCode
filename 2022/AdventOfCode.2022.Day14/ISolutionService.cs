namespace AdventOfCode._2022.Day14;

public interface ISolutionService
{
    public int RunPart1(string[] input);
    public Frame ParseInput(string[] input);
    List<string?[]> CreatePrintableOutput(Frame frame);

    // TODO: Use this method to create an animation of sand filling the grid
    // TODO: for each "step" where sand moves, save the resuting output as a "frame" in a sequence of animations
    // TODO: then play the animation in the console
    public List<Frame> CreateSequence(Frame frame);
    public int RunPart2(string[] input);
}

public class Frame : ICloneable
{
    // for each movement of the sand, increase the step counter
    public int Step = 0;

    // for each "sand" that we add increase the counter, when sand has come to rest.
    // increase sand counter and start a new animation sequence, also set step to 0
    public int SandCount = 0;

    public int XMin;
    public int XMax;

    public int YMin;
    public int YMax;

    public string?[,] Grid;

    public object Clone()
    {
        var clone = new Frame
        {
            Step = Step,
            SandCount = SandCount,
            XMin = XMin,
            XMax = XMax,
            YMin = YMin,
            YMax = YMax,
            Grid = (string?[,])Grid.Clone()
        };

        return clone;
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

    private (int, int) GetCoordinates(string input)
    {
        var split = input.Split(',', StringSplitOptions.TrimEntries);

        var x = int.Parse(split.First().Replace(">", ""));
        var y = int.Parse(split.Last().Replace(">", ""));

        return (x, y);
    }

    public Frame ParseInput(string[] input)
    {
        var result = new Frame()
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

                while (point.Item2 != previousPoint.Item2)
                {
                    if (point.Item2 > previousPoint.Item2)
                    {
                        previousPoint.Item2++;
                    }
                    else
                    {
                        previousPoint.Item2--;
                    }

                    temp.Add((previousPoint.Item1, previousPoint.Item2, "#"));
                }

                previousPoint = point;
            }
        }

        // add all items to grid
        result.Grid = new string[result.XMax - result.XMin + 1, result.YMax - result.YMin + 1];
        foreach (var coordinate in temp)
        {
            result.Grid[coordinate.x - result.XMin, coordinate.y - result.YMin] = coordinate.value;
        }

        // print result
        // CreatePrintableOutput(result);

        return result;
    }

    public List<Frame> CreateSequence(Frame frame)
    {
        var step = 0;
        // var sandCount = 0;
        var position = (500, 0);

        var result = new List<Frame>();

        var x = position.Item1 - frame.XMin;
        var y = position.Item2 - frame.YMin;

        while (frame.Grid[x, y] != "#")
        {
            var newFrame = (Frame)frame.Clone();
            newFrame.Step = step;
            // newFrame.SandCount = sandCount++;

            // set previous position to empty
            if (y > 0 && newFrame.Grid[x, y - 1] != "+")
            {
                newFrame.Grid[x, y - 1] = null;
            }

            if (y > 0)
            {
                newFrame.Grid[x, y] = "o";
            }

            // set new position to sand

            // set previous position to empty
            // newFrame.Grid[x, y] = null;

            // move sand down as long as there is no rock in the way
            y++;
            step++;

            result.Add(newFrame);

            // CreatePrintableOutput(newFrame);
        }

        return result;
    }

    public int RunPart2(string[] input)
    {
        _logger.LogInformation("Solving day 14 - Part 2");
        _logger.LogInformation("Input contains {Input} values", input.Length);

        throw new NotImplementedException();
    }

    public List<string?[]> CreatePrintableOutput(Frame frame)
    {
        var width = frame.XMax - frame.XMin + 1;
        var heigth = frame.YMax - frame.YMin + 1;

        List<string?[]> rows = new();
        for (var y = 0; y < heigth; y++)
        {
            var row = new string?[width];
            for (var x = 0; x < width; x++)
            {
                row[x] = frame.Grid[x, y];
            }

            rows.Add(row);
        }

        _logger.LogInformation("--------------------");
        _logger.LogInformation("Step {0}, sand {1} -----", frame.Step, frame.SandCount);
        _logger.LogInformation("--------------------");

        foreach (var line in rows)
        {
            var sb = new StringBuilder();
            foreach (var c in line)
            {
                if (c == null)
                {
                    sb.Append('.');
                }
                else
                {
                    sb.Append(c);
                }

                sb.Append(' ');
            }

            _logger.LogInformation(sb.ToString());
        }

        return rows;
    }
}