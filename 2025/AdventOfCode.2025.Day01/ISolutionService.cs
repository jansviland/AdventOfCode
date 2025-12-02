namespace AdventOfCode._2025.Day01;

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

    private static IEnumerable<int> Dial(IEnumerable<int> rotations)
    {
        int pos = 50;
        foreach (int rotation in rotations)
        {
            pos = (pos + rotation) % 100;
            yield return pos;
        }
    }

    private static IEnumerable<int> Parse1(string[] input) =>
        from line in input
        let d = line[0] == 'R' ? 1 : -1
        let a = int.Parse(line[1..])
        select a * d;

    // create a range of 1's for going Right, and a range of -1 For going left
    // -10, becomes [-1,-1,-1,-1,-1,-1,-1,-1,-1,-1]
    private static IEnumerable<int> Parse2(string[] input) =>
        from line in input
        let d = line[0] == 'R' ? 1 : -1
        let a = int.Parse(line[1..])
        from i in Enumerable.Range(0, a)
        select d;

    public long RunPart1(string[] input)
    {
        _logger.LogInformation("Solving - {Year} - Day {Day} - Part 1", _helper.GetYear(), _helper.GetDay());
        _logger.LogInformation("Input contains {Input} values", input.Length);

        return Dial(Parse1(input)).Count(x => x == 0);
    }

    public long RunPart2(string[] input)
    {
        _logger.LogInformation("Solving - {Year} - Day {Day} - Part 2", _helper.GetYear(), _helper.GetDay());
        _logger.LogInformation("Input contains {Input} values", input.Length);

        // rotate and keep track of the current value
        // count each time we hit 0
        return Dial(Parse2(input)).Count(x => x == 0);
    }

}
