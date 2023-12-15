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
            // _logger.LogInformation("Hash: {Hash}", hash);
            // _logger.LogInformation("------------------------------------");

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

            // _logger.LogInformation("Current Value: {Current}, Char: {Char}, ASCII {Ascii}", current, c, ascii);

            current += ascii;
            // _logger.LogInformation("Increase Value to: {Current}", current);

            current *= multiplier;
            // _logger.LogInformation("Multiply Value to: {Current}", current);

            current = current % modulo;
            // _logger.LogInformation("Modulo Value to: {Current}", current);
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
        var lenses = new List<Lens>();

        // var boxNumber = 0;
        foreach (string step in steps)
        {
            if (step.IndexOf('=') > 0)
            {
                var parts = step.Split('=');
                var label = parts[0].Trim();
                var value = parts[1].Trim();

                var boxNumber = Convert.ToInt32(Hash(label));

                _logger.LogInformation("Box: {Box}, Label: {Label}, Value: {Value}", boxNumber, label, value);

                // check if lens already exist, then just add box to it
                var lens = lenses.Find(l => l.label == label);
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
                }

                if (boxes[lens.boxNumber] != null)
                {
                    boxes[lens.boxNumber].Lenses.Add(lens);
                    continue;
                }

                // create new box
                var newBox = new Box()
                {
                    number = lens.boxNumber,
                };
                newBox.Lenses.Add(lens);

                boxes[lens.boxNumber] = newBox;

                _logger.LogInformation("----------- Step {Step} --------------", boxNumber);
                Print(boxes);

                // boxNumber++;
            }
            else
            {
                var parts = step.Split('-');
                var label = parts[0].Trim();
                
                var boxNumber = Convert.ToInt32(Hash(label));

                _logger.LogInformation("Box: {Box}, Label: {Label}", boxNumber, label);

                // find box, with lens with label, then remove it
                foreach (var box in boxes)
                {
                    if (box != null)
                    {
                        var findLens = box.Lenses.Find(l => l.label == label);
                        if (findLens != null)
                        {
                            box.Lenses.Remove(findLens);

                            findLens.boxNumber = boxNumber;
                            lenses.Add(findLens);
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
                            lenses.Add(lens);
                        }

                        break;
                    }
                }

                _logger.LogInformation("----------- Step {Step} --------------", boxNumber);
                Print(boxes);
            }
        }

        throw new NotImplementedException();
    }

    public void Print(Box[] boxes)
    {
        var sb = new StringBuilder();
        foreach (var box in boxes)
        {
            if (box != null)
            {
                sb.AppendLine();
                sb.Append("Box: ");
                sb.Append(box.number);
                sb.Append(": ");

                foreach (var lens in box.Lenses)
                {
                    sb.Append("[" + lens.label + ": " + lens.focalLength + "]");
                }
                // _logger.LogInformation("Box: {Box}, Lenses: {Lenses}", box.number, box.Lenses);

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