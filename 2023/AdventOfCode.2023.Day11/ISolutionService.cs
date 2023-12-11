using System.Diagnostics.CodeAnalysis;

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

        // find all #, going from top left to bottom right
        var galaxies = new List<(int, int)>(); // (x, y)

        for (var y = 0; y < expanded.Length; y++)
        {
            string s = expanded[y];
            for (var x = 0; x < s.Length; x++)
            {
                if (s[x] == '#')
                {
                    galaxies.Add((x, y));
                }
            }
        }

        // only compare the same two galaxies once, store all combinations in a dictionary as key
        var galaxiesCompared = new Dictionary<string, long>();

        // find distances
        foreach (var galaxy in galaxies)
        {
            foreach (var otherGalaxy in galaxies)
            {
                if (galaxy == otherGalaxy)
                {
                    continue;
                }

                var uniqeKey = $"{galaxy.Item1},{galaxy.Item2},{otherGalaxy.Item1},{otherGalaxy.Item2}";
                var uniqeKey2 = $"{otherGalaxy.Item1},{otherGalaxy.Item2},{galaxy.Item1},{galaxy.Item2}";

                if (galaxiesCompared.ContainsKey(uniqeKey) || galaxiesCompared.ContainsKey(uniqeKey2))
                {
                }
                else
                {
                    long distance = Distance(galaxy.Item1, galaxy.Item2, otherGalaxy.Item1, otherGalaxy.Item2);
                    galaxiesCompared.Add(uniqeKey, distance);
                    galaxiesCompared.Add(uniqeKey2, 0);
                }
            }
        }

        return galaxiesCompared.Values.Sum();
    }

    public long Distance(int x1, int y1, int x2, int y2)
    {
        return Math.Abs(x1 - x2) + Math.Abs(y1 - y2);
    }

    public long RunPart2(string[] input)
    {
        _logger.LogInformation("Solving - 2023 - Day 11 - Part 2");
        _logger.LogInformation("Input contains {Input} values", input.Length);

        /*
            1 -> 8890760
            10 -> 16 010 894 - diff: 7 120 134
            100 -> 87 212 234 - diff: 71 201 340
            1000 -> 799 225 634 - diff: 712 013 400
            10 000 -> 7 919 359 634 - diff: 7 120 134 000
            100 000 -> 79 120 699 634 - diff: 71 201 340 000
            1 000 000 -> 791 134 099 634
         */

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
                for (var i = 0; i < 1000; i++)
                {
                    newRows.Add(line);
                }
            }
        }

        return newRows.ToArray();
    }
}