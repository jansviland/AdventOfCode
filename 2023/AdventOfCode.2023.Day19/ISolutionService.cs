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

        long total = 0; 
        for (var i = endOfWorkflows; i < input.Length; i++)
        {
            string line = input[i];
            if (string.IsNullOrWhiteSpace(line))
            {
                continue;
            }
            
            var sum = sumOfApproved("in", line);
            total += sum;
        }

        return total;
    }

    private long sumOfApproved(string workflowKey, string values)
    {
        var workflow = _workflows[workflowKey];
        var workFlowSteps = workflow.Split(",");
        
        // foreach (var workFlowStep in workFlowSteps)
        // R - Rejected. Return 0
        // A - Approved. Return sum of values
        // a string of letters, like lnx, pv or qqz, go to new workflow with that as key
        // bigger og equal < or > and a :, compare and go to the key if true
        
        var valuesSplit = values.Substring(1, values.Length - 1).Split(",");
        
        // if (workflows.ContainsKey(workflow))
        // {
        //     return workflows[workflow] == "approved";
        // }

        return 0;
    }

    public long RunPart2(string[] input)
    {
        _logger.LogInformation("Solving - 2023 - Day 19 - Part 2");
        _logger.LogInformation("Input contains {Input} values", input.Length);

        throw new NotImplementedException();
    }
}