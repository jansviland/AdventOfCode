using Spectre.Console;

namespace AdventOfCode._2024.Day12;

public interface ISolutionService
{
    public long RunPart1(string[] input);
    public long RunPart2(string[] input);
}

record struct Plot(char plant, bool isFilled = false);

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

    Canvas CreateSpectreCanvas(IDictionary<Complex, Plot> garden)
    {
        var maxX = (int)garden.Keys.Max(c => c.Real);
        var maxY = (int)garden.Keys.Max(c => c.Imaginary);

        var canvas = new Canvas(maxX + 1, maxY + 1);

        foreach (var (key, value) in garden)
        {
            var x = (int)key.Real;
            var y = (int)key.Imaginary;

            // A to Z = 65 - 90
            canvas.SetPixel(x, y, _colors[90 - value.plant]);
        }

        return canvas;
    }

    Dictionary<Complex, Plot> Parse(string[] input)
    {
        var map = (
            from y in Enumerable.Range(0, input.Length)
            from x in Enumerable.Range(0, input[0].Length)
            select new KeyValuePair<Complex, Plot>(Complex.ImaginaryOne * y + x, new Plot(input[y][x]))).ToDictionary();

        return map;
    }

    List<List<Complex>> GetRegions(Dictionary<Complex, Plot> garden)
    {
        // use a flood filling algorithm to move to similar plants and fill the regions
        var regions = new List<List<Complex>>();

        foreach (var plot in garden)
        {
            if (plot.Value.isFilled)
            {
                // do nothing
            }
            else
            {
                // TODO: seperate this into another methood
                
                // fill
                // positions to flood
                var queue = new Queue<Complex>();
                queue.Enqueue(plot.Key);

                while (queue.Count > 0)
                {
                    foreach (var dir in new List<Complex>() { Up, Down, Left, Right })
                    {
                        if (garden.TryGetValue(plot.Key + dir, out Plot val) && val.plant == plot.Value.plant)
                        {
                            queue.Enqueue(plot.Key + dir);
                        }

                    }
                }
            }
        }

    }

    public long RunPart1(string[] input)
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

        AnsiConsole.Write(canvas);

        throw new NotImplementedException();
    }

    public long RunPart2(string[] input)
    {
        throw new NotImplementedException();
    }
}
