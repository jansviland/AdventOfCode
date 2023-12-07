namespace AdventOfCode._2023.Day7;

public interface ISolutionService
{
    public int RunPart1(string[] input);
    public int RunPart2(string[] input);
}

public class HandBase
{
    public string Cards { get; set; } = string.Empty;
    public int Rank { get; set; }
    public int Bet { get; set; }
}

public class Hand : HandBase, IComparable<Hand>
{
    public int CompareTo(Hand? other)
    {
        if (other == null)
        {
            return 1;
        }

        if (Rank != other.Rank)
        {
            return Rank.CompareTo(other.Rank);
        }
        else if (Rank == other.Rank)
        {
            for (var i = 0; i < Cards.Length; i++)
            {
                char c = Cards[i];
                char otherCard = other.Cards[i];
                if (c != otherCard)
                {
                    // set the order of cards here
                    var order = "23456789TJQKA";
                    var index = order.IndexOf(c);
                    var otherIndex = order.IndexOf(otherCard);

                    return index.CompareTo(otherIndex);
                }
            }
        }

        return 0;
    }
}

public class HandWithJoker : HandBase, IComparable<HandWithJoker>
{
    public int CompareTo(HandWithJoker? other)
    {
        if (other == null)
        {
            return 1;
        }

        if (Rank != other.Rank)
        {
            return Rank.CompareTo(other.Rank);
        }
        else if (Rank == other.Rank)
        {
            for (var i = 0; i < Cards.Length; i++)
            {
                char c = Cards[i];
                char otherCard = other.Cards[i];
                if (c != otherCard)
                {
                    // set the order of cards here
                    var order = "J23456789TQKA";
                    var index = order.IndexOf(c);
                    var otherIndex = order.IndexOf(otherCard);

                    return index.CompareTo(otherIndex);
                }
            }
        }

        return 0;
    }
}

public class SolutionService : ISolutionService
{
    private readonly ILogger<ISolutionService> _logger;

    public SolutionService(ILogger<SolutionService> logger)
    {
        _logger = logger;
    }

    public int GetRank(Hand hand)
    {
        var rank = 0;

        // group by card
        var groups = hand.Cards
            .GroupBy(x => x)
            .ToList();

        // all cards are the same, AAAAA, five of a kind
        if (groups.Count == 1)
        {
            return 100;
        }
        // only two groups, AAAAB, four of a kind or AAABB, full house
        else if (groups.Count == 2)
        {
            // check if four of a kind or full house
            if (groups.Any(x => x.Count() == 4))
            {
                // four of a kind
                return 95;
            }

            return 90;
        }
        // three groups, AAABC, three of a kind or AABBC, two pair, 
        else if (groups.Count == 3)
        {
            // check if three of a kind or two pair
            if (groups.Any(x => x.Count() == 3))
            {
                // three of a kind
                return 85;
            }

            return 80;
        }
        // four groups, AABCD, one pair
        else if (groups.Count == 4)
        {
            // check if one pair
            return 70;
        }
        // five groups, ABCDE, high card
        else if (groups.Count == 5)
        {
            // check if high card
            return 60;
        }

        return 0;
    }

    public int GetRank(HandWithJoker hand)
    {
        var rank = 0;

        var jokerAmount = hand.Cards.Count(x => x == 'J');

        // group by card
        var groups = hand.Cards
            .GroupBy(x => x)
            .ToList();

        // all cards are the same, AAAAA, five of a kind
        if (groups.Count == 1)
        {
            return 100;
        }
        // only two groups, AAAA4, four of a kind or AAA44, full house, AAAJ4, four of a kind with joker
        else if (groups.Count == 2)
        {
            // check if four of a kind or full house
            if (groups.Any(x => x.Count() == 4))
            {
                // four of a kind
                return 95;
            }

            return 90;
        }
        // three groups, AAABC, three of a kind or AABBC, two pair, 
        else if (groups.Count == 3)
        {
            // check if three of a kind or two pair
            if (groups.Any(x => x.Count() == 3))
            {
                // three of a kind
                return 85;
            }

            return 80;
        }
        // four groups, AABCD, one pair
        else if (groups.Count == 4)
        {
            // check if one pair
            return 70;
        }
        // five groups, ABCDE, high card
        else if (groups.Count == 5)
        {
            if (jokerAmount > 0)
            {
                // if one joker, then it's a pair
                if (jokerAmount == 1)
                {
                    return 70;
                }

                // if two jokers, then it's a three of a kind
                if (jokerAmount == 2)
                {
                    return 85;
                }
            }

            return 60;
        }

        return 0;
    }

    public int RunPart1(string[] input)
    {
        _logger.LogInformation("Solving - 2023 - Day 7 - Part 1");
        _logger.LogInformation("Input contains {Input} values", input.Length);

        var hands = new List<Hand>();

        foreach (var line in input)
        {
            var hand = new Hand();
            var split = line.Split(" ");
            hand.Cards = split[0];
            hand.Bet = int.Parse(split[1]);

            hand.Rank = GetRank(hand);

            hands.Add(hand);
        }

        hands.Sort();

        var count = 0;
        for (var i = 0; i < hands.Count; i++)
        {
            var rank = i + 1;
            var hand = hands[i];

            count += hand.Bet * rank;
        }

        return count;
    }

    public int RunPart2(string[] input)
    {
        _logger.LogInformation("Solving - 2023 - Day 7 - Part 2");
        _logger.LogInformation("Input contains {Input} values", input.Length);

        var hands = new List<HandWithJoker>();

        foreach (var line in input)
        {
            var hand = new HandWithJoker();
            var split = line.Split(" ");
            hand.Cards = split[0];
            hand.Bet = int.Parse(split[1]);

            hand.Rank = GetRank(hand);

            hands.Add(hand);
        }

        hands.Sort();

        var count = 0;
        for (var i = 0; i < hands.Count; i++)
        {
            var rank = i + 1;
            var hand = hands[i];

            if (hand.Cards.Count(x => x == 'J') > 0)
            {
                _logger.LogInformation("Joker found in hand {Hand}, Rank {Rank}", hand.Cards, rank);
            }

            count += hand.Bet * rank;
        }

        return count;
    }
}