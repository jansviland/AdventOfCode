namespace AdventOfCode._2025.Day05;

public interface ISolutionService
{
    public long RunPart1(string[] input);
    public long RunPart2(string[] input);
}

record Range(long start, long end);

public class SolutionService : ISolutionService
{
    private readonly ILogger<ISolutionService> _logger;
    private readonly Helper _helper = new();

    public SolutionService(ILogger<SolutionService> logger)
    {
        _logger = logger;
    }

    IEnumerable<long> RangeLong(Range range)
    {
        for (long i = range.start; i <= range.end; i++)
            yield return i;
    }

    IEnumerable<Range> ParseRanges(string[] input)
    {
        var ranges = new List<Range>();
        for (var i = 0; i < input.Length; i++)
        {
            var line = input[i];
            if (!line.Contains('-'))
            {
                break;
            }

            var split = line.Split('-');
            var start = long.Parse(split[0]);
            var end = long.Parse(split[1]);

            ranges.Add(new Range(start, end));
        }

        return ranges;
    }

    IEnumerable<long> ParseIngredientId(string[] input)
    {
        var list = new List<long>();
        for (var i = 0; i < input.Length; i++)
        {
            var line = input[i];
            if (line.Contains('-'))
            {
                continue;
            }

            var isNum = long.TryParse(line, out long result);
            if (isNum)
            {
                list.Add(result);
            }

        }

        return list;
    }

    public long RunPart1(string[] input)
    {
        _logger.LogInformation("Solving - {Year} - Day {Day} - Part 1", _helper.GetYear(), _helper.GetDay());
        _logger.LogInformation("Input contains {Input} values", input.Length);

        var ranges = ParseRanges(input);
        var ingredients = ParseIngredientId(input);

        return ingredients.Count(num => ranges.Any(r => num >= r.start && num <= r.end));

        // var count = 0;
        // foreach (long i in ingredients)
        // {
        //     foreach ((long start, long end) in ranges)
        //     {
        //         if (i >= start && i <= end)
        //         {
        //             count++;
        //             break;
        //         }
        //     }
        // }

        // return count;
    }

    public long RunPart2(string[] input)
    {
        _logger.LogInformation("Solving - {Year} - Day {Day} - Part 2", _helper.GetYear(), _helper.GetDay());
        _logger.LogInformation("Input contains {Input} values", input.Length);

        // var ranges = ParseRanges(input);
        // var uniqueValues = ranges
        //     .SelectMany(r => RangeLong(r.start, r.end))
        //     .Distinct()
        //     .Count();
        //     // .OrderBy(x => x);
        //
        // return uniqueValues;
        //
        
        throw new NotImplementedException();
    }

}
