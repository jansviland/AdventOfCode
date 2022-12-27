namespace AdventOfCode._2022.Day14;

public interface ISolutionService
{
    public int RunPart1(string[] input);
    public Frame ParseInput(string[] input);
    public Frame ParseInputPart2(string[] input);
    List<string?[]> CreatePrintableOutput(Frame frame);
    public List<Frame> CreateSequence(Frame frame, bool showallsteps = true);

    /// <summary>
    /// TODO: add a debug int option:
    /// 0 = only final result
    /// 1 = show only every new sand added
    /// 2 = show all steps
    /// </summary>
    /// <returns>final frame</returns>
    public List<Frame> CreateSequencePart2(Frame frame, int debugLevel = 0); // TODO: add option to not print at all, only show final result

    public void PrintFrame(Frame frame);
    public int RunPart2(string[] input);
}

public class Frame : ICloneable
{
    // for each "sand" that we add increase the counter, when sand has come to rest.
    // increase sand counter and start a new animation sequence, also set step to 0
    public int SandCount = 0;

    public int XMin;
    public int XMax;

    public int YMin;
    public int YMax;

    public int SandX;
    public int SandY;

    public string?[,] Grid;

    public object Clone()
    {
        var clone = new Frame
        {
            SandCount = SandCount,
            XMin = XMin,
            XMax = XMax,
            YMin = YMin,
            YMax = YMax,
            SandX = SandX,
            SandY = SandY,
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

        var startGrid = ParseInput(input);
        var frames = CreateSequence(startGrid, false);

        return frames.Last().SandCount;
    }

    private static (int, int) GetCoordinates(string input)
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
            SandX = 0,
            SandY = 0,
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

        return result;
    }

    public Frame ParseInputPart2(string[] input)
    {
        var grid = ParseInput(input);

        var inputWithAddedRocks = input.ToList();
        var newLine = $"{grid.XMin},{grid.YMax + 2} -> {grid.XMax},{grid.YMax + 2}";

        inputWithAddedRocks.Add(newLine);

        return ParseInput(inputWithAddedRocks.ToArray());
    }

    public List<Frame> CreateSequence(Frame frame, bool showallsteps = true)
    {
        // start point
        var position = (500, 0);

        var result = new List<Frame>();

        var x = position.Item1 - frame.XMin;
        var y = position.Item2 - frame.YMin;

        var lastFrame = (Frame)frame.Clone();
        result.Add(lastFrame);

        while (y < frame.YMax && x > 0)
        {
            // copy last frame and modify it
            var newFrame = (Frame)lastFrame.Clone();

            var tempX = x;
            var tempY = y;

            // move sand down as long as there is no rock in the way
            if (newFrame.Grid[x, y + 1] == null)
            {
                y++;
            }
            else if (newFrame.Grid[x - 1, y + 1] == null)
            {
                y++;
                x--;
            }
            else if (newFrame.Grid[x + 1, y + 1] == null)
            {
                y++;
                x++;
            }
            else
            {
                // reset
                x = position.Item1 - frame.XMin;
                y = position.Item2 - frame.YMin;

                newFrame.SandCount++;
            }

            // set previous position to empty
            if (y > 0 && newFrame.Grid[x, y - 1] != "+")
            {
                newFrame.Grid[tempX, tempY] = null;
            }

            if (y > 0)
            {
                newFrame.Grid[x, y] = "o";
            }

            newFrame.SandX = x;
            newFrame.SandY = y;

            if (showallsteps)
            {
                result.Add(newFrame);
            }
            else
            {
                if (newFrame.SandCount > lastFrame.SandCount)
                {
                    result.Add(newFrame);
                }
            }

            lastFrame = newFrame;
        }

        return result;
    }

    private static Frame IncreaseGridSize(Frame frame, bool shiftRight)
    {
        var newGrid = new string?[frame.Grid.GetLength(0) + 1, frame.Grid.GetLength(1)];

        for (var y = 0; y < frame.Grid.GetLength(1); y++)
        {
            for (var x = 0; x < frame.Grid.GetLength(0); x++)
            {
                if (shiftRight)
                {
                    newGrid[x + 1, y] = frame.Grid[x, y];
                }
                else
                {
                    newGrid[x, y] = frame.Grid[x, y];
                }
            }
        }

        if (shiftRight)
        {
            newGrid[0, newGrid.GetLength(1) - 1] = "#";
            frame.XMin--;
        }
        else
        {
            newGrid[newGrid.GetLength(0) - 1, newGrid.GetLength(1) - 1] = "#";
            frame.XMax++;
        }

        frame.Grid = newGrid;

        return frame;
    }

    /// <returns>Returns final frame of sequence</returns>
    public List<Frame> CreateSequencePart2(Frame frame, int debugLevel)
    {
        // start point
        var position = (500, 0);

        var result = new List<Frame>();

        var x = position.Item1 - frame.XMin;
        var y = position.Item2 - frame.YMin;

        var previousFrame = (Frame)frame.Clone();
        previousFrame.SandCount = 1;

        while (y < frame.YMax)
        {
            var newFrame = (Frame)previousFrame.Clone();

            var tempX = x;
            var tempY = y;

            // if moving to the left is out of bounds, increase size of grid and add rock to bottom
            if (x == 0)
            {
                newFrame = IncreaseGridSize(newFrame, true);
                newFrame = IncreaseGridSize(newFrame, true);
                x++;
            }
            else if (newFrame.Grid.GetLength(0) - 1 == x)
            {
                newFrame = IncreaseGridSize(newFrame, false);
            }

            // move down, left or right
            if (newFrame.Grid[x, y + 1] == null)
            {
                y++;
            }
            else if (newFrame.Grid[x - 1, y + 1] == null)
            {
                x--;
                y++;
            }
            else if (newFrame.Grid[x + 1, y + 1] == null)
            {
                x++;
                y++;
            }
            // can't move down, left or right, so it's the final sand
            else if (newFrame.Grid[x, y + 1] != null && newFrame.Grid[x - 1, y + 1] != null && newFrame.Grid[x + 1, y + 1] != null && y == 0)
            {
                newFrame.Grid[x, y] = "o";
                newFrame.SandCount++;

                result.Add(newFrame);

                return result;
            }
            else
            {
                // reset
                x = position.Item1 - newFrame.XMin;
                y = position.Item2 - newFrame.YMin;

                newFrame.SandCount++;
            }

            // set previous position to empty
            if (y > 0 && newFrame.Grid[tempX, tempY] != "+")
            {
                newFrame.Grid[tempX, tempY] = null;
            }

            if (y > 0)
            {
                newFrame.Grid[x, y] = "o";
            }

            newFrame.SandX = x;
            newFrame.SandY = y;

            if (debugLevel == 1)
            {
                if (newFrame.SandCount > previousFrame.SandCount)
                {
                    // result.Add(newFrame);
                    PrintFrame(newFrame);
                }
            }
            else if (debugLevel == 2)
            {
                // result.Add(newFrame);
                PrintFrame(newFrame);
            }
            else if (debugLevel == 3)
            {
                // print every 1000 sand
                if (newFrame.SandCount % 1009 == 0)
                {
                    // result.Add(newFrame);
                    PrintFrame(newFrame);
                }
            }

            previousFrame = newFrame;
        }

        _logger.LogInformation("Total sand count: {SandCount}", previousFrame.SandCount);

        result.Add(previousFrame);

        return result;
    }

    public int RunPart2(string[] input)
    {
        _logger.LogInformation("Solving day 14 - Part 2");
        _logger.LogInformation("Input contains {Input} values", input.Length);

        var startGrid = ParseInputPart2(input);
        var frames = CreateSequencePart2(startGrid, 0);

        return frames.Last().SandCount;
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
        }

        return rows;
    }

    public void PrintFrame(Frame frame)
    {
        StringBuilder sb = new();

        // print to console
        Console.WriteLine("Sand count: {0}, Sand position: ({1}, {2}), YMax: {3}    ",
            frame.SandCount, frame.SandX, frame.SandY, frame.YMax);

        Console.WriteLine("");

        for (var y = 0; y < frame.Grid.GetLength(1); y++)
        {
            for (var x = 0; x < frame.Grid.GetLength(0); x++)
            {
                if (string.IsNullOrEmpty(frame.Grid[x, y]))
                {
                    sb.Append(".");
                }
                else
                {
                    sb.Append(frame.Grid[x, y]);
                }

                if (x == frame.Grid.GetLength(0) - 1)
                {
                    sb.Append("   " + y);
                }
            }

            // add new line
            sb.Append(Environment.NewLine);
        }

        Console.Write(sb.ToString());
        sb.Clear();

        Thread.Sleep(30);
        Console.SetCursorPosition(0, Console.CursorTop - frame.Grid.GetLength(1) - 2);
    }
}