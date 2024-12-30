using Spectre.Console;
using Spectre.Console.Rendering;

namespace AdventOfCode._2024.Day12;

public interface ISolutionService
{
    public long RunPart1(string[] input, bool animate = false);
    public long RunPart2(string[] input);
}

public class SolutionService : ISolutionService
{
    private readonly ILogger<ISolutionService> _logger;
    private readonly Color[] _colors;

    private readonly Complex Up = -Complex.ImaginaryOne;
    private readonly Complex Down = Complex.ImaginaryOne;
    private readonly Complex Left = -1;
    private readonly Complex Right = 1;

    public SolutionService(ILogger<SolutionService> logger)
    {
        _logger = logger;

        // create colors for each A to Z
        _colors = new Color[26];
        for (var i = 0; i < 26; i++)
        {
            // Evenly distribute hues across the color wheel (0-360 degrees)
            var hue = (i / 26.0) * 360;
            _colors[i] = FromHue(hue);

            // _colors[i] = new Color((byte)(i * 10), (byte)(i * 10), (byte)(i * 10));
        }
    }

    private Color FromHue(double hue)
    {
        // Convert hue to RGB (simplified HSV approach with full saturation and value)
        var c = 1.0;
        var x = 1.0 - Math.Abs(hue / 60 % 2 - 1);
        var m = 0.0;

        double r = 0, g = 0, b = 0;
        if (hue < 60) (r, g, b) = (c, x, m);
        else if (hue < 120) (r, g, b) = (x, c, m);
        else if (hue < 180) (r, g, b) = (m, c, x);
        else if (hue < 240) (r, g, b) = (m, x, c);
        else if (hue < 300) (r, g, b) = (x, m, c);
        else (r, g, b) = (c, m, x);

        return new Color((byte)(r * 255), (byte)(g * 255), (byte)(b * 255));
    }

    IRenderable CreateSpectreCanvas(IDictionary<Complex, char> garden, HashSet<Complex> visited = null)
    {
        var maxX = (int)garden.Keys.Max(c => c.Real);
        var maxY = (int)garden.Keys.Max(c => c.Imaginary);

        var canvas = new Canvas(maxX + 1, maxY + 1);

        foreach (var (key, value) in garden)
        {
            var x = (int)key.Real;
            var y = (int)key.Imaginary;

            if (visited != null && visited.Contains(key))
            {
                canvas.SetPixel(x, y, Color.White);
            }
            else
            {
                // A to Z = 65 - 90
                canvas.SetPixel(x, y, _colors[90 - value]);
            }
        }

        // var canvas = CreateSpectreCanvas(garden, visited);
        var statusText = $"Region contains {visited?.Count() ?? 0} plots";

        if (visited == null)
        {
            return canvas;
        }

        var minX = visited.Min(x => x.Real);
        var minY = visited.Min(x => x.Imaginary);
        
        // var currentPiece = visited
        //     .Select(x => new KeyValuePair<Complex, char>(x, 'A'))
        //     .ToDictionary();

        var currentPiece = visited.ToDictionary(
            x => new Complex(x.Real - minX, x.Imaginary - minY),
            x => 'A');

        return new Table()
            .AddColumn("Garden")
            .AddColumn("Status")
            .AddRow(
                canvas,
                new Rows(
                    new Text(statusText + Environment.NewLine),
                    CreateSpectreCanvas(currentPiece)
                )
            );

        // return canvas;
    }

    Dictionary<Complex, char> Parse(string[] input)
    {
        var map = (
            from y in Enumerable.Range(0, input.Length)
            from x in Enumerable.Range(0, input[0].Length)
            select new KeyValuePair<Complex, char>(Complex.ImaginaryOne * y + x, input[y][x])).ToDictionary();

        return map;
    }

    IEnumerable<Complex> GetRegion(Dictionary<Complex, char> garden, Complex start, LiveDisplayContext? ctx = null)
    {
        var queue = new Queue<Complex>();
        queue.Enqueue(start);

        var visited = new HashSet<Complex>();

        // flood fill algorithm
        while (queue.Any())
        {
            var pos = queue.Dequeue();
            visited.Add(pos);

            foreach (var dir in new List<Complex>() { Up, Down, Left, Right })
            {
                var next = pos + dir;
                if (garden.TryGetValue(next, out var val)
                    && val == garden[start]
                    && !queue.Contains(next)
                    && !visited.Contains(next))
                {
                    queue.Enqueue(pos + dir);
                }
            }

            // if (ctx != null)
            // {
            //     ctx.UpdateTarget(CreateSpectreCanvas(garden, visited));
            //     Thread.Sleep(1);
            // }
        }

        if (ctx != null)
        {
            ctx.UpdateTarget(CreateSpectreCanvas(garden, visited));
            Thread.Sleep(100);
        }
        
        // TODO: return the: total price of fencing all regions on your map, not the visited cells

        return visited;
    }

    List<IEnumerable<Complex>> GetRegions(Dictionary<Complex, char> garden, LiveDisplayContext? ctx = null)
    {
        // use a flood filling algorithm to move to similar plants and fill the regions
        var regions = new List<IEnumerable<Complex>>();

        foreach (var plot in garden)
        {
            if (regions.Exists(x => x.Contains(plot.Key)))
            {
                // do nothing
            }
            else
            {
                // TODO: seperate this into another methood
                regions.Add(GetRegion(garden, plot.Key, ctx));
            }
        }

        return regions;
    }

    public long RunPart1(string[] input, bool animate = false)
    {
        var map = Parse(input);
        var canvas = CreateSpectreCanvas(map);

        // another flood fill algorithm? 
        // 1. Start in one corner,
        // 2. Check if we have already "filled" the area
        // 3. if not, take the char, lets say 'A', and flood all neighboors that are A, 
        // 4. now we have an area for a group of 'A's, add them all to a collection,
        //      a collection for an area contains all the Complex posistions of the elements in that area

        // 5. calc the value of this area, add to the sum, and move to the next element in the grid

        // for each element, add a boolean "filled" or "not filled"

        // AnsiConsole.Write(canvas);

        var regions = GetRegions(map);

        var height = (int)map.Keys.Max(c => c.Imaginary);
        var width = (int)map.Keys.Max(c => c.Real);

        if (animate)
        {
            AnsiConsole.Live(new Canvas(width, height))
                .Start(ctx => { GetRegions(map, ctx); });
        }

        throw new NotImplementedException();
    }

    public long RunPart2(string[] input)
    {
        throw new NotImplementedException();
    }
}
