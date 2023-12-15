namespace AdventOfCode._2023.Day15;

public interface ISolutionService
{
    public long RunPart1(string[] input);
    public long Hash(string line);
    public long RunPart2(string[] input);
}

public class SolutionService : ISolutionService
{
    private readonly ILogger<ISolutionService> _logger;

    public SolutionService(ILogger<SolutionService> logger)
    {
        _logger = logger;
    }

    public long RunPart1(string[] input)
    {
        _logger.LogInformation("Solving - 2023 - Day 15 - Part 1");
        _logger.LogInformation("Input contains {Input} values", input.Length);

        var line = input[0];
        var hash = Hash(line);

        return hash;
    }

    public long Hash(string line)
    {
        long current = 0;

        for (var i = 0; i < line.Length; i++)
        {
            var c = line[i];
            var ascii = (int)c;

            // _logger.LogInformation("Current Value: {Current}, Char: {Char}, ASCII {Ascii}", current, c, ascii);

            current += ascii;
            // _logger.LogInformation("Increase Value to: {Current}", current);

            current *= 17;
            // _logger.LogInformation("Multiply Value to: {Current}", current);

            current = current % 256;
            // _logger.LogInformation("Modulo Value to: {Current}", current);
        }

        return current;
    }

    public long RunPart2(string[] input)
    {
        _logger.LogInformation("Solving - 2023 - Day 15 - Part 2");
        _logger.LogInformation("Input contains {Input} values", input.Length);

        throw new NotImplementedException();
    }
}