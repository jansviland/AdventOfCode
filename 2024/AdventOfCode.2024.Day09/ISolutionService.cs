namespace AdventOfCode._2024.Day09;

public interface ISolutionService
{
    long RunPart1(string[] input);
    long RunPart2(string[] input);
    string ParseLine(string line);
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
        
        var parsed = ParseLine(input[0]);
        _logger.LogInformation("Parsed: {Parsed}", parsed);

        throw new NotImplementedException();
    }

    public string ParseLine(string line)
    {
        var isFile = true;
        var sb = new StringBuilder();
        var currentFileNumber = 0;

        for (var i = 0; i < line.Length; i++)
        {
            if (isFile)
            {
                var amount = int.Parse(line[i].ToString());
                string currentFileNumberString = '[' + currentFileNumber.ToString() + ']';
                sb.Append(string.Concat(Enumerable.Repeat(currentFileNumberString, amount)));
                currentFileNumber++;
            }
            else
            {
                var amount = int.Parse(line[i].ToString());
                sb.Append(string.Concat(Enumerable.Repeat('.', amount)));
            }
            
            isFile = !isFile;
        }

        return sb.ToString();
    }

    public long RunPart2(string[] input)
    {
        _logger.LogInformation("Solving - {Year} - Day {Day} - Part 2", _helper.GetYear(), _helper.GetDay());
        _logger.LogInformation("Input contains {Input} values", input.Length);

        throw new NotImplementedException();
    }
}
