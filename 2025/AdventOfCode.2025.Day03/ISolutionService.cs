namespace AdventOfCode._2025.Day03;

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

    IEnumerable<int> Parse(string[] input) =>
        from line in input
        let result = highestValuePair(line)
        select result;

    public int highestValuePair(string str)
    {
        var left = 0;
        var leftIndex = 0;
        for (int i = 0; i < str.Length - 1; i++) // we must have at least one value to the right in the end, so it can not be the last element
        {
            var num = str[i] - '0'; // assuming value 0-9, this is the fastest way to get the int from char
            if (num > left)
            {
                left = num;
                leftIndex = i;
            } 
        }

        var right = 0;
        for (int i = leftIndex + 1; i < str.Length; i++) // start from left index + 1, can not have the same bank twice
        {
            var num = str[i] - '0';
            if (num > right)
            {
                right = num;
            }
        }

        var result = left.ToString() + right.ToString();
        
        return int.Parse(result);
    }

    public long RunPart1(string[] input)
    {
        _logger.LogInformation("Solving - {Year} - Day {Day} - Part 1", _helper.GetYear(), _helper.GetDay());
        _logger.LogInformation("Input contains {Input} values", input.Length);

        return Parse(input).Sum();
    }

    public long RunPart2(string[] input)
    {
        _logger.LogInformation("Solving - {Year} - Day {Day} - Part 2", _helper.GetYear(), _helper.GetDay());
        _logger.LogInformation("Input contains {Input} values", input.Length);

        // return Repeates(Parse1(input)).Sum();
        throw new NotImplementedException();
    }

}
