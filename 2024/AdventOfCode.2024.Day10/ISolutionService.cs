using System.Globalization;
using Spectre.Console;

namespace AdventOfCode._2024.Day10;

public interface ISolutionService
{
    public long RunPart1(string[] input, bool animate = false);
    public long RunPart2(string[] input, bool animate = false);
}

public static class CharExtentions
{
    public static int ConvertToInt(this char c)
    {
        // https://stackoverflow.com/questions/239103/convert-char-to-int-in-c-sharp
        return c - '0';
    }
}

public class SolutionService : ISolutionService
{
    private readonly ILogger<ISolutionService> _logger;
    private readonly Helper _helper = new();

    private readonly Complex Up = -Complex.ImaginaryOne;
    private readonly Complex Down = Complex.ImaginaryOne;
    private readonly Complex Left = -Complex.One;
    private readonly Complex Right = Complex.One;

    // Create a live display to update the grid
    private LiveDisplay _liveDisplay;
    private int _count = 0;

    public SolutionService(ILogger<SolutionService> logger)
    {
        _logger = logger;
    }

    Canvas PrintSpectre(ImmutableDictionary<Complex, int> map, HashSet<Complex>? visited = null)
    {
        // Determine the boundaries of the map
        var maxX = (int)map.Keys.Max(c => c.Real);
        var maxY = (int)map.Keys.Max(c => c.Imaginary);

        // Create a canvas
        var canvas = new Canvas(maxX + 1, maxY + 1);

        // Base color for the gradient
        var baseColor = Color.Blue;

        // Helper function to adjust brightness
        Color AdjustBrightness(Color baseColor, float factor)
        {
            // Blend the base color with white to create a lighter shade
            var r = (byte)(baseColor.R + (255 - baseColor.R) * factor);
            var g = (byte)(baseColor.G + (255 - baseColor.G) * factor);
            var b = (byte)(baseColor.B + (255 - baseColor.B) * factor);

            return new Color(r, g, b);
        }

        // Populate the canvas with the map values
        foreach (var (key, value) in map)
        {
            var x = (int)key.Real;
            var y = (int)key.Imaginary;

            // Calculate a brightness factor for the current value (0-9)
            var brightnessFactor = (float)value / 9; // Scale value to range [0, 1]

            // Adjust the base color brightness
            var adjustedColor = AdjustBrightness(baseColor, brightnessFactor);

            // Set the pixel color
            canvas.SetPixel(x, y, adjustedColor);
        }

        // Highlight visited nodes
        if (visited != null)
        {
            foreach (var pos in visited)
            {
                canvas.SetPixel((int)pos.Real, (int)pos.Imaginary, Color.Red);
            }
        }

        // Render the canvas
        // AnsiConsole.Write(canvas);

        return canvas;

        // Render the live display
        // _liveDisplay.Update(canvas);
    }

    void PrintBlock(Dictionary<Complex, int> map)
    {
        // Determine the boundaries of the map
        var maxX = (int)map.Keys.Max(c => c.Real);
        var maxY = (int)map.Keys.Max(c => c.Imaginary);

        // Base color for the gradient
        var baseColor = Color.Blue;

        // Helper function to adjust brightness
        Color AdjustBrightness(Color baseColor, float factor)
        {
            var r = (byte)(baseColor.R + (255 - baseColor.R) * factor);
            var g = (byte)(baseColor.G + (255 - baseColor.G) * factor);
            var b = (byte)(baseColor.B + (255 - baseColor.B) * factor);
            return new Color(r, g, b);
        }

        // Render the grid using Unicode block characters
        for (var y = 0; y <= maxY; y++)
        {
            for (var x = 0; x <= maxX; x++)
            {
                var key = new Complex(x, y);
                if (map.TryGetValue(key, out int value))
                {
                    // Calculate a brightness factor for the current value (0-9)
                    var brightnessFactor = (float)value / 9;

                    // Adjust the base color brightness
                    var adjustedColor = AdjustBrightness(baseColor, brightnessFactor);

                    // Write the block character with the adjusted color
                    AnsiConsole.Markup($"[{adjustedColor.ToMarkup()}]█[/]");
                }
                else
                {
                    // Render a default placeholder for missing values
                    AnsiConsole.Markup("[grey].[/]");
                }
            }
            // Newline after each row
            AnsiConsole.WriteLine();
        }
    }

    void Print(Dictionary<Complex, int> map)
    {
        var sb = new StringBuilder();
        sb.AppendLine();

        var maxX = map.Keys.Max(c => c.Real);
        var maxY = map.Keys.Max(c => c.Imaginary);

        for (var y = 0; y <= maxY; y++)
        {
            for (var x = 0; x <= maxX; x++)
            {
                var key = new Complex(x, y);
                sb.Append(map.TryGetValue(key, out int value) ? value.ToString(CultureInfo.InvariantCulture) : '.');
                sb.Append(' ');
            }
            sb.AppendLine();
        }

        _logger.LogInformation(sb.ToString());
    }

    ImmutableDictionary<Complex, int> Parse(string[] input)
    {
        var map = (
            from y in Enumerable.Range(0, input.Length)
            from x in Enumerable.Range(0, input[0].Length)
            select new KeyValuePair<Complex, int>(Complex.ImaginaryOne * y + x, input[y][x].ConvertToInt())
        ).ToImmutableDictionary();

        return map;
    }

    IEnumerable<Complex> GetTrailHeads(ImmutableDictionary<Complex, int> map) => map.Keys.Where(pos => map[pos] == 0);

    List<Complex> GetTrailFrom(ImmutableDictionary<Complex, int> map, Complex start, LiveDisplayContext? ctx = null)
    {
        Queue<Complex> positions = new Queue<Complex>();
        positions.Enqueue(start);

        List<Complex> trails = new List<Complex>();
        HashSet<Complex> visited = new HashSet<Complex>();

        while (positions.Any())
        {
            var pos = positions.Dequeue();
            visited.Add(pos);

            // check if we are at a top (9)
            if (map[pos] == 9)
            {
                trails.Add(pos);
                _count++;
            }
            else
            {
                foreach (var dir in new List<Complex> { Up, Down, Right, Left })
                {
                    // check all directions, make sure they exist in map and also, we can just increase by 1, so check if neigboor is current value + 1
                    if (map.GetValueOrDefault(pos + dir) == map[pos] + 1)
                    {
                        positions.Enqueue(pos + dir);
                    }
                }
            }

            if (ctx != null && visited.Count() > 3)
            {
                // AnsiConsole.WriteLine($"Found {_count} trails");
                ctx.UpdateTarget(PrintSpectre(map, visited));
                Thread.Sleep(5);
            }
        }

        // if (ctx != null && trails.Count != 0)
        // {
        //     ctx.UpdateTarget(PrintSpectre(map, visited));
        //     AnsiConsole.WriteLine("Found trail heads: " + string.Join(", ", trails));
        //     Thread.Sleep(50);
        // }

        return trails;
    }

    Dictionary<Complex, List<Complex>> GetAllTrails(string[] input, LiveDisplayContext? ctx = null)
    {
        _count = 0;
        var map = Parse(input);
        return GetTrailHeads(map).ToDictionary(t => t, t => GetTrailFrom(map, t, ctx));
    }

    public long RunPart1(string[] input, bool animate = false)
    {
        _logger.LogInformation("Solving - {Year} - Day {Day} - Part 1", _helper.GetYear(), _helper.GetDay());
        _logger.LogInformation("Input contains {Input} values", input.Length);

        var map = Parse(input);
        var height = (int)map.Keys.Max(c => c.Imaginary);
        var width = (int)map.Keys.Max(c => c.Real);

        if (animate)
        {
            AnsiConsole.Live(new Canvas(width, height))
                .Start(ctx => { GetAllTrails(input, ctx).Sum(x => x.Value.Distinct().Count()); });
        }

        return GetAllTrails(input).Sum(x => x.Value.Distinct().Count());
    }

    public long RunPart2(string[] input, bool animate = false)
    {
        _logger.LogInformation("Solving - {Year} - Day {Day} - Part 2", _helper.GetYear(), _helper.GetDay());
        _logger.LogInformation("Input contains {Input} values", input.Length);

        return GetAllTrails(input).Sum(x => x.Value.Count());
    }
}
