namespace AdventOfCode._2023.Day8;

public interface ISolutionService
{
    public int RunPart1(string[] input);
    public int RunPart2(string[] input);
}

public class Node
{
    // public string Value { get; set; }

    public string Value { get; set; } = null!;
    public string Left { get; set; } = null!;
    public string Right { get; set; } = null!;

    // public Tree Left { get; set; }
    // public Tree Right { get; set; }
}

public class SolutionService : ISolutionService
{
    private readonly ILogger<ISolutionService> _logger;

    public SolutionService(ILogger<SolutionService> logger)
    {
        _logger = logger;
    }

    public int RunPart1(string[] input)
    {
        _logger.LogInformation("Solving - 2023 - Day 8 - Part 1");
        _logger.LogInformation("Input contains {Input} values", input.Length);

        var nodes = new Dictionary<string, Node>();
        for (var i = 2; i < input.Length; i++)
        {
            string key = input[i].Split('=')[0].Trim();
            string[] leftAndRight = input[i].Split('=')[1].Trim().Split(',');
            string left = leftAndRight[0].Replace('(', ' ').Replace(')', ' ').Replace(',', ' ').Trim();
            string right = leftAndRight[1].Replace('(', ' ').Replace(')', ' ').Replace(',', ' ').Trim();

            if (nodes.TryGetValue(key, out var newNode))
            {
            }
            else
            {
                // create new node
                nodes.Add(key, new Node
                {
                    Value = key,
                    Left = left,
                    Right = right
                });
            }
        }

        var instructions = input[0];

        var goal = "ZZZ";
        var start = "AAA";
        nodes.TryGetValue(start, out var current);

        if (current == null)
        {
            throw new Exception("Could not find start node");
        }

        var totalSteps = 0;
        var step = 0;
        while (current.Value != goal)
        {
            // start from the beginning when we reach the end of the instructions
            if (step >= instructions.Length)
            {
                step = 0;
            }

            var instruction = instructions[step];
            if (instruction == 'L')
            {
                // go left
                nodes.TryGetValue(current.Left, out var leftNode);
                if (leftNode == null)
                {
                    throw new Exception("Could not find left node");
                }

                current = leftNode;
            }
            else if (instruction == 'R')
            {
                // go right
                nodes.TryGetValue(current.Right, out var rightNode);
                if (rightNode == null)
                {
                    throw new Exception("Could not find right node");
                }

                current = rightNode;
            }
            else
            {
                throw new Exception("Unknown instruction");
            }

            step++;
            totalSteps++;
        }

        return totalSteps;
    }

    public int RunPart2(string[] input)
    {
        _logger.LogInformation("Solving - 2023 - Day 8 - Part 2");
        _logger.LogInformation("Input contains {Input} values", input.Length);

        throw new NotImplementedException();
    }
}