namespace AdventOfCode._2024.Day16;

public interface ISolutionService
{
    public long RunPart1(string[] input);
    public long RunPart2(string[] input);
}

public class SolutionService : ISolutionService
{
    private readonly ILogger<ISolutionService> _logger;
    private readonly Helper _helper = new();

    public SolutionService(ILogger<SolutionService> logger)
    {
        _logger = logger;
    }

    (Dictionary<Complex, char> maze, Complex start, Complex end) Parse(string[] input)
    {
        var maze = (
            from y in Enumerable.Range(0, input.Length)
            from x in Enumerable.Range(0, input[0].Length)
            select new KeyValuePair<Complex, char>(Complex.ImaginaryOne * y + x, input[y][x])
        ).ToDictionary();

        var start = maze.First(x => x.Value == 'S').Key;
        var end = maze.First(x => x.Value == 'E').Key;

        return (maze, start, end);
    }

    void PrintMaze(Dictionary<Complex, char> maze, List<Complex> path = null)
    {
        int width = (int)maze.Keys.Max(pos => pos.Real) + 1;
        int height = (int)maze.Keys.Max(pos => pos.Imaginary) + 1;

        var sb = new StringBuilder();
        sb.Append(Environment.NewLine);

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                var pos = new Complex(x, y);
                if (path != null && path.Contains(pos))
                {
                    Console.Write('*'); // Merk stien
                    sb.Append('*');
                }
                else if (maze.ContainsKey(pos))
                {
                    Console.Write(maze[pos]);
                    sb.Append(maze[pos]);
                }
                else
                {
                    Console.Write(' '); // Tomme områder utenfor labyrinten
                    sb.Append(' ');
                }
                sb.Append(' ');
            }
            Console.WriteLine();
            sb.Append(Environment.NewLine);
        }

        _logger.LogInformation(sb.ToString());
    }

    // list of each path, path consist of position and direction
    List<List<(Complex, Complex)>> FindAllPaths(Dictionary<Complex, char> maze, Complex start, Complex end)
    {
        var allPaths = new List<List<(Complex pos, Complex dir)>>();
        var currentPath = new List<(Complex pos, Complex dir)>();
        var visited = new HashSet<Complex>();

        // recursive function
        void DFS((Complex pos, Complex dir) current)
        {
            currentPath.Add(current);
            visited.Add(current.pos);

            if (current.pos == end)
            {
                // count how much this path is "worth", only save the one that gets the lowest amount of points
                allPaths.Add(new List<(Complex pos, Complex dir)>(currentPath));
            }
            else
            {
                // Define possible directions
                var directions = new List<Complex>
                {
                    new Complex(0, 1), // Up
                    new Complex(1, 0), // Right
                    new Complex(0, -1), // Down
                    new Complex(-1, 0) // Left
                };

                // Explore each direction
                foreach (var direction in directions)
                {
                    var neighbor = current.pos + direction;
                    if (IsValidMove(maze, neighbor, visited))
                    {
                        DFS((neighbor, direction)); // Recursive call
                    }
                }
            }

            // Backtrack: remove current position from path and mark as unvisited
            currentPath.RemoveAt(currentPath.Count - 1);
            visited.Remove(current.pos);
        }

        DFS((start, new Complex(1, 0)));

        return allPaths;
    }

    static bool IsValidMove(Dictionary<Complex, char> maze, Complex position, HashSet<Complex> visited)
    {
        return maze.ContainsKey(position) // Position exists in the maze
               && maze[position] != '#' // Not a wall
               && !visited.Contains(position); // Not already visited
    }

    (List<Complex> path, int steps, int turns) BFS(Dictionary<Complex, char> maze, Complex start, Complex end)
    {
        var queue = new Queue<Complex>();
        var visited = new HashSet<Complex>();
        var parent = new Dictionary<Complex, Complex>();

        queue.Enqueue(start);
        visited.Add(start);
        parent[start] = start; // Start har ingen forelder

        // Definer bevegelser: opp, ned, venstre, høyre
        var directions = new Complex[]
        {
            new Complex(0, 1), // Opp
            new Complex(0, -1), // Ned
            new Complex(-1, 0), // Venstre
            new Complex(1, 0) // Høyre
        };

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();

            if (current == end)
            {
                // Rekonstruer stien
                var path = new List<Complex>();
                while (!current.Equals(start))
                {
                    path.Add(current);
                    current = parent[current];
                }
                path.Add(start);
                path.Reverse();

                var turnCount = 0;
                Complex? previousDirection = null;

                foreach (var node in path.Skip(1)) // Skip first element since no previous direction to compare
                {
                    var currentDirection = node - parent[node]; // Get direction of movement
                    if (previousDirection.HasValue)
                    {
                        var angle = AngleBetween(previousDirection.Value, currentDirection);
                        if (Math.Abs(angle) == 90) // 90-degree turn detected
                        {
                            turnCount++;
                        }
                    }
                    previousDirection = currentDirection; // Update direction
                }

                return (path, path.Count, turnCount);
            }

            foreach (var direction in directions)
            {
                var neighbor = current + direction;

                // Sjekk om naboen er i labyrinten, ikke en vegg og ikke besøkt
                if (maze.ContainsKey(neighbor) && maze[neighbor] != '#' && !visited.Contains(neighbor))
                {
                    queue.Enqueue(neighbor);
                    visited.Add(neighbor);
                    parent[neighbor] = current;
                }
            }
        }

        return ([], 0, 0);
    }

    // check angle between two Complex numbers 
    static double AngleBetween(Complex a, Complex b)
    {
        // Using dot product to compute the angle
        double dot = (a.Real * b.Real) + (a.Imaginary * b.Imaginary);
        double magA = Math.Sqrt(a.Real * a.Real + a.Imaginary * a.Imaginary);
        double magB = Math.Sqrt(b.Real * b.Real + b.Imaginary * b.Imaginary);

        double cosAngle = dot / (magA * magB);
        return Math.Acos(cosAngle) * (180.0 / Math.PI); // Convert radian to degree
    }

    public long RunPart1(string[] input)
    {
        _logger.LogInformation("Solving - {Year} - Day {Day} - Part 1", _helper.GetYear(), _helper.GetDay());
        _logger.LogInformation("Input contains {Input} values", input.Length);

        var (maze, start, end) = Parse(input);

        PrintMaze(maze);

        var (path, steps, turns) = BFS(maze, start, end);

        PrintMaze(maze, path);

        // var allPaths = FindAllPaths(maze, start, end);

        // foreach (var path in allPaths)
        // {
        //     // PrintMaze(maze, path);
        // }

        // BUG: says 9 turns, it should be 7
        return turns * 1000 + steps;
    }

    public long RunPart2(string[] input)
    {
        _logger.LogInformation("Solving - {Year} - Day {Day} - Part 2", _helper.GetYear(), _helper.GetDay());
        _logger.LogInformation("Input contains {Input} values", input.Length);

        throw new NotImplementedException();

    }
}
