namespace AdventOfCode._2023.Day18;

public interface ISolutionService
{
    public long RunPart1(string[] input);
    public long RunPart2(string[] input);
}

public class DiggPlan
{
    public Complex Direction { get; set; }
    public int Meters { get; set; }
    public string Color { get; set; }
}

public class SolutionService : ISolutionService
{
    public static readonly Dictionary<string, Complex> Directions = new()
    {
        { "U", -Complex.ImaginaryOne },
        { "D", Complex.ImaginaryOne },
        { "L", -Complex.One },
        { "R", Complex.One },
    };

    private readonly ILogger<ISolutionService> _logger;

    public SolutionService(ILogger<SolutionService> logger)
    {
        _logger = logger;
    }

    public long RunPart1(string[] input)
    {
        _logger.LogInformation("Solving - 2023 - Day 18 - Part 1");
        _logger.LogInformation("Input contains {Input} values", input.Length);

        // (Complex position, Complex direction) = (Complex.Zero, Directions["E"]);

        var diggPlans = new List<DiggPlan>();
        foreach (string line in input)
        {
            var split = line.Split(" ");
            var direction = Directions[split[0]];
            var meters = int.Parse(split[1]);
            var color = split[2];

            _logger.LogInformation("Direction: {Direction}, Meters: {Meters}, Color: {Color}", direction, meters, color);

            var plan = new DiggPlan
            {
                Direction = direction,
                Meters = meters,
                Color = color
            };

            diggPlans.Add(plan);
        }

        // create grid with a dictionary, Complex (x, y) coordinates as key, DigPlan as value
        var grid = new Dictionary<Complex, DiggPlan>();

        throw new NotImplementedException();
    }

    public long RunPart2(string[] input)
    {
        _logger.LogInformation("Solving - 2023 - Day 18 - Part 2");
        _logger.LogInformation("Input contains {Input} values", input.Length);

        throw new NotImplementedException();
    }
}