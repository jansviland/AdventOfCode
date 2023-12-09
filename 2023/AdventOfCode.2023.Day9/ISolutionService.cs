namespace AdventOfCode._2023.Day9;

public interface ISolutionService
{
    public int RunPart1(string[] input);
    public int RunPart2(string[] input);
}

public class SolutionService : ISolutionService
{
    private readonly ILogger<ISolutionService> _logger;

    public SolutionService(ILogger<SolutionService> logger)
    {
        _logger = logger;
    }

    private void Print(List<int[]> lines)
    {
        var spacing = "  ";

        _logger.LogInformation("--------------------");

        var sb = new StringBuilder();
        for (var i = 0; i < lines.Count; i++)
        {
            sb.Append($"Line {i}: ");

            // append spaces before numbers
            for (var j = 0; j < i; j++)
            {
                sb.Append(spacing);
            }

            foreach (var number in lines[i])
            {
                sb.Append($"{number}{spacing}");
            }

            _logger.LogInformation(sb.ToString());
            sb.Clear();
        }
    }

    private int FinalNumber(int[] firstLine)
    {
        var lines = new List<int[]>();
        lines.Add(firstLine);

        var diff = -1;
        var currentLine = firstLine;

        while (diff != 0)
        {
            // create new array, all values are diff
            var newLine = new int[currentLine.Length - 1];
            for (int i = 0; i < currentLine.Length - 1; i++)
            {
                diff = currentLine[i + 1] - currentLine[i];
                newLine[i] = diff;
            }
            lines.Add(newLine);

            // set current line to new line
            currentLine = newLine;
        }

        Print(lines);

        // TODO: add an extra 0 to the last line, then increase all numbers from the bottom going up
        lines.Reverse();

        var additional = 1;

        var updatedLines = new List<int[]>();

        var appendNumber = 0;
        foreach (int[] line in lines)
        {
            var newline = new int[line.Length + additional];

            for (var i = 0; i < line.Length + additional; i++)
            {
                if (i < line.Length)
                {
                    newline[i] = line[i];
                }
                else
                {
                    appendNumber = newline[i - 1] + appendNumber;
                    newline[i] = appendNumber;
                }
            }

            updatedLines.Add(newline);
        }

        updatedLines.Reverse();
        Print(updatedLines);

        return updatedLines.First().Last();
    }

    public int RunPart1(string[] input)
    {
        _logger.LogInformation("Solving - 2023 - Day 9 - Part 1");
        _logger.LogInformation("Input contains {Input} values", input.Length);

        var list = new List<int[]>();
        foreach (string line in input)
        {
            var split = line.Split(" ");
            var numbers = split.Select(int.Parse).ToArray();
            list.Add(numbers);
        }

        var total = 0;
        foreach (int[] numbers in list)
        {
            var finalNumber = FinalNumber(numbers);
            total += finalNumber;

            _logger.LogInformation($"Final number: {finalNumber}");
        }

        return total;
    }

    public int RunPart2(string[] input)
    {
        _logger.LogInformation("Solving - 2023 - Day 9 - Part 2");
        _logger.LogInformation("Input contains {Input} values", input.Length);

        throw new NotImplementedException();
    }
}