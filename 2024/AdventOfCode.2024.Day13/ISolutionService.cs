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

    // https://en.wikipedia.org/wiki/Cramer%27s_rule
    // Example: 
    // det =  (a1 * b2 - a2 * b1)
    // detX = (c1 * b2 - c2 * b1)
    // detY = (a1 * c2 - a2 * c1)
    // x = detX / det
    // y = detY / det
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
