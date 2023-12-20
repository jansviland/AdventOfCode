namespace AdventOfCode._2023.Day19;

public interface ISolutionService
{
    public long RunPart1(string[] input);
    public long RunPart2(string[] input);
}

public class SolutionService : ISolutionService
{
    private readonly ILogger<ISolutionService> _logger;
    private readonly Dictionary<string, string> _workflows = new();

    public SolutionService(ILogger<SolutionService> logger)
    {
        _logger = logger;
    }

    public long RunPart1(string[] input)
    {
        _logger.LogInformation("Solving - 2023 - Day 19 - Part 1");
        _logger.LogInformation("Input contains {Input} values", input.Length);

        var endOfWorkflows = 0;
        for (var i = 0; i < input.Length; i++)
        {
            string line = input[i];
            if (string.IsNullOrWhiteSpace(line))
            {
                endOfWorkflows = i + 1;
                break;
            }

            var split = line.Split("{");
            _workflows.Add(split[0], split[1].Substring(0, split[1].Length - 1));
        }

        // final workflows
        _workflows.Add("A", "A");
        _workflows.Add("R", "R");

        long total = 0;
        for (var i = endOfWorkflows; i < input.Length; i++)
        {
            string line = input[i];
            if (string.IsNullOrWhiteSpace(line))
            {
                continue;
            }

            var sum = SumOfApproved("in", line.Substring(1, line.Length - 2));
            total += sum;
        }

        return total;
    }

    private long SumOfApproved(string workflowKey, string values)
    {
        var workflow = _workflows[workflowKey];
        var workFlowSteps = workflow.Split(",");

        for (int i = 0; i < workFlowSteps.Length; i++)
        {
            if (workFlowSteps[i] == "R")
            {
                return 0;
            }

            if (workFlowSteps[i] == "A")
            {
                var valuesSplit = values.Split(",").Select(x => x.Substring(2)).Select(long.Parse);
                return valuesSplit.Sum();
            }

            var workflowCompare = workFlowSteps[i].Split(":");
            if (workflowCompare.Length == 1)
            {
                return SumOfApproved(workflowCompare[0], values);
            }

            if (workflowCompare.Length == 2)
            {
                char compare = workflowCompare[0][0];
                char operation = workflowCompare[0][1];
                var value = long.Parse(workflowCompare[0].Substring(2));

                var valuesSplit = values.Split(",");
                var compareValue = valuesSplit.Where(x => x.IndexOf(compare) != -1).Select(x => x.Substring(2));
                var compareValueLong = long.Parse(compareValue.First());

                if (operation == '<')
                {
                    if (compareValueLong < value)
                    {
                        return SumOfApproved(workflowCompare[1], values);
                    }
                }
                else
                {
                    if (compareValueLong > value)
                    {
                        return SumOfApproved(workflowCompare[1], values);
                    }
                }
            }
        }

        return 0;
    }

    public long RunPart2(string[] input)
    {
        _logger.LogInformation("Solving - 2023 - Day 19 - Part 2");
        _logger.LogInformation("Input contains {Input} values", input.Length);

        // https://en.wikipedia.org/wiki/K-d_tree
        // https://www.reddit.com/r/adventofcode/comments/18lwcw2/2023_day_19_an_equivalent_part_2_example_spoilers/
        
        throw new NotImplementedException();
    }
}