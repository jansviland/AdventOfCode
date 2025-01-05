namespace AdventOfCode._2024.Day13;

public interface ISolutionService
{
    public long RunPart1(string[] input);
    public long RunPart2(string[] input);
}

record struct Machine(Complex a, Complex b, Complex price);

public class SolutionService : ISolutionService
{
    private readonly ILogger<ISolutionService> _logger;

    public SolutionService(ILogger<SolutionService> logger)
    {
        _logger = logger;
    }

    List<Machine> Parse(string[] input)
    {
        var result = new List<Machine>();

        Complex? a = null;
        Complex? b = null;
        Complex? r = null;

        // Regex pattern with named groups
        var pattern = @"X[+=](?<X>\d+), Y[+=](?<Y>\d+)";

        foreach (string s in input)
        {
            if (s.Contains("Button A:"))
            {
                var matches = Regex.Matches(s, pattern);
                var x = double.Parse(matches.First().Groups["X"].Value);
                var y = double.Parse(matches.First().Groups["Y"].Value);

                a = new Complex(x, y);
            }

            if (s.Contains("Button B:"))
            {
                var matches = Regex.Matches(s, pattern);
                var x = double.Parse(matches.First().Groups["X"].Value);
                var y = double.Parse(matches.First().Groups["Y"].Value);

                b = new Complex(x, y);
            }

            if (s.Contains("Prize:"))
            {
                var matches = Regex.Matches(s, pattern);
                var x = double.Parse(matches.First().Groups["X"].Value);
                var y = double.Parse(matches.First().Groups["Y"].Value);

                r = new Complex(x, y);
            }

            if (a != null && b != null && r != null)
            {
                result.Add(new Machine(a.Value, b.Value, r.Value));
                (a, b, r) = (null, null, null);
            }
        }

        return result;
    }

    long MinTokens(Machine machine)
    {
        // keep track of all visited positions, if we reach the same spot twice we can stop, it will just loop
        // create a binary tree, we can always do A or B, path A cost 3 tokens, path B cost 1 token

        // base case: reached goal or same location as before
        // in the binary tree we have Node: the Complex position, Edge: Path with cost 
        // when we reach a base case, check the total cost in tokens, save the cheepest path and total cost

        var visited = new HashSet<Complex>();

        throw new NotImplementedException();
    }


    public long RunPart1(string[] input)
    {
        var machines = Parse(input);

        throw new NotImplementedException();
    }

    public long RunPart2(string[] input)
    {
        throw new NotImplementedException();
    }
}
