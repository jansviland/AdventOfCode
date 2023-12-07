namespace AdventOfCode._2023.Day7;

public interface ISolutionService
{
    public int RunPart1(string[] input);
    public int RunPart2(string[] input);
}

public enum HandRank
{
    Unknown = 0,
    HighCard = 60,
    OnePair = 70,
    TwoPair = 80,
    ThreeOfAKind = 85,
    FullHouse = 90,
    FourOfAKind = 95,
    FiveOfAKind = 100
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
    public string CardsReplacedJoker { get; set; } = string.Empty;

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
                    const string order = "J23456789TQKA";
                    int index = order.IndexOf(c);
                    int otherIndex = order.IndexOf(otherCard);

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
        // group by card
        var groups = hand.Cards
            .GroupBy(x => x)
            .ToList();

        // all cards are the same, AAAAA, five of a kind
        if (groups.Count == 1)
        {
            return HandRank.FiveOfAKind.GetHashCode();
        }
        // only two groups, AAAAB, four of a kind or AAABB, full house
        else if (groups.Count == 2)
        {
            // check if four of a kind or full house
            if (groups.Any(x => x.Count() == 4))
            {
                return HandRank.FourOfAKind.GetHashCode();
            }

            return HandRank.FullHouse.GetHashCode();
        }
        // three groups, AAABC, three of a kind or AABBC, two pair
        else if (groups.Count == 3)
        {
            // check if three of a kind or two pair
            if (groups.Any(x => x.Count() == 3))
            {
                return HandRank.ThreeOfAKind.GetHashCode();
            }

            return HandRank.TwoPair.GetHashCode();
        }
        // four groups, AABCD, one pair
        else if (groups.Count == 4)
        {
            return HandRank.OnePair.GetHashCode();
        }
        // five groups, ABCDE, high card
        else if (groups.Count == 5)
        {
            return HandRank.HighCard.GetHashCode();
        }

        return HandRank.Unknown.GetHashCode();
    }

    public int GetRank(HandWithJoker hand)
    {
        int jokerAmount = hand.Cards.Count(x => x == 'J');

        // use a copy of the hand, to not change the original
        string handCopy = hand.Cards;

        if (jokerAmount > 0)
        {
            // can just replace the jokers with the most common card
            IGrouping<char, char>? mostCommonCard = handCopy
                .Where(x => x != 'J')
                .GroupBy(x => x)
                .MaxBy(x => x.Count());

            // JJJJJJ - five of a kind
            if (mostCommonCard == null)
            {
                hand.CardsReplacedJoker = "AAAAA";
                return HandRank.FiveOfAKind.GetHashCode();
            }

            // use handCopy to replace the jokers, keep the original hand since we still want to compare jokers to other cards later
            // if we replace 3 with J, it should be treated as 3 later. 
            char mostCommonCardChar = mostCommonCard.Key;
            handCopy = hand.Cards.Replace("J", mostCommonCardChar.ToString());

            hand.CardsReplacedJoker = handCopy;

            // _logger.LogInformation("Hand {Old} - {New}", old, hand.Cards);
        }

        var updatedHand = new Hand
        {
            Rank = 0,
            Cards = handCopy,
            Bet = hand.Bet
        };

        return GetRank(updatedHand);
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
            int handOrder = i + 1;
            var hand = hands[i];

            string? handType = Enum.GetName(typeof(HandRank), hand.Rank);

            if (hand.Cards.Count(x => x == 'J') > 1)
            {
                _logger.LogInformation("Joker found in hand {Hand}, new Hand {ReplacedHand}, Type {Type}, Rank {Rank}", hand.Cards, hand.CardsReplacedJoker, handType, handOrder);
            }

            count += hand.Bet * handOrder;
        }

        return count;
    }
}