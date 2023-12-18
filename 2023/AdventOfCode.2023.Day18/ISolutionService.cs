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
    private static readonly Complex Up = -Complex.ImaginaryOne; // - new Complex(0.0, -1.0);
    private static readonly Complex Down = Complex.ImaginaryOne; // new Complex(0.0, 1.0);
    private static readonly Complex Left = -Complex.One; // new Complex(-1.0, 0.0);
    private static readonly Complex Right = Complex.One; // new Complex(1.0, 0.0);

    private static readonly Dictionary<string, Complex> Directions = new()
    {
        { "U", -Complex.ImaginaryOne },
        { "D", Complex.ImaginaryOne },
        { "L", -Complex.One },
        { "R", Complex.One },
    };

    private readonly HashSet<Complex> _visited = new();
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

        // FloodFillRecursive(grid, new Complex(1, 1));

        // PrintGrid(grid);

        // count number of cells with digg plan
        FloodFill(grid, new Complex(1, 1));

        PrintGrid(grid);

        return grid.Count;
    }

    private void FloodFillRecursive(Dictionary<Complex, DiggPlan> grid, Complex current)
    {
        if (_visited.Contains(current))
        {
            return;
        }

        _visited.Add(current);

        var minX = grid.Keys.Min(x => x.Real);
        var maxX = grid.Keys.Max(x => x.Real);
        var minY = grid.Keys.Min(x => x.Imaginary);
        var maxY = grid.Keys.Max(x => x.Imaginary);

        if (current.Real >= minX && current.Real <= maxX && current.Imaginary >= minY && current.Imaginary <= maxY)
        {
            if (grid.TryGetValue(current, out var _))
            {
                return;
            }

            grid.Add(current, new DiggPlan
            {
                Direction = Complex.Zero,
                Meters = -1,
                Color = "#329390"
            });

            FloodFillRecursive(grid, current + Left);
            FloodFillRecursive(grid, current + Right);
            FloodFillRecursive(grid, current + Up);
            FloodFillRecursive(grid, current + Down);
        }
    }

    IEnumerable<Complex> GetAdjecent(Dictionary<Complex, DiggPlan> grid, Complex pos)
    {
        var minX = grid.Keys.Min(x => x.Real);
        var maxX = grid.Keys.Max(x => x.Real);
        var minY = grid.Keys.Min(x => x.Imaginary);
        var maxY = grid.Keys.Max(x => x.Imaginary);

        var dx = new[] { 0, 0, -1, 1 }; // up, down, left, right
        var dy = new[] { -1, 1, 0, 0 };

        for (var i = 0; i < dx.Length; i++)
        {
            var x = pos.Real + dx[i];
            var y = pos.Imaginary + dy[i];
            var newPos = new Complex(x, y);
            if (x >= minX && x <= maxX && y >= minY && y <= maxY)
            {
                yield return newPos;
            }
        }
    }

    private void FloodFill(Dictionary<Complex, DiggPlan> grid, Complex start)
    {
        var queue = new Queue<Complex>();
        queue.Enqueue(start);

        var visited = new HashSet<Complex>();

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();
            if (visited.Contains(current))
            {
                continue;
            }

            visited.Add(current);

            if (grid.TryGetValue(current, out var _))
            {
                // found a wall
                continue;
            }
            else
            {
                grid.Add(current, new DiggPlan
                {
                    Direction = Complex.Zero,
                    Meters = -1,
                    Color = "#FF0000"
                });
            }

            var adjecent = GetAdjecent(grid, current);
            foreach (var adj in adjecent)
            {
                queue.Enqueue(adj);
            }
        }

        grid = grid
            .OrderBy(x => x.Key.Real)
            .ThenBy(x => x.Key.Imaginary)
            .ToDictionary(x => x.Key, x => x.Value);
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
                    Console.Write("#".Pastel(diggPlan.Color));
                    // _logger.LogInformation("#");
                }
                else
                {
                    Console.Write(" ");
                    // _logger.LogInformation(" ");
                }
            }

            Console.WriteLine();
            // _logger.LogInformation("");
        }
    }

    private void PrintGrid(Dictionary<Complex, char> grid)
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
                if (grid.TryGetValue(pos, out var val))
                {
                    Console.Write(val);
                }
                else
                {
                    Console.Write(" ");
                }
            }

            Console.WriteLine();
        }
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