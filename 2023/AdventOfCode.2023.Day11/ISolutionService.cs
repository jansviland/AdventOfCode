namespace AdventOfCode._2023.Day11;

public interface ISolutionService
{
    public long RunPart1(string[] input);
    public long RunPart2(string[] input);
    public string[] ExpandGrid(string[] input);
}

public class SolutionService : ISolutionService
{
    private readonly ILogger<ISolutionService> _logger;

    public SolutionService(ILogger<SolutionService> logger)
    {
        _logger = logger;
    }

    public long RunPart1(string[] input)
    {
        _logger.LogInformation("Solving - 2023 - Day 11 - Part 1");
        _logger.LogInformation("Input contains {Input} values", input.Length);

        var expanded = ExpandGrid(input);
        var grid = Parse(expanded);
        
        // find distances
        var distances = new Dictionary<(int, int), int>();
        
        throw new NotImplementedException();
    }

    public long RunPart2(string[] input)
    {
        _logger.LogInformation("Solving - 2023 - Day 11 - Part 2");
        _logger.LogInformation("Input contains {Input} values", input.Length);

        throw new NotImplementedException();
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

    /// <summary>
    /// Transpose a 2D array
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public string[] Transpose(string[] input)
    {
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

    public string[] ExpandGrid(string[] input)
    {
        var expandedRows = Expand(input);
        var columns = Transpose(expandedRows);
        var expandedColumns = Expand(columns);
        var backAgain = Transpose(expandedColumns);

        return backAgain;
    }

    public string[] Expand(string[] input)
    {
        var newRows = new List<string>();
        foreach (string line in input)
        {
            if (line.ToArray().Any(x => x == '#'))
            {
                newRows.Add(line);
            }
            else
            {
                newRows.Add(line);
                newRows.Add(line);
            }
        }

        return newRows.ToArray();
    }
}