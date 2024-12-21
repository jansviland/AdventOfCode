using System.Collections;
using System.Globalization;
using System.Runtime.Intrinsics.X86;
using Common;
using Spectre.Console;

namespace AdventOfCode._2024.Day10;

public interface ISolutionService
{
    public long RunPart1(string[] input);
    public long RunPart2(string[] input);
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

    public SolutionService(ILogger<SolutionService> logger)
    {
        _logger = logger;
    }

    void PrintSpectre(Dictionary<Complex, int> map)
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

        // Render the canvas
        AnsiConsole.Write(canvas);
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

    Dictionary<Complex, int> Parse(string[] input)
    {
        var map = (
            from y in Enumerable.Range(0, input.Length)
            from x in Enumerable.Range(0, input[0].Length)
            select new KeyValuePair<Complex, int>(Complex.ImaginaryOne * y + x, input[y][x].ConvertToInt())
        ).ToDictionary();

        return map;
    }

    public long RunPart1(string[] input)
    {
        _logger.LogInformation("Solving - {Year} - Day {Day} - Part 1", _helper.GetYear(), _helper.GetDay());
        _logger.LogInformation("Input contains {Input} values", input.Length);

        var map = Parse(input);
        // Print(map);
        PrintSpectre(map);
        // PrintBlock(map);

        throw new NotImplementedException();
    }

    public long RunPart2(string[] input)
    {
        _logger.LogInformation("Solving - {Year} - Day {Day} - Part 2", _helper.GetYear(), _helper.GetDay());
        _logger.LogInformation("Input contains {Input} values", input.Length);

        throw new NotImplementedException();
    }
}
