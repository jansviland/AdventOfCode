namespace AdventOfCode._2023.Day9;

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

    private void Print(List<long[]> lines)
    {
        var spacing = "     ";

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

    // alternative solution
    long[] Diff(long[] numbers) =>
        numbers.Zip(numbers.Skip(1)).Select(p => p.Second - p.First).ToArray();

    long ExtrapolateRight(long[] numbers) =>
        !numbers.Any() ? 0 : ExtrapolateRight(Diff(numbers)) + numbers.Last();

    long ExtrapolateLeft(long[] numbers) =>
        ExtrapolateRight(numbers.Reverse().ToArray());

    private long FinalNumber(IEnumerable<long> firstLine)
    {
        var lines = new List<long[]>();
        lines.Add(firstLine.ToArray());

        var currentLine = firstLine.ToArray();

        while (currentLine.Any(x => x != 0))
        {
            // create new array, all values are diff
            var newLine = new long[currentLine.Length - 1];
            for (int i = 0; i < currentLine.Length - 1; i++)
            {
                long diff = currentLine[i + 1] - currentLine[i];
                newLine[i] = diff;
            }
            lines.Add(newLine);

            // set current line to new line
            currentLine = newLine;
        }

        // TODO: add an extra 0 to the last line, then increase all numbers from the bottom going up
        lines.Reverse();

        var additional = 1;

        var updatedLines = new List<long[]>();

        long appendNumber = 0;
        foreach (long[] line in lines)
        {
            var newline = new long[line.Length + additional];

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

    public long RunPart1(string[] input)
    {
        _logger.LogInformation("Solving - 2023 - Day 9 - Part 1");
        _logger.LogInformation("Input contains {Input} values", input.Length);

        var list = new List<long[]>();
        foreach (string line in input)
        {
            var split = line.Split(" ");
            var numbers = split.Select(long.Parse).ToArray();
            list.Add(numbers);
        }

        long total = 0;
        foreach (long[] numbers in list)
        {
            var finalNumber1 = FinalNumber(numbers);
            total += finalNumber1;
        }

        return total;
    }

    public long RunPart2(string[] input)
    {
        _logger.LogInformation("Solving - 2023 - Day 9 - Part 2");
        _logger.LogInformation("Input contains {Input} values", input.Length);

        var list = new List<long[]>();
        foreach (string line in input)
        {
            var split = line.Split(" ");
            var numbers = split.Select(long.Parse).ToArray();
            list.Add(numbers);
        }

        long total = 0;
        foreach (long[] numbers in list)
        {
            var finalNumber = FinalNumber(numbers.Reverse());
            // var finalNumber = ExtrapolateLeft(numbers);
            total += finalNumber;

            _logger.LogInformation($"Final number: {finalNumber}");
        }

        return total;
    }
}