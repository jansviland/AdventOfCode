using System.Diagnostics;

namespace AdventOfCode._2022.Day2;

public interface ISolutionService
{
    public int Run(string[] input);
    public int RunPart2(string[] input);
    public int CalculateRowPart1(string input);
    public int CalculateRowPart2(string input);
}

public enum HandShape
{
    Rock = 1,
    Paper = 2,
    Scissors = 3
}

public class SolutionService : ISolutionService
{
    private readonly ILogger<SolutionService> _logger;

    public SolutionService(ILogger<SolutionService> logger)
    {
        _logger = logger;
    }

    public int Run(string[] input)
    {
        _logger.LogInformation("Solving day 1 with input: {Input}", input);

        var sum = 0;
        for (int i = 0; i < input.Length; i++)
        {
            sum += CalculateRowPart1(input[i]);
        }

        return sum;
    }

    public int RunPart2(string[] input)
    {
        throw new NotImplementedException();
    }

    public int CalculateRowPart1(string input)
    {
        var split = input.Split(' ');
        var opponent = GetHandShape(split[0]);
        var yourResponse = GetHandShape(split[1]);

        // value from type
        var typePoints = (yourResponse) switch
        {
            HandShape.Rock => 1,
            HandShape.Paper => 2,
            HandShape.Scissors => 3,
            _ => throw new ArgumentOutOfRangeException(nameof(opponent), opponent, null)
        };

        // win, lose, draw
        var outcomePoints = (opponent, yourResponse) switch
        {
            (HandShape.Rock, HandShape.Scissors) => 0,
            (HandShape.Rock, HandShape.Rock) => 3,
            (HandShape.Rock, HandShape.Paper) => 6,

            (HandShape.Paper, HandShape.Rock) => 0,
            (HandShape.Paper, HandShape.Paper) => 3,
            (HandShape.Paper, HandShape.Scissors) => 6,

            (HandShape.Scissors, HandShape.Paper) => 0,
            (HandShape.Scissors, HandShape.Scissors) => 3,
            (HandShape.Scissors, HandShape.Rock) => 6,
            _ => throw new ArgumentOutOfRangeException()
        };

        _logger.LogInformation("Opponent choose {Opponent}, you respond with {YourResponse}, Outcome: {Outcome}",
            opponent, yourResponse, outcomePoints);

        return typePoints + outcomePoints;
    }

    public int CalculateRowPart2(string input)
    {
        throw new NotImplementedException();
    }

    private HandShape GetHandShape(string input)
    {
        return input switch
        {
            "A" or "X" => HandShape.Rock,
            "B" or "Y" => HandShape.Paper,
            "C" or "Z" => HandShape.Scissors,
            _ => throw new ArgumentOutOfRangeException(nameof(input), input, null)
        };
    }
}