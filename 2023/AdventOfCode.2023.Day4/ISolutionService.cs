namespace AdventOfCode._2023.Day4;

public interface ISolutionService
{
    public int RunPart1(string[] input);
    public int RunPart2(string[] input);
}

public class SolutionService : ISolutionService
{
    private readonly ILogger<SolutionService> _logger;

    public SolutionService(ILogger<SolutionService> logger)
    {
        _logger = logger;
    }

    public int RunPart1(string[] input)
    {
        _logger.LogInformation("Solving - 2023 - Day 4 - Part 1");
        _logger.LogInformation("Input contains {Input} values", input.Length);

        var sum = 0;
        foreach (var line in input)
        {
            var split = line.Split(':');
            var numbers = split[1].Split('|');
            var winningNumbers = numbers[0]
                .Trim()
                .Split(' ')
                .Select(x => int.TryParse(x, out var result) ? result : -1)
                .ToArray();

            var drawnNumbers = numbers[1]
                .Trim()
                .Split(' ')
                .Select(x => int.TryParse(x, out var result) ? result : 0)
                .ToArray();

            var matches = winningNumbers
                .Intersect(drawnNumbers)
                .ToList();

            if (matches.Count == 0)
            {
                continue;
            }

            var count = 1;
            for (var i = 0; i < matches.Count - 1; i++)
            {
                count *= 2;
            }

            sum += count;
        }

        return sum;
    }

    public int RunPart2(string[] input)
    {
        _logger.LogInformation("Solving - 2023 - Day 4 - Part 2");
        _logger.LogInformation("Input contains {Input} values", input.Length);

        // dictinoary of card number and count of times we have copied it
        var cardDictionary = new Dictionary<int, int>();

        // add the first card to the dictionary
        for (var i = 1; i <= input.Length; i++)
        {
            cardDictionary.Add(i, 1);
        }

        var cardNumber = 1;
        foreach (var line in input)
        {
            var split = line.Split(':');
            var numbers = split[1].Split('|');
            var winningNumbers = numbers[0]
                .Trim()
                .Split(' ')
                .Select(x => int.TryParse(x, out var result) ? result : -1)
                .ToArray();

            var drawnNumbers = numbers[1]
                .Trim()
                .Split(' ')
                .Select(x => int.TryParse(x, out var result) ? result : 0)
                .ToArray();

            var matches = winningNumbers
                .Intersect(drawnNumbers)
                .ToList();

            var count = 1;
            for (var i = 0; i < matches.Count; i++)
            {
                var amountOfCurrentCard = cardDictionary[cardNumber];

                // for each match, we win copies of the same number of next cards as the number of matches
                // in addtion, if we already have several copies of the winning card, we win multiple times 
                var cardCount = cardDictionary[cardNumber + count];
                cardDictionary[cardNumber + count] = cardCount + amountOfCurrentCard;
                count++;
            }

            cardNumber++;
        }

        return cardDictionary.Sum(x => x.Value);
    }
}