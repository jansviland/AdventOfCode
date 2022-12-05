namespace AdventOfCode._2022.Day5;

public interface ISolutionService
{
    public string RunPart1(string[] input);
    public int RunPart2(string[] input);
    public List<Stack<Crate>> ParseInput(string[] input);
}

public class Crate
{
    public string Name { get; set; }
}

public class SolutionService : ISolutionService
{
    private readonly ILogger<SolutionService> _logger;

    public SolutionService(ILogger<SolutionService> logger)
    {
        _logger = logger;
    }

    public string RunPart1(string[] input)
    {
        _logger.LogInformation("Solving day 4");
        _logger.LogInformation("Input contains {Input} values", input.Length);

        throw new NotImplementedException();
    }

    public int RunPart2(string[] input)
    {
        _logger.LogInformation("Solving day 4 - Part 2");
        _logger.LogInformation("Input contains {Input} values", input.Length);

        throw new NotImplementedException();
    }

    private void PrintAllCrates(List<Stack<Crate>> stacks)
    {
        foreach (var stack in stacks)
        {
            foreach (var crate in stack)
            {
                // TODO: print the same way as in the description

                _logger.LogInformation("Crate {Crate} is in stack {Stack}", crate.Name, stacks.IndexOf(stack));
            }
        }
    }

    public List<Stack<Crate>> ParseInput(string[] input)
    {
        // find where moves start in the input
        var startLine = 0;
        for (int i = 0; i < input.Length; i++)
        {
            if (input[i].Contains("move"))
            {
                startLine = i - 3;
                break;
            }
        }

        // start from the bottom, and work your way up
        // each line has the same pattern 3 characters, 1 space, 3 characters, 1 space, 3 characters
        // so we can parse the first three characters
        // if its an empty string, no crate is there, if we see [A-Z] then we have a crate,
        // then we add it to the top of the current stack, and move to the next stack

        var stacks = new List<Stack<Crate>>();
        for (var i = startLine; i >= 0; i--)
        {
            var line = input[i];
            var currentStack = 0;

            for (int p = 0; p < line.Length; p += 4)
            {
                var crateName = line.Substring(p, 3);
                if (!string.IsNullOrWhiteSpace(crateName))
                {
                    if (stacks.Count <= currentStack)
                    {
                        stacks.Add(new Stack<Crate>());
                    }

                    stacks[currentStack].Push(new Crate
                    {
                        // remove [ and ] from the name
                        Name = crateName.Substring(1, crateName.Length - 2)
                    });
                }

                currentStack++;
            }
        }

        return stacks;
    }
}