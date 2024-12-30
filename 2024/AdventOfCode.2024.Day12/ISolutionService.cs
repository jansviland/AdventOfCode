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

    IRenderable CreateDisplay(IDictionary<Complex, char> garden, HashSet<Complex> visited = null, int perimeter = 0)
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

        if (visited == null)
        {
            return canvas;
        }
        
        // var canvas = CreateDisplay(garden, visited);
        var statusText = $"Region contains {visited?.Count() ?? 0} plots";
        var priceText = $"perimeter: " + perimeter + Environment.NewLine + "price: " + visited?.Count + " * " + perimeter + " = " + visited?.Count * perimeter;

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
                    CreateDisplay(currentPiece),
                    new Text(Environment.NewLine + priceText) 
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

    (IEnumerable<Complex> region, long price) GetRegionPrice(Dictionary<Complex, char> garden, Complex start, LiveDisplayContext? ctx = null)
    {
        var queue = new Queue<Complex>();
        queue.Enqueue(start);

        var region = new HashSet<Complex>();

        // flood fill algorithm
        while (queue.Any())
        {
            var pos = queue.Dequeue();
            region.Add(pos);

            foreach (var dir in new List<Complex>() { Up, Down, Left, Right })
            {
                var next = pos + dir;
                if (garden.TryGetValue(next, out var val)
                    && val == garden[start]
                    && !queue.Contains(next)
                    && !region.Contains(next))
                {
                    queue.Enqueue(pos + dir);
                }
            }

            // if (ctx != null)
            // {
            //     ctx.UpdateTarget(CreateDisplay(garden, visited));
            //     Thread.Sleep(1);
            // }
        }

        var perimeter = CalcPerimeter(region);

        if (ctx != null)
        {
            ctx.UpdateTarget(CreateDisplay(garden, region, perimeter));
            Thread.Sleep(100);
        }
        
        return (region, region.Count * perimeter);
    }

    int CalcPerimeter(IEnumerable<Complex> region)
    {
        var minX = region.Min(x => x.Real);
        var minY = region.Min(x => x.Imaginary);
        
        var reducedRegion = region.Select(x => new Complex(x.Real - minX, x.Imaginary - minY));

        var maxX = reducedRegion.Max(x => x.Real);
        var maxY = reducedRegion.Max(x => x.Imaginary);

        // calc left side: 
        var left = 0;
        var right = 0;
        for (var x = 0; x <= maxX; x++)
        {
            // get all elements on x axis
            var plots = reducedRegion
                .Where(a => (int)a.Real == x)
                .ToList();

            foreach (var p in plots)
            {
                if (!reducedRegion.Contains(p + Left))
                {
                    left++;
                }
                if (!reducedRegion.Contains(p + Right))
                {
                    right++;
                }
            }
        }

        var top = 0;
        var bottom = 0;
        for (var y = 0; y <= maxY; y++)
        {
            // get all elements on y axis
            var plots = reducedRegion
                .Where(a => (int)a.Imaginary == y);

            foreach (var p in plots)
            {
                if (!reducedRegion.Contains(p + Down))
                {
                    bottom++;
                }
                if (!reducedRegion.Contains(p + Up))
                {
                    top++;
                }
            }
        }
        
        // AnsiConsole.MarkupLine($"left: {left}, right: {right}, top: {top}, bottom: {bottom}");
        return left + right + top + bottom;
    }

    long GetRegions(Dictionary<Complex, char> garden, LiveDisplayContext? ctx = null)
    {
        // use a flood filling algorithm to move to similar plants and fill the regions
        var regions = new List<IEnumerable<Complex>>();

        var total = 0L;
        foreach (var plot in garden)
        {
            if (!regions.Exists(x => x.Contains(plot.Key)))
            {
                var (region, price) = GetRegionPrice(garden, plot.Key, ctx);

                regions.Add(region);
                total += price;
            }
        }

        return total;
    }
    
    public long RunPart1(string[] input, bool animate = false)
    {
        var map = Parse(input);
        if (animate)
        {
            var height = (int)map.Keys.Max(c => c.Imaginary);
            var width = (int)map.Keys.Max(c => c.Real);
            
            AnsiConsole.Live(new Canvas(width, height))
                .Start(ctx => { GetRegions(map, ctx); });

            return GetRegions(map);
        }
        else
        {
            return GetRegions(map);
        }
    }

    public long RunPart2(string[] input)
    {
        throw new NotImplementedException();
    }
}
