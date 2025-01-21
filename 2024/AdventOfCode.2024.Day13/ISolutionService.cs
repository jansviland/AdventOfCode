namespace AdventOfCode._2024.Day13;

// define machine as three vectors
using Machine = (Vec2 a, Vec2 b, Vec2 p);

public interface ISolutionService
{
    public long RunPart1(string[] input);
    public long RunPart2(string[] input);
}

// implementation of a vector using long instead of float
record struct Vec2(long x, long y);

public class SolutionService : ISolutionService
{
    private readonly ILogger<ISolutionService> _logger;

    public SolutionService(ILogger<SolutionService> logger)
    {
        _logger = logger;
    }

    // Calculates the determinant of two vectors a and b. This is used in Cramer's rule for solving the linear equation.
    long Det(Vec2 a, Vec2 b) => a.x * b.y - a.y * b.x;

    // This task is essentally and linear equation problem. We can solve it using Cramer's rule.
    //
    // https://en.wikipedia.org/wiki/Cramer%27s_rule
    // Example: 
    // 2x + 3y = 15
    // 4x - 5y = -5
    //
    // Det(A) = |2 3| = 2 * 5 - 3 * 4 = -2
    //          |4 -5|   4 * 3 - -5 * 2 = 23
    //
    // Det(Ax) = |15 3| = 15 * 5 - 3 * -5 = 90
    //           |-5 -5|   -5 * 3 - -5 * 15 = 90
    //
    // Det(Ay) = |2 15| = 2 * -5 - 15 * 4 = -65
    //           |4 -5|   4 * 15 - -5 * 2 = 65
    //
    // x = Det(Ax) / Det(A) = 90 / -2 = -45
    // y = Det(Ay) / Det(A) = 65 / -2 = -32.5
    //
    // Since x is negative we know that the solution is invalid.
    //
    // The prize is calculated by the formula 3 * x + y.
    // So the prize in this case would be 3 * -45 + -32.5 = -137.5
    //
    // The function returns 0 if the solution is invalid otherwise the prize.
    long CalculatePrice(Machine m)
    {
        var (a, b, p) = m;

        var i = Det(p, b) / Det(a, b);
        var j = Det(a, p) / Det(a, b);

        // return the prize when a non negative _integer_ solution is found
        if (i >= 0 && j >= 0 && a.x * i + b.x * j == p.x && a.y * i + b.y * j == p.y)
        {
            return 3 * i + j;
        }
        else
        {
            return 0;
        }
    }

    IEnumerable<Machine> Parse(string[] input)
    {
        var longInput = input.Aggregate("", ((curr, next) => curr + next));
        var blocks = longInput.Split("\n\n");

        foreach (string block in blocks)
        {
            var nums = Regex.Matches(block, @"\d+", RegexOptions.Multiline)
                .Select(m => int.Parse(m.Value))
                .Chunk(2)
                .Select(p => new Vec2(p[0], p[1]))
                .ToArray();

            yield return (nums[0], nums[1], nums[2]);
        }
    }

    List<Machine> Parse2(string[] input, long shift = 0)
    {
        var result = new List<Machine>();

        Vec2? a = null;
        Vec2? b = null;
        Vec2? r = null;

        // Regex pattern with named groups
        var pattern = @"X[+=](?<X>\d+), Y[+=](?<Y>\d+)";

        foreach (string s in input)
        {
            if (s.Contains("Button A:"))
            {
                var matches = Regex.Matches(s, pattern);
                var x = long.Parse(matches.First().Groups["X"].Value);
                var y = long.Parse(matches.First().Groups["Y"].Value);

                a = new Vec2(x, y);
            }

            if (s.Contains("Button B:"))
            {
                var matches = Regex.Matches(s, pattern);
                var x = long.Parse(matches.First().Groups["X"].Value);
                var y = long.Parse(matches.First().Groups["Y"].Value);

                b = new Vec2(x, y);
            }

            if (s.Contains("Prize:"))
            {
                var matches = Regex.Matches(s, pattern);
                var x = long.Parse(matches.First().Groups["X"].Value);
                var y = long.Parse(matches.First().Groups["Y"].Value);

                r = new Vec2(x + shift, y + shift);
            }

            if (a != null && b != null && r != null)
            {
                result.Add(new Machine(a.Value, b.Value, r.Value));
                (a, b, r) = (null, null, null);
            }
        }

        return result;
    }

    public long RunPart1(string[] input)
    {
        return Parse2(input).Sum(CalculatePrice);
    }

    public long RunPart2(string[] input)
    {
        return Parse2(input, 10000000000000).Sum(CalculatePrice);
    }
}
