namespace AdventOfCode._2024.Day04;

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
        
        var regex = new Regex(@"XMAS|SAMX");
        var regexXmas = new Regex(@"XMAS");
        var regexSamx = new Regex(@"SAMX");

        var result = 0;
        
        var allLines = new List<string>();
        
        // horizontal lines
        allLines.AddRange(input);
        
        // vertical lines
        var columns = new List<string>();
        for (var y = 0; y < input.Length; y++)
        {
            var line = input[y];
            for (var x = 0; x < line.Length; x++)
            {
                // if the column does not exist, create it
                if (columns.Count <= x)
                {
                    columns.Add("");
                }
                
                // for each character along the x axis, add it to the column y axis
                // colums == lines, reversing the x and y axis
                columns[x] += line[x];
            }
        }
        allLines.AddRange(columns);
        
        // diagonal lines
        var diagonals = new List<string>();
        for (var y = 0; y < input.Length; y++)
        {
            var line = input[y];
            for (var x = 0; x < line.Length; x++)
            {
                // if the diagonal does not exist, create it
                if (diagonals.Count <= x + y)
                {
                    diagonals.Add("");
                }
                
                // for each character along the x axis, add it to the diagonal
                // diagonals == lines, reversing the x and y axis
                diagonals[x + y] += line[x];
            }
        }
        allLines.AddRange(diagonals);
        
        // diagonal lines reverse
        var diagonalsReverse = new List<string>();
        for (var y = 0; y < input.Length; y++)
        {
            // reverse the line
            var line = new string(input[y].Reverse().ToArray());
            
            for (var x = 0; x < line.Length; x++)
            {
                // if the diagonal does not exist, create it
                if (diagonalsReverse.Count <= x + y)
                {
                    diagonalsReverse.Add("");
                }
                
                // for each character along the x axis, add it to the diagonal
                // diagonals == lines, reversing the x and y axis
                diagonalsReverse[x + y] += line[x];
            }
        }
        allLines.AddRange(diagonalsReverse);
        
        // count matches
        for (var i = 0; i < allLines.Count; i++)
        {
            var count = regex.Matches(allLines[i]).Count;
            var countXmas = regexXmas.Matches(allLines[i]).Count;
            var countSamx = regexSamx.Matches(allLines[i]).Count;
            
            _logger.LogInformation("Found {Count} accurences of XMAS or SAMX in line {Line}", count, allLines[i]);
            
            result += countXmas + countSamx;
        }
        
        return result;
    }

    public long RunPart2(string[] input)
    {
        _logger.LogInformation("Solving - {Year} - Day {Day} - Part 2", _helper.GetYear(), _helper.GetDay());
        _logger.LogInformation("Input contains {Input} values", input.Length);

        throw new NotImplementedException();
    }
}
