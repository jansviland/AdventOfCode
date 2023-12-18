using Common;

namespace AdventOfCode._2023.Day16;

public class Tile
{
    public char Value { get; set; }
    public int Steps { get; set; }
}

public class SolutionServiceV2 : ISolutionService
{
    private static readonly Complex Up = -Complex.ImaginaryOne; // - new Complex(0.0, -1.0);
    private static readonly Complex Down = Complex.ImaginaryOne; // new Complex(0.0, 1.0);
    private static readonly Complex Left = -Complex.One; // new Complex(-1.0, 0.0);
    private static readonly Complex Right = Complex.One; // new Complex(1.0, 0.0);

    private static readonly StringBuilder _sb = new StringBuilder();

    private readonly ILogger<ISolutionService> _logger;

    public SolutionServiceV2(ILogger<SolutionServiceV2> logger)
    {
        _logger = logger;
    }

    public long RunPart1(string[] input, bool animated = false)
    {
        var grid = ParseInput(input);
        var count = EnergizedCells(grid, (Complex.Zero, Right));

        PrintGrid(grid);

        return count;
    }

    public long RunPart2(string[] input)
    {
        var grid = ParseInput(input);
        // grid.ToImmutableDictionary();


        // var max = 0;
        // var beams = GetStartBeams(grid);
        // foreach (var beam in beams)
        // {
        //     var gridCopy = grid.ToDictionary(x => x.Key, x => x.Value);
        //     var result = EnergizedCells(gridCopy, beam);
        //     _logger.LogInformation($"Starting beam at {beam.position} with direction {beam.direction} resulted in {result} cells being energized");
        //     max = Math.Max(max, result);
        //
        //     // PrintGrid(gridCopy, true);
        // }
        //
        // return max;

        return (from beam in GetStartBeams(grid) select EnergizedCells(grid, beam)).Max();
    }

    private IEnumerable<(Complex position, Complex direction)> GetStartBeams(Dictionary<Complex, Tile> grid)
    {
        // get the bottom right corner
        var br = grid.Keys.MaxBy(pos => pos.Imaginary + pos.Real);
        return new[]
        {
            // when x == max x, go left
            from pos in grid.Keys where pos.Real == br.Real select (pos, Left),

            // when x == 0, go right
            from pos in grid.Keys where pos.Real == 0 select (pos, Right),

            // when y == max y, go up
            from pos in grid.Keys where pos.Imaginary == br.Imaginary select (pos, Up),

            // when y == 0, go down
            from pos in grid.Keys where pos.Imaginary == 0 select (pos, Down),

        }.SelectMany(x => x);
    }

    private void PrintGrid(Dictionary<Complex, Tile> grid, bool animated = false)
    {
        if (animated)
        {
            Console.Clear();
            Console.SetCursorPosition(0, 0);
        }

        var minX = grid.Keys.Min(c => c.Real);
        var maxX = grid.Keys.Max(c => c.Real);
        var minY = grid.Keys.Min(c => c.Imaginary);
        var maxY = grid.Keys.Max(c => c.Imaginary);

        for (int y = (int)minY; y <= (int)maxY; y++)
        {
            for (int x = (int)minX; x <= (int)maxX; x++)
            {
                var pos = new Complex(x, y);
                if (grid.TryGetValue(pos, out var tile))
                {
                    if (tile.Steps > 0)
                    {
                        _sb.Append(tile.Steps.ToString().PadLeft(3, ' '));
                    }
                    else
                    {
                        _sb.Append(tile.Value.ToString().PadLeft(3, ' '));
                    }
                }
            }

            _sb.AppendLine();
        }

        Console.WriteLine(_sb.ToString());
        _sb.Clear();

        if (animated)
        {
            Thread.Sleep(10);
        }
    }

    private int EnergizedCells(Dictionary<Complex, Tile> grid, (Complex position, Complex direction) beam)
    {
        // this is essentially just a flood fill algorithm.
        var q = new Queue<(Complex position, Complex direction)>();
        q.Enqueue(beam);

        var seen = new HashSet<(Complex position, Complex direction)>();

        // var totalSteps = 0;

        while (q.TryDequeue(out beam))
        {
            seen.Add(beam);

            // increment the steps
            grid[beam.position].Steps++;

            foreach (var dir in Exits(grid[beam.position], beam.direction))
            {
                var pos = beam.position + dir;
                if (grid.ContainsKey(pos) && !seen.Contains((pos, dir)))
                {
                    q.Enqueue((pos, dir));
                }
            }

            // if (totalSteps % 100 == 0)
            // {
            //     PrintGrid(grid, true);
            // }
            //
            // totalSteps++;
        }

        return seen.Select(beam => beam.position).Distinct().Count();
    }

    /// <summary>
    /// instead of using a 2d array (two dimensional array), we use a dictionary with the key as a complex number
    /// where the real part is the x coordinate and the imaginary part is the y coordinate.
    /// 
    /// from the documentation: https://learn.microsoft.com/en-us/dotnet/api/system.numerics.complex?view=net-8.0
    /// The Complex type uses the Cartesian coordinate system (real, imaginary) when instantiating and manipulating complex numbers.
    /// A complex number can be represented as a point in a two-dimensional coordinate system, which is known as the complex plane. 
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public Dictionary<Complex, Tile> ParseInput(string[] input)
    {
        // var dictionary = new Dictionary<Complex, char>();
        // for (int irow = 0; irow < input.Length; irow++)
        // {
        //     for (int icol = 0; icol < input[irow].Length; icol++)
        //     {
        //         var cell = input[irow][icol];
        //         var pos = new Complex(icol, irow);
        //         dictionary.Add(pos, cell);
        //     }
        // }

        return (
            from irow in Enumerable.Range(0, input.Length)
            from icol in Enumerable.Range(0, input[0].Length)
            let cell = input[irow][icol]
            let pos = new Complex(icol, irow)
            let tile = new Tile
            {
                Value = cell,
                Steps = 0
            }
            select new KeyValuePair<Complex, Tile>(pos, tile)
        ).ToDictionary();
    }

    // given a cell and a direction, return the possible exits (new directions from that cell)
    public Complex[] Exits(Tile tile, Complex dir)
    {
        switch (tile.Value)
        {
            case '-' when dir == Up || dir == Down:
                return new[] { Left, Right };
            case '|' when dir == Left || dir == Right:
                return new[] { Up, Down };
            case '/':
                // rotate 90 degrees
                return new[] { -new Complex(dir.Imaginary, dir.Real) };
            case '\\':
                // rotate -90 degrees
                return new[] { new Complex(dir.Imaginary, dir.Real) };
            default:
                return new[] { dir };
        }
    }

}