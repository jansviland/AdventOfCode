namespace AdventOfCode._2024.Day03;

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

        // Regular expression to match mul(number,number)
        string pattern = @"mul\((\d+),(\d+)\)";

        // Create a Regex object
        Regex regex = new Regex(pattern);

        // join strings into one string
        string inputString = string.Join("", input);

        // Find matches
        MatchCollection matches = regex.Matches(inputString);

        var total = 0;
        foreach (Match match in matches)
        {
            _logger.LogInformation("Match: {Match}", match.Value);

            var firstValue = int.Parse(match.Groups[1].Value);
            var secondValue = int.Parse(match.Groups[2].Value);

            var result = firstValue * secondValue;

            _logger.LogInformation("{FirstValue} * {SecondValue} = {Result}", firstValue, secondValue, result);

            total += result;
        }

        return total;
    }

    public long RunPart2(string[] input)
    {
        _logger.LogInformation("Solving - {Year} - Day {Day} - Part 2", _helper.GetYear(), _helper.GetDay());
        _logger.LogInformation("Input contains {Input} values", input.Length);

        // Regular expression to match mul(number,number)
        string patternMul = @"mul\((\d+),(\d+)\)";
        string patterndo = @"do\(\)";
        string patterndont = @"don't\(\)";

        // Create a Regex object
        Regex regexMul = new Regex(patternMul);
        Regex regexDo = new Regex(patterndo);
        Regex regexDont = new Regex(patterndont);

        var total = 0;
        var mulEnabled = true; // "do" is enabled by default, if "don't" is found, disable it

        // join strings into one string
        string inputString = string.Join("", input);

        // 1. look for do or dont
        // 2. save if we have state do or dont
        // 3. look for mul (after the next do or dont)
        // 4. continue until we find another do or dont or mul

        int index = 0;
        while (index < inputString.Length)
        {
            // find first do or dont
            Match matchDo = regexDo.Match(inputString, index);
            Match matchDont = regexDont.Match(inputString, index);
            Match matchMul = regexMul.Match(inputString, index);
            
            var doIndex = matchDo.Success ? matchDo.Index : int.MaxValue;
            var dontIndex = matchDont.Success ? matchDont.Index : int.MaxValue;
            var mulIndex = matchMul.Success ? matchMul.Index : int.MaxValue;
            
            if (doIndex == int.MaxValue && dontIndex == int.MaxValue && mulIndex == int.MaxValue)
            {
                break;
            }
            

            if (matchDo.Success && matchDont.Success && matchMul.Success)
            {
                if (matchMul.Index < matchDont.Index && matchMul.Index < matchDo.Index)
                {
                    _logger.LogInformation("Match: {Match}", matchMul.Value);
                    
                    var firstValue = int.Parse(matchMul.Groups[1].Value);
                    var secondValue = int.Parse(matchMul.Groups[2].Value);

                    if (mulEnabled)
                    {
                        var result = firstValue * secondValue;

                        _logger.LogInformation("{FirstValue} * {SecondValue} = {Result}", firstValue, secondValue, result);

                        total += result;
                    }

                    index = matchMul.Index + matchMul.Length;
                }
                else if (matchDo.Index < matchDont.Index)
                {
                    _logger.LogInformation("Match: {Match}", matchDo.Value);
                    
                    mulEnabled = true;
                    index = matchDo.Index + matchDo.Length;
                }
                else
                {
                    _logger.LogInformation("Match: {Match}", matchDont.Value);
                    
                    mulEnabled = false;
                    index = matchDont.Index + matchDont.Length;
                }
            }
            else if (matchDo.Success)
            {
                mulEnabled = true;
                index = matchDo.Index + matchDo.Length;
            }
            else if (matchDont.Success)
            {
                mulEnabled = false;
                index = matchDont.Index + matchDont.Length;
            }
            else
            {
                index++;
            }
            
        }

        return total;
    }
}
