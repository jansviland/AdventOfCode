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

        /*
         { in | s < 1351 }
    ┣━━━━{ px | a < 2006 }
    ┃	┣━━━━{ qkq | x < 1416 }
    ┃	┃	┣━━━━<Accept>
    ┃	┃	┗━━━━{ crn | x > 2662 }
    ┃	┃		┣━━━━<Accept>
    ┃	┃		┗━━━━<Reject>
    ┃	┗━━━━{ m > 2090 }
    ┃		┣━━━━<Accept>
    ┃		┗━━━━{ rfg | s < 537 }
    ┃			┣━━━━{ gd | a > 3333 }━━━━<Reject>
    ┃			┗━━━━{ x > 2440 }
    ┃				┣━━━━<Reject>
    ┃				┗━━━━<Accept>
    ┗━━━━{ qqz | s > 2770 }
        ┣━━━━{ qs | s > 3448 }
        ┃	┣━━━━<Accept>
        ┃	┗━━━━{ lnx | m > 1548 }━━━━<Accept>
        ┗━━━━{ m < 1801 }
            ┣━━━━{ hdj | m > 838 }
            ┃	┣━━━━<Accept>
            ┃	┗━━━━{ pv | a > 1716 }
            ┃		┣━━━━<Reject>
            ┃		┗━━━━<Accept>
            ┗━━━━<Reject>

Edit: and here's a version of the example's workflow tree with allowed part ranges at each node:

{ in | s < 1351 } ⟶ {'x': (1, 4000), 'm': (1, 4000), 'a': (1, 4000), 's': (1, 4000)}
	┣━━━━{ px | a < 2006 } ⟶ {'x': (1, 4000), 'm': (1, 4000), 'a': (1, 4000), 's': (1, 1350)}
	┃	┣━━━━{ qkq | x < 1416 } ⟶ {'x': (1, 4000), 'm': (1, 4000), 'a': (1, 2005), 's': (1, 1350)}
	┃	┃	┣━━━━<Accept> ⟶ {'x': (1, 1415), 'm': (1, 4000), 'a': (1, 2005), 's': (1, 1350)}
	┃	┃	┗━━━━{ crn | x > 2662 } ⟶ {'x': (1416, 4000), 'm': (1, 4000), 'a': (1, 2005), 's': (1, 1350)}
	┃	┃		┣━━━━<Accept> ⟶ {'x': (2663, 4000), 'm': (1, 4000), 'a': (1, 2005), 's': (1, 1350)}
	┃	┃		┗━━━━<Reject> ⟶ {'x': (1416, 2662), 'm': (1, 4000), 'a': (1, 2005), 's': (1, 1350)}
	┃	┗━━━━{ m > 2090 } ⟶ {'x': (1, 4000), 'm': (1, 4000), 'a': (2006, 4000), 's': (1, 1350)}
	┃		┣━━━━<Accept> ⟶ {'x': (1, 4000), 'm': (2091, 4000), 'a': (2006, 4000), 's': (1, 1350)}
	┃		┗━━━━{ rfg | s < 537 } ⟶ {'x': (1, 4000), 'm': (1, 2090), 'a': (2006, 4000), 's': (1, 1350)}
	┃			┣━━━━{ gd | a > 3333 } ⟶ {'x': (1, 4000), 'm': (1, 2090), 'a': (2006, 4000), 's': (1, 536)} ⟵ <Reject>
	┃			┗━━━━{ x > 2440 } ⟶ {'x': (1, 4000), 'm': (1, 2090), 'a': (2006, 4000), 's': (537, 1350)}
	┃				┣━━━━<Reject> ⟶ {'x': (2441, 4000), 'm': (1, 2090), 'a': (2006, 4000), 's': (537, 1350)}
	┃				┗━━━━<Accept> ⟶ {'x': (1, 2440), 'm': (1, 2090), 'a': (2006, 4000), 's': (537, 1350)}
	┗━━━━{ qqz | s > 2770 } ⟶ {'x': (1, 4000), 'm': (1, 4000), 'a': (1, 4000), 's': (1351, 4000)}
		┣━━━━{ qs | s > 3448 } ⟶ {'x': (1, 4000), 'm': (1, 4000), 'a': (1, 4000), 's': (2771, 4000)}
		┃	┣━━━━<Accept> ⟶ {'x': (1, 4000), 'm': (1, 4000), 'a': (1, 4000), 's': (3449, 4000)}
		┃	┗━━━━{ lnx | m > 1548 } ⟶ {'x': (1, 4000), 'm': (1, 4000), 'a': (1, 4000), 's': (2771, 3448)} ⟵ <Accept>
		┗━━━━{ m < 1801 } ⟶ {'x': (1, 4000), 'm': (1, 4000), 'a': (1, 4000), 's': (1351, 2770)}
			┣━━━━{ hdj | m > 838 } ⟶ {'x': (1, 4000), 'm': (1, 1800), 'a': (1, 4000), 's': (1351, 2770)}
			┃	┣━━━━<Accept> ⟶ {'x': (1, 4000), 'm': (839, 1800), 'a': (1, 4000), 's': (1351, 2770)}
			┃	┗━━━━{ pv | a > 1716 } ⟶ {'x': (1, 4000), 'm': (1, 838), 'a': (1, 4000), 's': (1351, 2770)}
			┃		┣━━━━<Reject> ⟶ {'x': (1, 4000), 'm': (1, 838), 'a': (1717, 4000), 's': (1351, 2770)}
			┃		┗━━━━<Accept> ⟶ {'x': (1, 4000), 'm': (1, 838), 'a': (1, 1716), 's': (1351, 2770)}
			┗━━━━<Reject> ⟶ {'x': (1, 4000), 'm': (1801, 4000), 'a': (1, 4000), 's': (1351, 2770)}
			
			
			Multiple each possible range together to get the total number of possible combinations:
			<Accept> ⟶ {'x': (1, 1415), 'm': (1, 4000), 'a': (1, 2005), 's': (1, 1350)} ⟶ 1415 * 4000 * 2005 * 1350 = 15320205000000
			<Accept> ⟶ {'x': (2663, 4000), 'm': (1, 4000), 'a': (1, 2005), 's': (1, 1350)} ⟶ 1338 * 4000 * 2005 * 1350 = 14486526000000
			<Accept> ⟶ {'x': (1, 4000), 'm': (2091, 4000), 'a': (2006, 4000), 's': (1, 1350)} ⟶ 4000 * 1910 * 1995 * 1350 = 
			
			
			[(1, 1415), (1, 4000), (1, 2005), (1, 1350)] 15320205000000 - correct
            [(2663, 4000), (1, 4000), (1, 2005), (1, 1350)] 14486526000000 - correct
            [(1, 2440), (1, 2090), (2006, 4000), (537, 1350)] 8281393428000 - correct
            [(1, 4000), (1, 4000), (1, 4000), (3449, 4000)] 35328000000000 - correct
            [(1, 4000), (1, 838), (1, 1716), (1351, 2770)] 8167885440000 - correct
			
			final sum should be: 167409079868000
			
			
         */

        throw new NotImplementedException();
    }
}