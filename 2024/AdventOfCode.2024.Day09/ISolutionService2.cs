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

        var disk = Parse(input[0]);
        var updatedDisk = ReOrder(disk, false);

        return Checksum(updatedDisk);
    }

    void Print(LinkedList<Block> disk)
    {
        var sb = new StringBuilder();
        foreach (var block in disk)
        {
            // repeat block id, length amount of times
            for (int i = 0; i < block.length; i++)
            {
                sb.Append(block.fileId == -1 ? "." : block.fileId.ToString());
            }
        }
        _logger.LogInformation(sb.ToString());
    }

    ulong Checksum(LinkedList<Block> disk)
    {
        ulong result = 0;
        var current = disk.First;
        ulong index = 0;

        while (current != null)
        {
            for (var i = 0; i < current.Value.length ; i++)
            {
                result += index * (ulong)current.Value.fileId;
                // _logger.LogInformation("{index} * {FileId}", index, current.Value.fileId);
                
                index++;
                
            }

            current = current.Next;
        }

        return result;
    }

    LinkedList<Block> ReOrder(LinkedList<Block> disk, bool wholeFiles = false)
    {
        var result = new LinkedList<Block>();
        // var leftOverEmptySpace = new LinkedList<Block>();

        // move from left to right, if empty space, move last element in linked-list to the front
        // if we move whole files, check that we have enough space first
        while (disk.Count > 0 && disk.First != null && disk.Last != null)
        {
            var first = disk.First.Value;
            var last = disk.Last.Value;

            // not empty space
            if (first.fileId != -1)
            {
                result.AddLast(new Block(first.fileId, first.length));
                disk.RemoveFirst();
            }
            else
            {
                // empty space
                if (last.fileId == -1)
                {
                    // leftOverEmptySpace.AddLast(new Block(last.fileId, last.length));
                    disk.RemoveLast();
                    // disk.RemoveFirst();
                }
                else if (first.length == last.length)
                {
                    result.AddLast(new Block(last.fileId, last.length));
                    disk.RemoveLast();
                    disk.RemoveFirst();
                }
                else if (first.length > last.length)
                {
                    result.AddLast(new Block(last.fileId, last.length));
                    disk.RemoveLast();
                    disk.RemoveFirst();

                    // TODO: add missing empty space
                    disk.AddFirst(new Block(-1, first.length - last.length));
                }
                else
                {
                    // empty space is smaller then the last block of files

                    // file
                    if (wholeFiles)
                    {
                        // we are not moving whole files
                        // disk.RemoveFirst();
                        // disk.RemoveLast();
                        
                        // TODO: 
                    }
                    else
                    {
                        // split block in two
                        var blockA = new Block(last.fileId, first.length);
                        var blockB = new Block(last.fileId, last.length - first.length);

                        // fill gap with block A and add the remainer to the back
                        result.AddLast(blockA);

                        // add empty space
                        // result.AddLast(new Block(-1, ))

                        disk.RemoveFirst();
                        disk.RemoveLast();
                        // disk.RemoveFirst();

                        disk.AddLast(blockB);
                    }
                }
            }

            _logger.LogInformation("----------------------------");
            Print(disk);
            Print(result);
        }

        return result;
    }

    public ulong RunPart2(string[] input)
    {
        _logger.LogInformation("Solving - {Year} - Day {Day} - Part 2", _helper.GetYear(), _helper.GetDay());
        _logger.LogInformation("Input contains {Input} values", input.Length);

        var disk = Parse(input[0]);
        var updatedDisk = ReOrder(disk, true);

        return Checksum(updatedDisk);
    }
}
