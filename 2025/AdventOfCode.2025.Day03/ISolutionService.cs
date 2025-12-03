namespace AdventOfCode._2025.Day03;

public interface ISolutionService
{
    public long RunPart1(string[] input);
    public long RunPart2(string[] input);
    long HighestValueCombo(string str, int total);
}

public class SolutionService : ISolutionService
{
    private readonly ILogger<ISolutionService> _logger;
    private readonly Helper _helper = new();

    public SolutionService(ILogger<SolutionService> logger)
    {
        _logger = logger;
    }

    // IEnumerable<int> Parse(string[] input) =>
    //     from line in input
    //     let result = highestValuePair(line)
    //     select result;

    IEnumerable<long> Parse2(string[] input, int total) =>
        from line in input
        let result = HighestValueCombo(line, total)
        select result;

    // public int highestValuePair(string str)
    // {
    //     var left = 0;
    //     var leftIndex = 0;
    //     for (int i = 0; i < str.Length - 1; i++) // we must have at least one value to the right in the end, so it can not be the last element
    //     {
    //         var num = str[i] - '0'; // assuming value 0-9, this is the fastest way to get the int from char
    //         if (num > left)
    //         {
    //             left = num;
    //             leftIndex = i;
    //         }
    //     }
    //
    //     var right = 0;
    //     for (int i = leftIndex + 1; i < str.Length; i++) // start from left index + 1, can not have the same bank twice
    //     {
    //         var num = str[i] - '0';
    //         if (num > right)
    //         {
    //             right = num;
    //         }
    //     }
    //
    //     var result = left.ToString() + right.ToString();
    //
    //     return int.Parse(result);
    // }

    public long HighestValueCombo(string str, int total)
    {
        var sb = new StringBuilder();
        var currentBank = 0;
        var nextIndex = 0;
        
        while (currentBank < total)
        {
            var curr = -1;
            var remaining = total - currentBank;
            
            for (int i = nextIndex; i <= str.Length - remaining; i++) // we must have at least one value to the right in the end, so it can not be the last element
            {
                var num = str[i] - '0'; // assuming value 0-9, this is the fastest way to get the int from char
                if (num > curr)
                {
                    curr = num;
                    nextIndex = i;
                }
            }

            sb.Append(str[nextIndex]);
            currentBank++;
            nextIndex++;
        }

        return long.Parse(sb.ToString());
    }

    public long RunPart1(string[] input)
    {
        _logger.LogInformation("Solving - {Year} - Day {Day} - Part 1", _helper.GetYear(), _helper.GetDay());
        _logger.LogInformation("Input contains {Input} values", input.Length);

        // return Parse(input).Sum();
        return Parse2(input, 2).Sum();
    }

    public long RunPart2(string[] input)
    {
        _logger.LogInformation("Solving - {Year} - Day {Day} - Part 2", _helper.GetYear(), _helper.GetDay());
        _logger.LogInformation("Input contains {Input} values", input.Length);

        return Parse2(input, 12).Sum();
    }

}
