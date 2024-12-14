namespace AdventOfCode._2024.Day14;

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

        // get left column and right column, subtract each number in the right column from the corresponding number in the left column, sum the results
        return Column(input, 0)
            .Zip(Column(input, 1), (l, r) => Math.Abs(l - r))
            .Sum();
    }

    public long RunPart2(string[] input)
    {
        _logger.LogInformation("Solving - {Year} - Day {Day} - Part 2", _helper.GetYear(), _helper.GetDay());
        _logger.LogInformation("Input contains {Input} values", input.Length);

        // get right column and count the number of times each number appears, store in dictionary with number as key, count as value
        var numberCount = Column(input, 1)
            .CountBy(x => x)
            .ToDictionary();

        // get left column, multiply each number by the count of the number in the right column, sum the results
        // if the number is not in the dictionary, the count is 0 and we multiply by 0 so it doesn't affect the sum
        return Column(input, 0)
            .Select(num => numberCount.GetValueOrDefault(num) * num)
            .Sum();
    }

    private static IEnumerable<int> Column(string[] input, int column) =>
        from line in input
        let nums = line.Split("   ").Select(int.Parse).ToArray()
        orderby nums[column]
        select nums[column];
}
