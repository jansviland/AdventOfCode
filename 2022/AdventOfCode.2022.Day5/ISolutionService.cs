namespace AdventOfCode._2022.Day5;

public interface ISolutionService
{
    public string RunPart1(string[] input);
    public string RunPart2(string[] input);
    public List<Stack<Crate>> ParseInput(string[] input);
    public List<string> CreatePrintableOutput(List<Stack<Crate>> stacks);
    public List<Stack<Crate>> MoveCratesOneAtATime(List<Stack<Crate>> stacks, string move);
    public List<Stack<Crate>> MoveCratesMultipleAtATime(List<Stack<Crate>> stacks, string move);
}

public class Crate
{
    public string Name { get; set; } = null!;
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

        _logger.LogInformation("Initial stack");
        CreatePrintableOutput(stacks);

        var startLine = 0;
        for (var i = 0; i < input.Length; i++)
        {
            if (input[i].Contains("move"))
            {
                startLine = i;
                break;
            }
        }

        for (var i = startLine; i < input.Length; i++)
        {
            stacks = MoveCratesOneAtATime(stacks, input[i]);
        }

        // get top of each stack
        return stacks
            .Select(s => s.Peek().Name)
            .Aggregate((a, b) => a + b);
    }

    public string RunPart2(string[] input)
    {
        _logger.LogInformation("Solving day 5 - Part 2");
        _logger.LogInformation("Input contains {Input} values", input.Length);

        var stacks = ParseInput(input);

        _logger.LogInformation("Initial stack");
        CreatePrintableOutput(stacks);

        var startLine = 0;
        for (var i = 0; i < input.Length; i++)
        {
            if (input[i].Contains("move"))
            {
                startLine = i;
                break;
            }
        }

        for (var i = startLine; i < input.Length; i++)
        {
            stacks = MoveCratesMultipleAtATime(stacks, input[i]);
        }

        // get top of each stack
        return stacks
            .Select(s => s.Peek().Name)
            .Aggregate((a, b) => a + b);
    }

    public List<string> CreatePrintableOutput(List<Stack<Crate>> stacks)
    {
        var maxStackSize = stacks.Max(s => s.Count);

        var output = new List<string>();
        for (var i = maxStackSize - 1; i >= 0; i--)
        {
            var line = new StringBuilder();
            foreach (var s in stacks)
            {
                if (s.Count > i)
                {
                    var index = s.Count - i - 1;
                    line.Append("[" + s.ElementAt(index).Name + "] ");
                }
                else
                {
                    line.Append("    ");
                }
            }

            output.Add(line.Remove(line.Length - 1, 1).ToString());
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

    public List<Stack<Crate>> MoveCratesOneAtATime(List<Stack<Crate>> stacks, string move)
    {
        var split = move.Split(" ");
        var amount = int.Parse(split[1]);
        var from = int.Parse(split[3]);
        var to = int.Parse(split[5]);

        for (var i = 0; i < amount; i++)
        {
            var crate = stacks[from - 1].Pop();
            stacks[to - 1].Push(crate);
        }

        _logger.LogInformation(move);

        CreatePrintableOutput(stacks);

        return stacks;
    }

    public List<Stack<Crate>> MoveCratesMultipleAtATime(List<Stack<Crate>> stacks, string move)
    {
        var split = move.Split(" ");
        var amount = int.Parse(split[1]);
        var from = int.Parse(split[3]);
        var to = int.Parse(split[5]);

        var temp = new Stack<Crate>();
        for (var i = 0; i < amount; i++)
        {
            temp.Push(stacks[from - 1].Pop());
        }

        for (var i = 0; i < amount; i++)
        {
            stacks[to - 1].Push(temp.Pop());
        }

        _logger.LogInformation(move);

        CreatePrintableOutput(stacks);

        return stacks;
    }

    /// <summary>
    /// start from the bottom, and work your way up
    /// each line has the same pattern 3 characters, 1 space, 3 characters, 1 space, 3 characters
    /// so we can parse the first three characters
    /// if it's an empty string, no crate is there, if we see [A-Z] then we have a crate,
    /// then we add it to the top of the current stack, and move to the next stack
    /// </summary>
    public List<Stack<Crate>> ParseInput(string[] input)
    {
        var startLine = 0;
        for (var i = 0; i < input.Length; i++)
        {
            if (input[i].Contains("move"))
            {
                startLine = i - 3;
                break;
            }
        }

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
                        Name = crateName.Substring(1, crateName.Length - 2)
                    });
                }

                currentStack++;
            }
        }

        return stacks;
    }
}