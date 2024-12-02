namespace AdventOfCode._2024.Day02;

public interface ISolutionService
{
    public long RunPart1(string[] input);
    public long RunPart2(string[] input);
}

public class SolutionService : ISolutionService
{
    private readonly ILogger<ISolutionService> _logger;
    private readonly Helper _helper = new();

    public SolutionService(ILogger<SolutionService> logger)
    {
        _logger = logger;
    }

    public long RunPart1(string[] input)
    {
        _logger.LogInformation("Solving - {Year} - Day {Day} - Part 1", _helper.GetYear(), _helper.GetDay());
        _logger.LogInformation("Input contains {Input} values", input.Length);

        var safeLines = 0;

        foreach (var line in input)
        {

            int[] values = line.Split(" ").Select(int.Parse).ToArray();

            if (IsSafe(values))
            {
                safeLines++;
            }
        }

        return safeLines;
    }

    private bool IsSafe(int[] values)
    {
        _logger.LogInformation("Checking line: {Line}", string.Join(" ", values));
        
        // check if the values is increasing or decreasing
        bool increasing = true;
        int diff = values[0] - values[1];
        if (diff < 0)
        {
            increasing = false;
        }

        for (var i = 0; i < values.Length - 1; i++)
        {
            var current = values[i];
            var next = values[i + 1];
            var diffToAdjacent = current - next;

            if (diffToAdjacent < 0 && increasing)
            {
                _logger.LogInformation("Line was increasing and then decreased");
                return false;
            }
            else if (diffToAdjacent > 0 && !increasing)
            {
                _logger.LogInformation("Line was decreasing and then increased");
                return false;
            }

            var absDiff = Math.Abs(diffToAdjacent);
            if (absDiff > 3)
            {
                _logger.LogInformation("Line had a difference greater than 3");
                return false;
            }

            if (absDiff < 1)
            {
                _logger.LogInformation("Line had a difference less than 1");
                return false;
            }
        }

        return true;
    }

    public long RunPart2(string[] input)
    {
        _logger.LogInformation("Solving - {Year} - Day {Day} - Part 2", _helper.GetYear(), _helper.GetDay());
        _logger.LogInformation("Input contains {Input} values", input.Length);

        // for each line, check variations of the line where one value is removed
        // example: 1 2 3 4 5
        // if that does not work, attempt 2 3 4 5, then 1 3 4 5, then 1 2 4 5, then 1 2 3 5, then 1 2 3 4

        var safeLines = 0;
        
        foreach (var line in input)
        {
            var values = line.Split(" ").Select(int.Parse).ToArray();
            
            if (IsSafe(values))
            {
                safeLines++;
                continue;
            }
            
            for (var i = 0; i < values.Length; i++)
            {
                var newValues = values.ToList();
                newValues.RemoveAt(i);
                
                if (IsSafe(newValues.ToArray()))
                {
                    safeLines++;
                    break;
                }
            }
            
        }
        
        return safeLines;
    }


}
