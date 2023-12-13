namespace AdventOfCode._2023.Day13;

public interface ISolutionService
{
    public long RunPart1(string[] input);
    public long RunPart2(string[] input);
}

public class SolutionService : ISolutionService
{
    private readonly ILogger<ISolutionService> _logger;

    public SolutionService(ILogger<SolutionService> logger)
    {
        _logger = logger;
    }

    public char[,] Parse(string[] input)
    {
        var result = new char[input.Length, input[0].Length];
        for (var y = 0; y < input.Length; y++)
        {
            for (var x = 0; x < input[0].Length; x++)
            {
                result[y, x] = input[y][x];
            }
        }

        return result;
    }

    public string[] Transpose(string[] input)
    {
        // var rowMax = input.Length;
        // var colMax = input[0].Length;

        var result = new string[input[0].Length];
        for (var y = 0; y < input.Length; y++)
        {
            for (var x = 0; x < input[0].Length; x++)
            {
                result[x] += input[y][x];
            }
        }

        return result;
    }

    public int GetMirrorReflectionIndex(string[] mirror)
    {
        // start at 1, because we need to compare with left and right
        var mirrorPosition = 1;

        for (var y = 0; y < mirror.Length; y++)
        {
            var line = mirror[y];

            var checkLeftAndRight = true;
            while (checkLeftAndRight)
            {
                var compareLeftIndex = mirrorPosition - 1;
                var compareRightIndex = mirrorPosition;

                while (compareLeftIndex >= 0 && compareRightIndex < line.Length)
                {
                    var left = line[compareLeftIndex];
                    var right = line[compareRightIndex];

                    // found a position along the x line that is not a mirror reflection
                    if (right != left)
                    {
                        mirrorPosition++;
                        break;
                    }

                    compareLeftIndex--;
                    compareRightIndex++;
                }

                checkLeftAndRight = false;
            }
        }

        // only return the position if it exist
        if (mirrorPosition == mirror[0].Length)
        {
            return 0;
        }

        return mirrorPosition;
    }

    public long RunPart1(string[] input)
    {
        _logger.LogInformation("Solving - 2023 - Day 13 - Part 1");
        _logger.LogInformation("Input contains {Input} values", input.Length);

        // https://www.reddit.com/r/adventofcode/comments/18hbc6k/2023_day_13_smoothly_animated_visualization/

        var verticalMirrors = new List<string[]>();
        var horizontalMirrors = new List<string[]>();

        var verticalMirror = true;
        var currentMirror = new List<string>();
        foreach (string line in input)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                if (verticalMirror)
                {
                    verticalMirrors.Add(currentMirror.ToArray());
                }
                else
                {
                    horizontalMirrors.Add(currentMirror.ToArray());
                }
                currentMirror = new List<string>();
                verticalMirror = !verticalMirror;
            }
            else
            {
                currentMirror.Add(line);
            }
        }

        // add the last one
        if (verticalMirror)
        {
            verticalMirrors.Add(currentMirror.ToArray());
        }
        else
        {
            horizontalMirrors.Add(currentMirror.ToArray());
        }
        currentMirror = new List<string>();
        verticalMirror = !verticalMirror;

        var allMirrors = new List<string[]>();
        allMirrors.AddRange(verticalMirrors);
        allMirrors.AddRange(horizontalMirrors);

        var columnsToTheLeft = 0;
        foreach (string[] mirror in verticalMirrors)
        {
            var reflection = GetMirrorReflectionIndex(mirror);
            columnsToTheLeft += reflection;
        }

        // add the last one
        var rowsAbove = 0;
        foreach (string[] mirror in horizontalMirrors)
        {
            var transposed = Transpose(mirror);
            var reflection = GetMirrorReflectionIndex(transposed);
            rowsAbove += reflection * 100;
        }
        
        // TODO: print out the mirrors

        var total = rowsAbove + columnsToTheLeft;

        return total;
    }

    public long RunPart2(string[] input)
    {
        _logger.LogInformation("Solving - 2023 - Day 13 - Part 2");
        _logger.LogInformation("Input contains {Input} values", input.Length);

        throw new NotImplementedException();
    }

}