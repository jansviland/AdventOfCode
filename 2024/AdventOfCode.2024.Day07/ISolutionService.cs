using Microsoft.Extensions.Primitives;

namespace AdventOfCode._2024.Day07;

public interface ISolutionService
{
    public ulong RunPart1(string[] input);
    public ulong RunPart2(string[] input);
}

public class SolutionService : ISolutionService
{
    private readonly ILogger<ISolutionService> _logger;
    private readonly Helper _helper = new();

    private readonly StringBuilder sb = new();

    public SolutionService(ILogger<SolutionService> logger)
    {
        _logger = logger;
    }

    public ulong RunPart1(string[] input)
    {
        _logger.LogInformation("Solving - {Year} - Day {Day} - Part 1", _helper.GetYear(), _helper.GetDay());
        _logger.LogInformation("Input contains {Input} values", input.Length);

        ulong total = 0;
        for (var i = 0; i < input.Length; i++)
        {
            var split = input[i].Split(":");
            ulong result = ulong.Parse(split[0]);
            ulong[] values = split[1].Split(" ").Where(x => !string.IsNullOrEmpty(x)).Select(ulong.Parse).ToArray();

            if (IsValueValid(result, values))
            {
                var percentage = (double)i / input.Length;
                _logger.LogInformation("Line {Line}/{Total} - {Percentage:P}", i, input.Length, percentage);
                total += result;
            }
        }

        return total;
    }

    private bool IsValueValid(ulong result, ulong[] values)
    {
        char[] operators = ['+', '*'];
        var variations = GenerateCombinations(values.Length - 1, operators);

        // possible combinations is 2 ^ (n - 1)
        var possibleVariations = (int)Math.Pow(operators.Length, values.Length - 1);
        var currentVariation = 0;

        // _logger.LogInformation("Found {Variations} variations for {Values}", possibleVariations, string.Join(",", values));

        while (currentVariation != possibleVariations)
        {
            ulong currentResult = values[0];

            sb.Append($"{values[0]}");
            for (var i = 1; i < values.Length; i++)
            {
                if (variations[currentVariation][i - 1] == '+')
                {
                    currentResult += values[i];
                    sb.Append($" + {values[i]}");
                }
                else
                {
                    currentResult *= values[i];
                    sb.Append($" * {values[i]}");
                }
            }

            sb.Append($" = {currentResult}");

            if (currentResult == result)
            {
                sb.Append(": Found a match!");
                _logger.LogInformation(sb.ToString());
                sb.Clear();

                return true;
            }
            else
            {
                // sb.Append(": No match");
                // _logger.LogInformation(sb.ToString());
                sb.Clear();
            }

            currentVariation++;
        }

        return false;
    }
    
    private bool IsValueValidPart2(ulong result, ulong[] values)
    {
        char[] operators = ['+', '*', '|'];
        var variations = GenerateCombinations(values.Length - 1, operators);

        // possible combinations is 2 ^ (n - 1)
        var possibleVariations = (int)Math.Pow(operators.Length, values.Length - 1);
        var currentVariation = 0;

        // _logger.LogInformation("Found {Variations} variations for {Values}", possibleVariations, string.Join(",", values));

        while (currentVariation != possibleVariations)
        {
            ulong currentResult = values[0];

            sb.Append($"{values[0]}");
            for (var i = 1; i < values.Length; i++)
            {
                if (variations[currentVariation][i - 1] == '+')
                {
                    currentResult += values[i];
                    sb.Append($" + {values[i]}");
                }
                else if (variations[currentVariation][i - 1] == '*')
                {
                    currentResult *= values[i];
                    sb.Append($" * {values[i]}");
                }
                else
                {
                    currentResult = ulong.Parse($"{currentResult}{values[i]}");
                    sb.Append($" || {values[i]}");
                }
            }

            sb.Append($" = {currentResult}");

            if (currentResult == result)
            {
                sb.Append(": Found a match!");
                _logger.LogInformation(sb.ToString());
                sb.Clear();

                return true;
            }
            else
            {
                // sb.Append(": No match");
                // _logger.LogInformation(sb.ToString());
                sb.Clear();
            }

            currentVariation++;
        }

        return false;
    }

    static List<char[]> GenerateCombinations(int size, char[] operators)
    {
        int totalCombinations = (int)Math.Pow(operators.Length, size);
        var results = new List<char[]>(totalCombinations);

        for (int i = 0; i < totalCombinations; i++)
        {
            var combination = new char[size];
            int index = i;

            for (int j = 0; j < size; j++)
            {
                combination[j] = operators[index % operators.Length];
                index /= operators.Length;
            }

            results.Add(combination);
        }

        return results;
    }

    public ulong RunPart2(string[] input)
    {
        _logger.LogInformation("Solving - {Year} - Day {Day} - Part 2", _helper.GetYear(), _helper.GetDay());
        _logger.LogInformation("Input contains {Input} values", input.Length);

        ulong total = 0;
        for (var i = 0; i < input.Length; i++)
        {
            var split = input[i].Split(":");
            ulong result = ulong.Parse(split[0]);
            ulong[] values = split[1].Split(" ").Where(x => !string.IsNullOrEmpty(x)).Select(ulong.Parse).ToArray();

            if (IsValueValidPart2(result, values))
            {
                var percentage = (double)i / input.Length;
                _logger.LogInformation("Line {Line}/{Total} - {Percentage:P}", i, input.Length, percentage);
                total += result;
            }
        }

        return total;
    }
    
    static List<string> GenerateCombinations(ulong[] numbers)
    {
        var results = new List<string>();
        Generate(numbers, 0, "", results);
        return results;
    }
    
    static void Generate(ulong[] numbers, int index, string current, List<string> results)
    {
        if (index == numbers.Length)
        {
            results.Add(current);
            return;
        }

        for (int i = index; i < numbers.Length; i++)
        {
            // Create a group by concatenating numbers from index to i
            string group = string.Join("", numbers[index..(i + 1)]);

            // Recurse with the group added to the current combination
            string next = string.IsNullOrEmpty(current) ? group : current + " " + group;
            Generate(numbers, i + 1, next, results);
        }
    }
}
