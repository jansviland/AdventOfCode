namespace AdventOfCode._2024.Day09;

// using LL = System.Collections.Generic.LinkedList<Block>;
// using Node = System.Collections.Generic.LinkedListNode<Block>;

public interface ISolutionService2
{
    ulong RunPart1(string[] input);
    ulong RunPart2(string[] input);

    // string ParseLine(string line);
    // string ReOrder(string line);
    // ulong CalcChecksum(string line);
    // string ReOrderPart2(string line);
}

// Ideal for representing lightweight, immutable data structures.
// Value type because it is declared as a struct, changes to a copy do not affect the original instance.
// The record keyword introduces value equality and can be made immutable by default 
record struct Block(int fileId, int length)
{
}

public class SolutionService2 : ISolutionService2
{
    private readonly ILogger<ISolutionService2> _logger;
    private readonly Helper _helper = new();

    public SolutionService2(ILogger<SolutionService2> logger)
    {
        _logger = logger;
    }

    // The digits alternate between indicating the length of a file and the length of free space.
    // i % 2 == 1 (odd numbers), i % 2 == 0 (even numbers)
    // the current index is every other number, since every other number is free space, so we set fileId to i/2
    // to get the number from the char element we can do char - '0'
    LinkedList<Block> Parse(string input) => new LinkedList<Block>(input.Select((ch, i) => new Block(i % 2 == 1 ? -1 : i / 2, ch - '0')));

    public ulong RunPart1(string[] input)
    {
        _logger.LogInformation("Solving - {Year} - Day {Day} - Part 1", _helper.GetYear(), _helper.GetDay());
        _logger.LogInformation("Input contains {Input} values", input.Length);

        var fs = Parse(input[0]);

        // TODO: move last element to first, where there is space (.)

        throw new NotImplementedException();
    }

    public ulong RunPart2(string[] input)
    {
        _logger.LogInformation("Solving - {Year} - Day {Day} - Part 2", _helper.GetYear(), _helper.GetDay());
        _logger.LogInformation("Input contains {Input} values", input.Length);

        throw new NotImplementedException();
    }
}
