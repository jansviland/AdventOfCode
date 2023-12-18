using System.Drawing;
using Pastel;

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
    private static readonly Dictionary<string, Complex> Directions = new()
    {
        { "U", -Complex.ImaginaryOne },
        { "D", Complex.ImaginaryOne },
        { "L", -Complex.One },
        { "R", Complex.One },
    };

    private readonly ILogger<ISolutionService> _logger;
    private static readonly StringBuilder _sb = new StringBuilder();

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

            // _logger.LogInformation("Direction: {Direction}, Meters: {Meters}, Color: {Color}", direction, meters, color);

            var plan = new DiggPlan
            {
                Direction = direction,
                Meters = meters,
                Color = color.Substring(1, color.Length - 2)
            };

            diggPlans.Add(plan);
        }

        // create grid with a dictionary, Complex (x, y) coordinates as key, DigPlan as value
        var grid = CreateGrid(diggPlans);

        PrintGrid(grid);

        throw new NotImplementedException();
    }

    private void PrintGrid(Dictionary<Complex, DiggPlan> grid)
    {
        var minX = grid.Keys.Min(x => x.Real);
        var maxX = grid.Keys.Max(x => x.Real);
        var minY = grid.Keys.Min(x => x.Imaginary);
        var maxY = grid.Keys.Max(x => x.Imaginary);

        for (int y = (int)minY; y <= (int)maxY; y++)
        {
            for (int x = (int)minX; x <= (int)maxX; x++)
            {
                var pos = new Complex(x, y);
                if (grid.TryGetValue(pos, out var diggPlan))
                {
                    // if (tile.Steps > 0)
                    // {
                    //     _sb.Append(tile.Steps.ToString().PadLeft(3, ' '));
                    // }
                    // else
                    // {
                    //     _sb.Append(tile.Value.ToString().PadLeft(3, ' '));
                    // }
                    // _sb.Append(tile.Color);

                    Console.Write("#".Pastel(diggPlan.Color));

                    // "colorize me".Pastel("#1E90FF");
                    // var consoleColor = new ConsoleColor();
                    // consoleColor = Pastel(Color.Fr
                    // Console.ForegroundColor = diggPlan.Direction
                }
                else
                {
                    Console.Write(" ");
                }

            }

            Console.WriteLine();

            // _sb.AppendLine();
        }

        // Console.WriteLine(_sb.ToString());
        // _sb.Clear();
    }

    private Dictionary<Complex, DiggPlan> CreateGrid(IEnumerable<DiggPlan> diggPlans)
    {
        var grid = new Dictionary<Complex, DiggPlan>();

        var currentPosition = Complex.Zero; // (0, 0)
        foreach (var diggPlan in diggPlans)
        {
            var position = currentPosition;
            var direction = diggPlan.Direction;
            for (var i = 0; i < diggPlan.Meters; i++)
            {
                position += direction;
                grid.Add(position, diggPlan);
            }

            currentPosition = position;
        }

        // order by x, then y
        grid = grid
            .OrderBy(x => x.Key.Real)
            .ThenBy(x => x.Key.Imaginary)
            .ToDictionary(x => x.Key, x => x.Value);

        return grid;
    }

    public long RunPart2(string[] input)
    {
        _logger.LogInformation("Solving - 2023 - Day 18 - Part 2");
        _logger.LogInformation("Input contains {Input} values", input.Length);

        throw new NotImplementedException();
    }
}