namespace AdventOfCode._2022.Day5;

public interface ISolutionService
{
    public string RunPart1(string[] input);
    public int RunPart2(string[] input);
    public List<Stack<Crate>> ParseInput(string[] input);
    public List<string> CreatePrintableOutput(List<Stack<Crate>> stacks);
    public List<Stack<Crate>> MoveCrate(List<Stack<Crate>> stacks, string move);
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
        _logger.LogInformation("Solving day 5");
        _logger.LogInformation("Input contains {Input} values", input.Length);

        var stacks = ParseInput(input);
        var output = CreatePrintableOutput(stacks);

        // TODO: preform the moves

        throw new NotImplementedException();
    }

    public int RunPart2(string[] input)
    {
        _logger.LogInformation("Solving day 5 - Part 2");
        _logger.LogInformation("Input contains {Input} values", input.Length);

        throw new NotImplementedException();
    }

    public List<string> CreatePrintableOutput(List<Stack<Crate>> stacks)
    {
        // TODO: find max stack size
        var maxStackSize = stacks.Max(s => s.Count);

        var output = new List<string>();
        for (var i = maxStackSize - 1; i >= 0; i--)
        {
            var line = new StringBuilder();

            for (var p = 0; p < stacks.Count; p++)
            {
                if (stacks[p].Count > i)
                {
                    line.Append("[" + stacks[p].Pop().Name + "] ");
                }
                else
                {
                    line.Append("    ");
                }

                // remove last space
                if (p == stacks.Count - 1)
                {
                    line.Remove(line.Length - 1, 1);
                }
            }

            output.Add(line.ToString());
        }

        var sb = new StringBuilder();
        for (var i = 0; i < stacks.Count; i++)
        {
            sb.Append(" " + (i + 1) + "  ");
        }

        output.Add(sb.Remove(sb.Length - 1, 1).ToString());

        foreach (var s in output)
        {
            _logger.LogInformation(s);
        }

        return output;
    }

    public List<Stack<Crate>> MoveCrate(List<Stack<Crate>> stacks, string move)
    {
        _logger.LogInformation("Preforming move: {Move}", move);

        // TODO: print the stacks after move is preformed

        throw new NotImplementedException();
    }

    public List<Stack<Crate>> ParseInput(string[] input)
    {
        // find where moves start in the input
        var startLine = 0;
        for (var i = 0; i < input.Length; i++)
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

            for (var p = 0; p < line.Length; p += 4)
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