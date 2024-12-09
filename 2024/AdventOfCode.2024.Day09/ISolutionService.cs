namespace AdventOfCode._2024.Day09;

public interface ISolutionService
{
    ulong RunPart1(string[] input);
    long RunPart2(string[] input);
    
    string ParseLine(string line);
    string ReOrder(string line);
    ulong CalcChecksum(string line);
}

public class SolutionService : ISolutionService
{
    private readonly ILogger<ISolutionService> _logger;
    private readonly Helper _helper = new();

    public SolutionService(ILogger<SolutionService> logger)
    {
        _logger = logger;
    }

    public ulong RunPart1(string[] input)
    {
        _logger.LogInformation("Solving - {Year} - Day {Day} - Part 1", _helper.GetYear(), _helper.GetDay());
        _logger.LogInformation("Input contains {Input} values", input.Length);

        var parsed = ParseLine(input[0]);
        _logger.LogInformation("Parsed: {Parsed}", parsed);

        var reordered = ReOrder(parsed);
        _logger.LogInformation("Reordered: {Reordered}", reordered);

        var checksum = CalcChecksum(reordered);
        _logger.LogInformation("Checksum: {Checksum}", checksum);
        
        return checksum;
    }

    public ulong CalcChecksum(string line)
    {
        var integers = line.Replace(".", string.Empty).Split(['[', ']'], StringSplitOptions.RemoveEmptyEntries)
            .Select(int.Parse)
            .ToList();
        
        ulong count = 0;
        for (var i = 0; i < integers.Count; i++)
        {
            count += (ulong) (integers[i] * i);
        }
        
        return count;
    }

    public string ReOrder(string line)
    {
        int i = 0;
        int removedCount = 0;
        
        var sb = new StringBuilder();
        var currentLine = line;

        while (currentLine.Length > 0 && i < currentLine.Length)
        {
            var lastOpening = currentLine.LastIndexOf('[');
            
            // take first empty . from the front
            // take first number from the back
            if (line[i] != '.' || i > lastOpening)
            {
                sb.Append(currentLine[i]);
                i++;
            }
            else
            {
                // find first number [123] from the back
                var lastClosing = currentLine.LastIndexOf(']');
                var lastNumber = currentLine.Substring(lastOpening + 1, lastClosing - lastOpening - 1);

                sb.Append('[' + lastNumber + ']');
                
                currentLine = currentLine.Substring(0, lastOpening);
                removedCount++;
                
                i++;
            }
        }
        
        sb.Append(string.Concat(Enumerable.Repeat('.', removedCount)));

        return sb.ToString();
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
