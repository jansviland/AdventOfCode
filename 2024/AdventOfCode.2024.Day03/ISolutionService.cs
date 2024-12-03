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

        // 1. look for do(), don't() and mul(x,y)
        // 2. if we don't find any, we are done
        // 3. save if we have state do or dont (mulEnabled)
        // 4. if we have mul(x,y) and mulEnabled is true, calculate x*y and add to total

        int index = 0;
        while (index < inputString.Length)
        {
            
            // TODO: combine these into one regex, with groups for do, dont and mul
            
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

            // find the next match
            if (doIndex < dontIndex && doIndex < mulIndex)
            {
                mulEnabled = true;
                index = matchDo.Index + matchDo.Length;

                _logger.LogInformation("Found do, setting {Index}", index);
            }
            else if (dontIndex < doIndex && dontIndex < mulIndex)
            {
                mulEnabled = false;
                index = matchDont.Index + matchDont.Length;

                _logger.LogInformation("Found don't, setting {Index}", index);
            }
            else
            {
                _logger.LogInformation("Found mul, setting {Index}, MulEnabled is {MulEnabled}", index, mulEnabled);

                if (mulEnabled)
                {
                    var firstValue = int.Parse(matchMul.Groups[1].Value);
                    var secondValue = int.Parse(matchMul.Groups[2].Value);
                    var result = firstValue * secondValue;

                    _logger.LogInformation("{FirstValue} * {SecondValue} = {Result}", firstValue, secondValue, result);

                    total += result;
                }

                index = matchMul.Index + matchMul.Length;
            }
        }

        return total;
    }
}
