namespace AdventOfCode._2025.Day02;

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

    bool IsValid(int val)
    {
        if (val < 1)
        {
            return false;
        }

        if (val < 9)
        {
            return true;
        }

        var str = val.ToString();
        var split = str.Substring(0, str.Length / 2);

        if (split.Length > 2 && split[0] == split[1])
        {
            return false;
        }

        return true;
    }

    IEnumerable<int> FilterInvalid(IEnumerable<int[]> ranges)
    {
        foreach (int[] range in ranges)
        {
            // can not repeat twice, 55 has '5' and '5'
            for (int i = range[0]; i < range[1]; i++)
            {
                // for each value i
                // split double digit into two and compare
                // split 4 digit, into two 1212, is 12 compared to 12, 
                // 6 digit like 123123, is split into 123 and 123, invalid

                // if i is invalid
                if (!IsValid(i))
                {
                    yield return i;
                }
            }
        }
    }


    IEnumerable<int[]> Parse1(string[] input) =>
        from range in input[0].Split(',')
        let pair = range.Split('-', StringSplitOptions.RemoveEmptyEntries)
        // from num in pair
        let start = int.Parse(pair[0])
        let end = int.Parse(pair[1])
        select new[] { start, end };

    public long RunPart1(string[] input)
    {
        _logger.LogInformation("Solving - {Year} - Day {Day} - Part 1", _helper.GetYear(), _helper.GetDay());
        _logger.LogInformation("Input contains {Input} values", input.Length);

        // 1. Parse
        var parsed = Parse1(input);
        
        // 2. Filter out all invalid 
        var invalid = FilterInvalid(parsed);
        // 3. Sum

        throw new NotImplementedException();

        // var result = from line in input
        //     let d = line[0] == 'R' ? 1 : -1
        //     let a = int.Parse(line.Substring(1))
        //     select a * d;
        //
        // return Dial(result).Count(x => x == 0);
    }

    public long RunPart2(string[] input)
    {
        _logger.LogInformation("Solving - {Year} - Day {Day} - Part 2", _helper.GetYear(), _helper.GetDay());
        _logger.LogInformation("Input contains {Input} values", input.Length);

        throw new NotImplementedException();

        // var parsed = from line in input
        //     let d = line[0] == 'R' ? 1 : -1
        //     let a = int.Parse(line.Substring(1))
        //
        //     // create a range of 1's for going Right, and a range of -1 For going left
        //     // -10, becomes [-1,-1,-1,-1,-1,-1,-1,-1,-1,-1]
        //     from i in Enumerable.Range(0, a)
        //     select d;
        //
        // // rotate and keep track of the current value
        // // count each time we hit 0
        // return Dial(parsed).Count(x => x == 0);
    }

}
