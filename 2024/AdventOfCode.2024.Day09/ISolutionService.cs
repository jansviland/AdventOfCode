namespace AdventOfCode._2024.Day09;

public interface ISolutionService
{
    ulong RunPart1(string[] input);
    ulong RunPart2(string[] input);

    string ParseLine(string line);
    string ReOrder(string line);
    ulong CalcChecksum(string line);
    string ReOrderPart2(string line);
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
        var integers = line
            .Replace(".", "[0]")
            .Split(['[', ']'], StringSplitOptions.RemoveEmptyEntries)
            .Select(int.Parse)
            .ToList();

        ulong count = 0;
        for (var i = 0; i < integers.Count; i++)
        {
            count += (ulong)(integers[i] * i);
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

            if (line[i] != '.' || i > lastOpening)
            {
                sb.Append(currentLine[i]);
            }
            else
            {
                // find first number [123] from the back
                var lastClosing = currentLine.LastIndexOf(']');
                var lastNumber = currentLine.Substring(lastOpening + 1, lastClosing - lastOpening - 1);

                sb.Append('[' + lastNumber + ']');

                currentLine = currentLine.Substring(0, lastOpening);
                removedCount++;

            }
            i++;
        }

        sb.Append(string.Concat(Enumerable.Repeat('.', removedCount)));

        return sb.ToString();
    }

    public string ReOrderPart2(string line)
    {
        // [InlineData("[0][0]...[1][1][1]...[2]...[3][3][3].[4][4].[5][5][5][5].[6][6][6][6].[7][7][7].[8][8][8][8][9][9]", "[0][0][9][9][2][1][1][1][7][7][7].[4][4].[3][3][3]....[5][5][5][5].[6][6][6][6].....[8][8][8][8]..")]

        var integers = line
            .Replace(".", "[0]")
            .Split(['[', ']'], StringSplitOptions.RemoveEmptyEntries)
            .Select(int.Parse)
            .ToList();

        var currentString = line;
        var i = 0;

        var sb = new StringBuilder();
        var endOfString = new StringBuilder();

        while (currentString.Length > 0)
        {
            // find first continues sequence of .
            var firstDot = currentString.IndexOf('.');
            var secondDot = firstDot + 1;

            while (secondDot < currentString.Length && currentString[secondDot] == '.')
            {
                secondDot++;
            }

            var gap = secondDot - firstDot;

            sb.Append(currentString.Substring(0, firstDot));

            currentString = currentString.Substring(firstDot);

            var foundMatch = false;

            while (foundMatch == false && currentString.Length > 0)
            {
                var lastOpening = currentString.LastIndexOf('[');
                var lastClosing = currentString.LastIndexOf(']');
                var lastNumber = currentString.Substring(lastOpening + 1, lastClosing - lastOpening - 1);
                var lastNumberInt = int.Parse(lastNumber);

                var amount = integers.Count(x => x == lastNumberInt);

                if (amount <= gap)
                {
                    // 1. amount x [lastNumber] to the sb
                    // 2. add missing dots to the sb
                    // 3. remove from currentString

                    var lastNumberString = '[' + lastNumber + ']';
                    sb.Append(string.Concat(Enumerable.Repeat(lastNumberString, amount)));

                    var missingDots = gap - amount;
                    sb.Append(string.Concat(Enumerable.Repeat('.', missingDots)));

                    currentString = currentString.Substring(gap, currentString.IndexOf("[" + lastNumber + "]", StringComparison.Ordinal) - gap);
                    
                    endOfString.Append(string.Concat(Enumerable.Repeat('.', amount)));
                    
                    foundMatch = true;
                }
                else
                {
                    var lastNumberString = '[' + lastNumber + ']';
                    endOfString.Append(string.Concat(Enumerable.Repeat(lastNumberString, amount)));
                    // move on to the next number from the back
                    currentString = currentString.Substring(0, currentString.IndexOf("[" + lastNumber + "]", StringComparison.Ordinal));
                }
                
                _logger.LogInformation(sb.ToString() + currentString + endOfString.ToString());
            }
        }

        return sb.ToString() + endOfString.ToString();
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

    public ulong RunPart2(string[] input)
    {
        _logger.LogInformation("Solving - {Year} - Day {Day} - Part 2", _helper.GetYear(), _helper.GetDay());
        _logger.LogInformation("Input contains {Input} values", input.Length);

        throw new NotImplementedException();
    }
}
