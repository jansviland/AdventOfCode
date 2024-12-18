namespace AdventOfCode._2024.Day09;

using Fs = LinkedList<Block>;
using Node = LinkedListNode<Block>;

// Ideal for representing lightweight, immutable data structures.
// Value type because it is declared as a struct, changes to a copy do not affect the original instance.
// The record keyword introduces value equality and can be made immutable by default 
record struct Block(int fileId, int length)
{
}

public interface ISolutionService2
{
    long RunPart1(string[] input);
    long RunPart2(string[] input);
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
    Fs Parse(string input) => new Fs(input.Select((ch, i) => new Block(i % 2 == 1 ? -1 : i / 2, ch - '0')));

    public long RunPart1(string[] input)
    {
        _logger.LogInformation("Solving - {Year} - Day {Day} - Part 1", _helper.GetYear(), _helper.GetDay());
        _logger.LogInformation("Input contains {Input} values", input.Length);

        return Checksum(ReOrder(Parse(input[0]), true));
    }
    
    public long RunPart2(string[] input)
    {
        _logger.LogInformation("Solving - {Year} - Day {Day} - Part 2", _helper.GetYear(), _helper.GetDay());
        _logger.LogInformation("Input contains {Input} values", input.Length);

        return Checksum(ReOrder(Parse(input[0]), false));
    }

    void Print(Fs disk)
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
        Console.WriteLine(sb.ToString());
        _logger.LogInformation(sb.ToString());
    }

    long Checksum(Fs disk)
    {
        var res = 0L;
        var l = 0L;

        for (var i = disk.First; i != null; i = i.Next)
        {
            for (var k = 0; k < i.Value.length; k++)
            {
                if (i.Value.fileId != -1)
                {
                    res += l * i.Value.fileId;
                }
                l++;
            }
        }

        return res;
    }

    Fs ReOrder(Fs disk, bool fragmentEnabled)
    {
        var (l, r) = (disk.First, disk.Last);
        while (l != r)
        {
            // check if left part is != -1 (empty), then move right (look for empty space on the left)
            // check if right part is -1 (empty), then move left, (look for non-empty on the right)
            // until we have an empty space on the left and a file on the right
            if (l.Value.fileId != -1)
            {
                l = l.Next;
            }
            else if (r.Value.fileId == -1)
            {
                r = r.Previous;
            }
            else
            {
                // left is empty, right is a file
                RelocateBlock(disk, l, r, fragmentEnabled);
                r = r.Previous;
                
                // Print(disk);
            }
        }

        // Print(disk);
        
        return disk;
    }

    private void RelocateBlock(Fs disk, Node left, Node right, bool fragmentsEnabled)
    {
        // move from left to the right of the linked list
        for (var i = left; i != right; i = i.Next)
        {
            if (i.Value.fileId != -1)
            {
                // do nothing
            }
            else if (i.Value.length == right.Value.length)
            {
                // if left and right are of equal size, swap
                (i.Value, right.Value) = (right.Value, i.Value);
                return;
            }
            else if (i.Value.length > right.Value.length)
            {
                // must calc diff before making changes to elements
                var diff = i.Value.length - right.Value.length;
                
                i.Value = right.Value; // move right element to the left 
                right.Value = right.Value with { fileId = -1 }; // set right element to empty space
                
                // add remainder after i
                disk.AddAfter(i, new Block(-1, diff));
                return;
            }
            else if (i.Value.length < right.Value.length && fragmentsEnabled)
            {
                var diff = right.Value.length - i.Value.length;
                i.Value = i.Value with { fileId = right.Value.fileId };
                right.Value = right.Value with { length = diff };
                disk.AddAfter(right, new Block(-1, i.Value.length));
            }
        }
    }

    Fs ReOrderOld(Fs disk, bool wholeFiles = false)
    {
        var result = new Fs();
        var leftOverEnd = new Fs();

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
                    leftOverEnd.AddFirst(new Block(last.fileId, last.length));
                    disk.RemoveLast();
                    // disk.RemoveFirst();
                }
                else if (first.length == last.length)
                {
                    result.AddLast(new Block(last.fileId, last.length));
                    disk.RemoveLast();
                    disk.RemoveFirst();
                    leftOverEnd.AddFirst(new Block(-1, first.length));
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
                        disk.RemoveLast();
                        leftOverEnd.AddFirst(new Block(last.fileId, last.length));
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
            Print(leftOverEnd);
        }

        return result;
    }

}
