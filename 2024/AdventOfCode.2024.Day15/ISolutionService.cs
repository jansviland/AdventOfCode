namespace AdventOfCode._2024.Day15;

public interface ISolutionService
{
    public long RunPart1(string[] input);
    public long RunPart2(string[] input);
}

public class SolutionService : ISolutionService
{
    private readonly ILogger<ISolutionService> _logger;
    private readonly Helper _helper = new();

    private Complex Up = -Complex.ImaginaryOne;
    private Complex Down = Complex.ImaginaryOne;
    private Complex Left = -1;
    private Complex Right = 1;

    public SolutionService(ILogger<SolutionService> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// Parse map input
    /// </summary>
    /// <returns>Dict with map, complex coordinate as key, char as value. And start pos</returns>
    public (Dictionary<Complex, char>, Complex) ParseMap(string[] input)
    {
        var map = (
            from y in Enumerable.Range(0, input.Length)
            from x in Enumerable.Range(0, input[0].Length)
            select new KeyValuePair<Complex, char>(Complex.ImaginaryOne * y + x, input[y][x])
        ).ToDictionary();

        var start = map.First(x => x.Value == '@').Key;

        return (map, start);
    }

    public void PrintMap(Dictionary<Complex, char> map)
    {
        var sb = new StringBuilder();
        sb.Append(Environment.NewLine);

        // Determine the bounds of the map
        int minX = (int)map.Keys.Min(c => c.Real);
        int maxX = (int)map.Keys.Max(c => c.Real);
        int minY = (int)map.Keys.Min(c => c.Imaginary);
        int maxY = (int)map.Keys.Max(c => c.Imaginary);

        for (var y = minY; y <= maxY; y++)
        {
            for (var x = minX; x <= maxX; x++)
            {
                Complex key = new Complex(x, y);
                if (map.TryGetValue(key, out char value))
                {
                    sb.Append(value);
                }
                else
                {
                    sb.Append(' ');
                }
                sb.Append(' ');
            }
            sb.Append(Environment.NewLine);
        }

        _logger.LogInformation(sb.ToString());
    }

    public long RunPart1(string[] input)
    {
        _logger.LogInformation("Solving - {Year} - Day {Day} - Part 1", _helper.GetYear(), _helper.GetDay());
        _logger.LogInformation("Input contains {Input} values", input.Length);

        var mapInput = input
            .TakeWhile(l => l.Contains('#'))
            .ToArray();

        var moveInput = input
            .SkipWhile(l => l.Contains('#'))
            .SkipWhile(string.IsNullOrWhiteSpace)
            .SelectMany(s => s) // convert strings to char
            .ToList();

        var (map, start) = ParseMap(mapInput);

        PrintMap(map);

        var updatedMap = Move(map, start, moveInput);

        throw new NotImplementedException();
    }

    Complex GetNextPosition(Complex pos, char move)
    {
        // attempt to move
        var nextPos = Complex.NaN;

        switch (move)
        {
            case '<': nextPos = pos + Left; break;
            case '>': nextPos = pos + Right; break;
            case '^': nextPos = pos - Complex.ImaginaryOne; break;
            case 'v': nextPos = pos + Complex.ImaginaryOne; break;
        }

        return nextPos;
    }

    Dictionary<Complex, char> Move(Dictionary<Complex, char> map, Complex pos, List<char> moves)
    {
        var step = 0;
        while (map.ContainsKey(pos) && step < moves.Count())
        {
            var move = moves[step];
            var nextPos = GetNextPosition(pos, move);

            if (map[nextPos] == '#')
            {
                // wall
            }
            else if (map[nextPos] == 'O')
            {
                // box
                // TODO: attempt to push box forward, check if '.' exist in direction, if so, move the empty space behind
                // and push box ahead one step
                var numberOfDotsInDirection = 0;
                var lookAhead = nextPos;

                // while (map.ContainsKey(lookAhead) && map[lookAhead] != '#')
                // {
                //     if (map[lookAhead] == '.')
                //     {
                //         numberOfDotsInDirection++;
                //     }
                // }
                //
                //
                // var nextNextPos = GetNextPosition(nextPos, move);
                // if (map.ContainsKey(nextNextPos) && map[nextNextPos] == '.')
                // {
                //     // swap box with .
                //     map[nextPos] = '.';
                //     map[nextNextPos] = 'O';
                //
                //     pos = nextPos;
                // }
                // else
                // {
                //     
                // }
                // while (map[GetNextPosition(boxPos, move) ])


            }
            else
            {
                // empty space
                pos = nextPos;
            }

            step++;

            _logger.LogInformation("Step: {Step}, Move {Move}", step, moves[step - 1]);

            // print move
            var current = map[pos] == '@' ? '.' : map[pos];
            map[pos] = '@';
            PrintMap(map);
            map[pos] = current;
        }

        return map;
    }

    public long RunPart2(string[] input)
    {
        _logger.LogInformation("Solving - {Year} - Day {Day} - Part 2", _helper.GetYear(), _helper.GetDay());
        _logger.LogInformation("Input contains {Input} values", input.Length);

        throw new NotImplementedException();
    }

}
