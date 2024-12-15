namespace AdventOfCode._2024.Day06;

// inspired by encse
public class SolutionService2 : ISolutionService
{
    private readonly ILogger<ISolutionService> _logger;
    private readonly Helper _helper = new();

    public SolutionService2(ILogger<SolutionService> logger)
    {
        _logger = logger;
    }

    public long RunPart1(string[] input)
    {
        _logger.LogInformation("Solving - {Year} - Day {Day} - Part 1", _helper.GetYear(), _helper.GetDay());
        _logger.LogInformation("Input contains {Input} values", input.Length);

        var (map, start) = Parse(input);

        return Walk(map, start).visited.Count();
    }

    public void PrintMap(Dictionary<Complex, char> map, int height, int width, HashSet<Complex> visited)
    {
        var sb = new StringBuilder();
        sb.Append(Environment.NewLine);

        for (var y = 0; y <= height; y++)
        {
            for (var x = 0; x <= width; x++)
            {
                Complex key = new Complex(x, y);
                if (visited.Contains(key))
                {
                    sb.Append('X');
                }
                else if (map.TryGetValue(key, out char value))
                {
                    sb.Append(value);
                }
                else
                {
                    sb.Append(' ');
                }
            }
            sb.Append(Environment.NewLine);
        }

        _logger.LogInformation(sb.ToString());
    }

    /// <summary>
    /// Parse input like this:
    ///
    ///     ....#.....
    ///     .........#
    ///     ..........
    ///     ..#.......
    ///     .......#..
    ///     ..........
    ///     .#..^.....
    ///     ........#.
    ///     #.........
    ///     ......#...
    /// </summary>
    /// <returns>Tuple with two types, one Dictonary that has Complex Cooridnate as key and char as value, and the starting position</returns>
    public (Dictionary<Complex, char> map, Complex start) Parse(string[] input)
    {
        var map = (
            from y in Enumerable.Range(0, input.Length)
            from x in Enumerable.Range(0, input[0].Length)
            select new KeyValuePair<Complex, char>(Complex.ImaginaryOne * y + x, input[y][x])
        ).ToDictionary();

        var start = map.First(a => a.Value == '^').Key;

        return (map, start);
    }

    // walk through maze, if we hit a wall '#', turn right. Also keep track of all our steps
    // also check if we are in a loop (this is true if we have been the same place twice with the same direction)
    // then we will do the same thing again and walk in a loop
    (IEnumerable<Complex> visited, bool isLoop) Walk(Dictionary<Complex, char> map, Complex pos)
    {
        var seen = new HashSet<(Complex pos, Complex dir)>(); // store positions and directions
        var dir = -Complex.ImaginaryOne; // current direction

        // while pos exist on map and we have not been in the same position moving in the same direction
        while (map.ContainsKey(pos) && !seen.Contains((pos, dir)))
        {
            seen.Add((pos, dir));
            if (map.GetValueOrDefault(pos + dir) == '#')
            {
                dir *= Complex.ImaginaryOne; // (0, 1)
                // multiplying up direction (0, -1) with (0, 1) -> (1, 0) right
                // multiplying right direction (1, 0) with (0, 1) -> (0, 1) down
                // multiplying down direction (0, 1) with (0, 1) -> (-1, 0) left
            }
            else
            {
                pos += dir;

            }
        }

        // PrintMap(
        //     map,
        //     (int)map.Keys.Max(x => x.Imaginary),
        //     (int)map.Keys.Max(x => x.Real),
        //     seen.Select(tuple => tuple.pos).ToHashSet());

        return (
            visited: seen.Select(s => s.pos).Distinct(), // all positions
            isLoop: seen.Contains((pos, dir)) // if we have been on the same position with the same direction before, it will result in a loop
        );
    }

    public long RunPart2(string[] input)
    {
        _logger.LogInformation("Solving - {Year} - Day {Day} - Part 2", _helper.GetYear(), _helper.GetDay());
        _logger.LogInformation("Input contains {Input} values", input.Length);

        var (map, start) = Parse(input);
        var visited = Walk(map, start).visited;
        var loops = 0;

        // go through each visited position, check if the position is empty . and attempt to place a wall
        // then check if this becomes a loop
        foreach (var block in visited.Where(pos => map[pos] == '.'))
        {
            map[block] = '#'; // replace empty with wall
            if (Walk(map, start).isLoop) // check if the original walk with modification result in loop
            {
                loops++;
            }
            map[block] = '.'; // set pos it back to empty again
        }

        return loops;
    }

}
