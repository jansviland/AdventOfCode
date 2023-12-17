namespace AdventOfCode._2023.Day16;

public class SolutionServiceV2 : ISolutionService
{
    private static readonly Complex Up = -Complex.ImaginaryOne; // - new Complex(0.0, -1.0);
    private static readonly Complex Down = Complex.ImaginaryOne; // new Complex(0.0, 1.0);
    private static readonly Complex Left = -Complex.One; // new Complex(-1.0, 0.0);
    private static readonly Complex Right = Complex.One; // new Complex(1.0, 0.0);

    // create a dictionary to store the grid
    private Dictionary<Complex, char> grid = new();

    // define a beam as a position and a direction
    private (Complex position, Complex direction) beam;

    public SolutionServiceV2()
    {

    }

    public long RunPart1(string[] input, bool animated = false)
    {
        var map = ParseInput(input);

        var count = EnergizedCells(map, (Complex.Zero, Right));

        return count;
    }

    int EnergizedCells(Dictionary<Complex, char> map, (Complex position, Complex direction) beam)
    {

        // this is essentially just a flood fill algorithm.
        var q = new Queue<(Complex position, Complex direction)>();
        q.Enqueue(beam);

        var seen = new HashSet<(Complex position, Complex direction)>();

        while (q.TryDequeue(out beam))
        {
            seen.Add(beam);
            foreach (var dir in Exits(map[beam.position], beam.direction))
            {
                var pos = beam.position + dir;
                if (map.ContainsKey(pos) && !seen.Contains((pos, dir)))
                {
                    q.Enqueue((pos, dir));
                }
            }
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
    public Dictionary<Complex, char> ParseInput(string[] input)
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
            select new KeyValuePair<Complex, char>(pos, cell)
        ).ToDictionary();
    }

    // given a cell and a direction, return the possible exits (new directions from that cell)
    public Complex[] Exits(char cell, Complex dir)
    {
        switch (cell)
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

    public long RunPart2(string[] input)
    {
        throw new NotImplementedException();
    }
}