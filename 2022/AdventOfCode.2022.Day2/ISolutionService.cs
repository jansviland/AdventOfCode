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
    Rock,
    Paper,
    Scissors
}

public enum Outcome
{
    Lose,
    Draw,
    Win
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

        // points for outcome
        var outcomePoints = GetOutcomePoints(opponent, yourResponse);

        // points from handshape selected
        var typePoints = GetTypePoints(yourResponse);

        return typePoints + outcomePoints;
    }

    private int GetOutcomePoints(HandShape opponent, HandShape yourResponse)
    {
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

        return outcomePoints;
    }

    private int GetTypePoints(HandShape handShape)
    {
        var typePoints = (handShape) switch
        {
            HandShape.Rock => 1,
            HandShape.Paper => 2,
            HandShape.Scissors => 3,
            _ => throw new ArgumentOutOfRangeException(nameof(handShape), handShape, null)
        };

        return typePoints;
    }

    public int CalculateRowPart2(string input)
    {
        var split = input.Split(' ');
        var opponent = GetHandShape(split[0]);
        var desiredOutcome = GetDesiredOutcome(split[1]);

        _logger.LogInformation("Opponent choose {Opponent}, you want {DesiredOutcome}",
            opponent, desiredOutcome);

        throw new NotImplementedException();
    }

    private Outcome GetDesiredOutcome(string input)
    {
        return input switch
        {
            "X" => Outcome.Lose,
            "Y" => Outcome.Draw,
            "Z" => Outcome.Win,
            _ => throw new ArgumentOutOfRangeException(nameof(input), input, null)
        };
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