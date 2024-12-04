namespace AdventOfCode._2024.Day04;

public interface ISolutionService
{
    public long RunPart1(string[] input);
    public long RunPart2(string[] input);
}

public class SolutionService : ISolutionService
{
    private readonly ILogger<ISolutionService> _logger;
    private readonly Helper _helper = new();

    public SolutionService(ILogger<SolutionService> logger)
    {
        _logger = logger;
    }

    public long RunPart1(string[] input)
    {
        _logger.LogInformation("Solving - {Year} - Day {Day} - Part 1", _helper.GetYear(), _helper.GetDay());
        _logger.LogInformation("Input contains {Input} values", input.Length);

        var regex = new Regex(@"(?=XMAS|SAMX)"); // we can not use (@"XMAS|SAMX"); as this will count XMASAMX as 1 match, it should be two

        var result = 0;

        var allLines = new List<string>();

        // horizontal lines
        allLines.AddRange(input);

        // vertical lines
        var columns = new List<string>();
        for (var y = 0; y < input.Length; y++)
        {
            var line = input[y];
            for (var x = 0; x < line.Length; x++)
            {
                // if the column does not exist, create it
                if (columns.Count <= x)
                {
                    columns.Add("");
                }

                // for each character along the x axis, add it to the column y axis
                // colums == lines, reversing the x and y axis
                columns[x] += line[x];
            }
        }
        allLines.AddRange(columns);

        // diagonal lines
        var diagonals = new List<string>();
        for (var y = 0; y < input.Length; y++)
        {
            var line = input[y];
            for (var x = 0; x < line.Length; x++)
            {
                // if the diagonal does not exist, create it
                if (diagonals.Count <= x + y)
                {
                    diagonals.Add("");
                }

                // for each character along the x axis, add it to the diagonal
                // diagonals == lines, reversing the x and y axis
                diagonals[x + y] += line[x];
            }
        }
        allLines.AddRange(diagonals);

        // diagonal lines reverse
        var diagonalsReverse = new List<string>();
        for (var y = 0; y < input.Length; y++)
        {
            // reverse the line
            var line = new string(input[y].Reverse().ToArray());

            for (var x = 0; x < line.Length; x++)
            {
                // if the diagonal does not exist, create it
                if (diagonalsReverse.Count <= x + y)
                {
                    diagonalsReverse.Add("");
                }

                // for each character along the x axis, add it to the diagonal
                // diagonals == lines, reversing the x and y axis
                diagonalsReverse[x + y] += line[x];
            }
        }
        allLines.AddRange(diagonalsReverse);

        // count matches
        for (var i = 0; i < allLines.Count; i++)
        {
            var count = regex.Matches(allLines[i]).Count;

            _logger.LogInformation("Found {Count} accurences of XMAS or SAMX in line {Line}", count, allLines[i]);

            // result += countXmas + countSamx;
            result += count;
        }

        return result;
    }

    public long RunPart2(string[] input)
    {
        _logger.LogInformation("Solving - {Year} - Day {Day} - Part 2", _helper.GetYear(), _helper.GetDay());
        _logger.LogInformation("Input contains {Input} values", input.Length);

        var count = 0;
        for (var y = 0; y < input.Length; y++)
        {
            var line = input[y];
            for (var x = 0; x < line.Length; x++)
            {
                var character = line[x];
                if (character == 'A')
                {
                    var diagonalA = false;
                    var diagonalB = false;

                    // check top left is M and bottom right is S
                    if (y > 0 && x > 0 && input[y - 1][x - 1] == 'M' && y < input.Length - 1 && x < line.Length - 1 && input[y + 1][x + 1] == 'S')
                    {
                        diagonalA = true;
                    }
                    // check if top left is S and bottom right is M
                    else if (y > 0 && x > 0 && input[y - 1][x - 1] == 'S' && y < input.Length - 1 && x < line.Length - 1 && input[y + 1][x + 1] == 'M')
                    {
                        diagonalA = true;
                    }

                    // check top right is M and bottom left is S
                    if (y > 0 && x < line.Length - 1 && input[y - 1][x + 1] == 'M' && y < input.Length - 1 && x > 0 && input[y + 1][x - 1] == 'S')
                    {
                        diagonalB = true;
                    }
                    // check if top right is S and bottom left is M
                    else if (y > 0 && x < line.Length - 1 && input[y - 1][x + 1] == 'S' && y < input.Length - 1 && x > 0 && input[y + 1][x - 1] == 'M')
                    {
                        diagonalB = true;
                    }

                    if (diagonalA && diagonalB)
                    {
                        count++;
                        _logger.LogInformation("Found X-MAS at {X},{Y}", x, y);
                    }
                }
            }
        }
        return count;
    }
}
