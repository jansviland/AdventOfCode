namespace AdventOfCode._2024.Day05;

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

        var pageRules = new Dictionary<uint, List<uint>>();
        var rows = new List<List<uint>>();

        var parseRules = true;
        foreach (var line in input)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                parseRules = false;
                continue;
            }

            if (parseRules)
            {
                var split = line.Split("|");
                var key = uint.Parse(split[0]);
                var value = uint.Parse(split[1]);

                if (pageRules.ContainsKey(key))
                {
                    pageRules[key].Add(value);
                }
                else
                {
                    pageRules.Add(key, new List<uint> { value });
                }
                continue;
            }

            rows.Add(line.Split(",").Select(uint.Parse).ToList());
        }

        uint total = 0;
        foreach (var r in rows)
        {
            var isValid = IsValid(r, pageRules);
            if (isValid)
            {
                var middleIndex = Math.Floor((double)r.Count / 2);
                var middle = r[(int)middleIndex];
                total += middle;
                _logger.LogInformation("Row {Row} is valid - Middle value: {Middle} - Total: {Total}", string.Join(",", r), middle, total);
            }
            else
            {
                _logger.LogInformation("Row {Row} is invalid", string.Join(",", r));
            }
            
            _logger.LogInformation("--------------------");
        }

        return total;
    }

    private bool IsValid(List<uint> row, Dictionary<uint, List<uint>> pageRules)
    {
        for (var i = 0; i < row.Count; i++)
        {
            if (!pageRules.ContainsKey(row[i]))
            {
                continue;
            }

            var value = row[i];
            var rules = pageRules[row[i]];
            _logger.LogInformation("Row: {Row} - Number {Page} - Subsequent Numbers: {Rules}", string.Join(',', row), row[i], string.Join(",", rules));
            
            var left = row.Slice(0, i);
            var right = row.Slice(i + 1, row.Count - i - 1);
            
            _logger.LogInformation("Left: {Left} - Right: {Right}", string.Join(',', left), string.Join(',', right));
            
            // check that all the right values exist in rules
            foreach (var r in right)
            {
                if (!rules.Contains(r))
                {
                    _logger.LogInformation("{Rule} should be after {Value}", r, value);
                    return false;
                }
            }

            foreach (uint l in left)
            {
                if (rules.Contains(l))
                {
                    _logger.LogInformation("{Rule} can not be follwed by {Value}!", l, value);
                    return false;
                }
            }
        }

        return true;
    }

    public long RunPart2(string[] input)
    {
        _logger.LogInformation("Solving - {Year} - Day {Day} - Part 2", _helper.GetYear(), _helper.GetDay());
        _logger.LogInformation("Input contains {Input} values", input.Length);

        throw new NotImplementedException();
    }
}
