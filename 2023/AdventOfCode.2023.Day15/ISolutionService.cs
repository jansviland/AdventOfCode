using Microsoft.Extensions.Primitives;

namespace AdventOfCode._2023.Day15;

public interface ISolutionService
{
    public long RunPart1(string[] input);
    public long Hash(string line, long multiplier = 17, long modulo = 256);
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
        var steps = line.Split(',');

        long sum = 0;
        foreach (string step in steps)
        {
            var hash = Hash(step);
            sum += hash;
        }

        return sum;
    }

    public long Hash(string line, long multiplier = 17, long modulo = 256)
    {
        long current = 0;

        for (var i = 0; i < line.Length; i++)
        {
            var c = line[i];
            var ascii = (int)c;

            current += ascii;
            current *= multiplier;
            current = current % modulo;
        }

        return current;
    }

    public long RunPart2(string[] input)
    {
        _logger.LogInformation("Solving - 2023 - Day 15 - Part 2");
        _logger.LogInformation("Input contains {Input} values", input.Length);

        var line = input[0];
        var steps = line.Split(',');

        Box?[] boxes = new Box[256];
        var lensesWithoutBox = new List<Lens>();

        foreach (string step in steps)
        {
            _logger.LogInformation("----------- Step: {Step} --------------", step);

            if (step.IndexOf('=') > 0)
            {
                var parts = step.Split('=');
                var label = parts[0].Trim();
                var value = parts[1].Trim();

                var boxNumber = Convert.ToInt32(Hash(label));

                // check if lens already exist, then just add box to it
                var lens = lensesWithoutBox.Find(l => l.label == label);
                if (lens == null)
                {
                    // if we don't have a lens, create one, with current box number
                    lens = new Lens()
                    {
                        label = label,
                        focalLength = int.Parse(value),
                        boxNumber = boxNumber
                    };
                }
                else
                {
                    // if we have a lens, just update it with correct focal length, box number is already set
                    lens.focalLength = int.Parse(value);
                    lensesWithoutBox = lensesWithoutBox.Where(l => l.label != label).ToList();
                }

                if (boxes[lens.boxNumber] != null)
                {
                    // check if box already contain a lens with the same label, if so, remove it and add it to lensesWithoutBox
                    var findLens = boxes[lens.boxNumber].Lenses.Find(l => l.label == label);
                    if (findLens != null)
                    {
                        lensesWithoutBox.Add(findLens);
                        boxes[lens.boxNumber].Lenses.Remove(findLens);

                        // add it to the front
                        boxes[lens.boxNumber].Lenses.Insert(0, lens);

                        // remove lens with same label from lensesWithoutBox
                        lensesWithoutBox = lensesWithoutBox.Where(l => l.label != label).ToList();
                    }
                    else
                    {
                        // add new lens to box, to the back
                        boxes[lens.boxNumber].Lenses.Add(lens);
                        lensesWithoutBox = lensesWithoutBox.Where(l => l.label != label).ToList();
                    }

                    Print(boxes);
                    continue;
                }

                // create new box
                var newBox = new Box()
                {
                    number = lens.boxNumber,
                };
                newBox.Lenses.Add(lens);

                boxes[lens.boxNumber] = newBox;
            }
            else
            {
                var parts = step.Split('-');
                var label = parts[0].Trim();

                var boxNumber = Convert.ToInt32(Hash(label));

                // find box, with lens with label, then remove it
                var box = boxes[boxNumber];
                if (box != null)
                {
                    var findLens = box.Lenses.Find(l => l.label == label);
                    if (findLens != null)
                    {
                        box.Lenses.Remove(findLens);

                        findLens.boxNumber = boxNumber;
                        lensesWithoutBox.Add(findLens);

                        lensesWithoutBox = lensesWithoutBox.Where(l => l.label != label).ToList();
                    }
                    else
                    {
                        // we know the box number, but not the focal length, store if for later
                        var lens = new Lens()
                        {
                            label = label,
                            focalLength = -1,
                            boxNumber = boxNumber
                        };
                        lensesWithoutBox.Add(lens);
                    }
                }
            }

            Print(boxes);

        }

        Print(boxes);

        foreach (var lens in lensesWithoutBox)
        {
            _logger.LogInformation("Lens without box: {Lens}", lens.label);
        }

        var total = 0;

        var sb = new StringBuilder();
        foreach (var box in boxes)
        {
            if (box != null && box.Lenses.Count > 0)
            {
                var sum = 0;
                for (var i = 0; i < box.Lenses.Count; i++)
                {
                    var lense = box.Lenses[i];
                    sum += (box.number + 1) * (i + 1) * lense.focalLength;

                    // rn: 1 (box 0) * 1 (first slot) * 1 (focal length) = 1
                    sb.AppendLine(lense.label + ": " + (box.number + 1) + " (box " + box.number + ") * " + (i + 1) + " (slot " + i + ") * " + lense.focalLength + " (focal length) = " + sum);
                }

                total += sum;
            }
        }

        _logger.LogInformation(sb.ToString());

        return total;
    }

    public void Print(Box[] boxes)
    {
        var sb = new StringBuilder();
        foreach (var box in boxes)
        {
            if (box != null && box.Lenses.Count > 0)
            {
                sb.AppendLine();
                sb.Append("Box: " + box.number + ": ");

                foreach (var lens in box.Lenses)
                {
                    sb.Append("[" + lens.label + ": " + lens.focalLength + "]");
                }
            }
        }

        sb.AppendLine();
        _logger.LogInformation(sb.ToString());
    }
}

public class Box
{
    public int number { get; set; }

    public List<Lens> Lenses { get; set; } = new List<Lens>();
}

public class Lens
{
    public int boxNumber { get; set; }
    public string label { get; set; }
    public int focalLength { get; set; }
}