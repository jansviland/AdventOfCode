namespace AdventOfCode._2023.Day10;

public interface ISolutionService
{
    public int RunPart1(string[] input);
    public Cell[,] GetAllDistances(Cell[,] grid);
    public int RunPart2(string[] input);
}

public class Cell : IComparable<Cell>
{
    public char Value { get; set; }
    public int X { get; set; }
    public int Y { get; set; }
    public int Distance { get; set; }

    public int CompareTo(Cell? other)
    {
        return Value.CompareTo(other.Value);
    }
}

public class SolutionService : ISolutionService
{
    private readonly ILogger<ISolutionService> _logger;

    public SolutionService(ILogger<SolutionService> logger)
    {
        _logger = logger;
    }

    public Cell[,] CreateGrid(string[] input)
    {
        var grid = new Cell[input.Length, input[0].Length];

        for (var row = 0; row < input.Length; row++)
        {
            string line = input[row];
            for (var col = 0; col < line.Length; col++)
            {
                var cell = new Cell
                {
                    Value = line[col],
                    X = row,
                    Y = col
                };

                grid[row, col] = cell;
            }
        }

        return grid;
    }

    public void PrintGrid(Cell[,] grid)
    {
        var sb = new StringBuilder();
        for (var row = 0; row < grid.GetLength(0); row++)
        {
            for (var col = 0; col < grid.GetLength(1); col++)
            {
                sb.Append(grid[row, col].Value);
            }
            _logger.LogInformation(sb.ToString());
            sb.Clear();
        }
    }

    public int RunPart1(string[] input)
    {
        _logger.LogInformation("Solving - 2023 - Day 10 - Part 1");
        _logger.LogInformation("Input contains {Input} values", input.Length);

        var grid = CreateGrid(input);
        PrintGrid(grid);

        var distances = GetAllDistances(grid);

        throw new NotImplementedException();
    }
    public Cell[,] GetAllDistances(Cell[,] grid)
    {
        // find S
        var start = Common.Grid.Find(grid, 'S');
        _logger.LogInformation("Start is at {Start}", start);

        var possiblePaths = new Stack<Cell>();
        possiblePaths.Push(start);

        var visited = new HashSet<Cell>();

        // loop through all possible paths, find next possible paths
        while (possiblePaths.Count > 0)
        {
            var current = possiblePaths.Peek();

            var x = current.X;
            var y = current.Y;

            // check if position is valid
            if (x < 0 || x >= grid.GetLength(0) || y < 0 || y >= grid.GetLength(1))
            {
                possiblePaths.Pop();
                continue;
            }

            visited.Add(current);
            possiblePaths.Pop();

            var type = grid[x, y].Value;
            var distance = grid[x, y].Distance + 1;

            switch (type)
            {
                case 'S':
                    possiblePaths.Push(new Cell { X = x + 1, Y = y, Distance = distance });
                    possiblePaths.Push(new Cell { X = x - 1, Y = y, Distance = distance });
                    possiblePaths.Push(new Cell { X = x, Y = y + 1, Distance = distance });
                    possiblePaths.Push(new Cell { X = x, Y = y - 1, Distance = distance });
                    break;
                case '|':
                {
                    possiblePaths.Push(new Cell { X = x, Y = y + 1, Distance = distance });
                    possiblePaths.Push(new Cell { X = x, Y = y - 1, Distance = distance });
                    break;
                }
                case '-':
                    possiblePaths.Push(new Cell { X = x + 1, Y = y, Distance = distance });
                    possiblePaths.Push(new Cell { X = x - 1, Y = y, Distance = distance });
                    break;
            }

            // remove visited from possible paths
            foreach (var visitedCell in visited)
            {
                if (possiblePaths.Contains(visitedCell))
                {
                    possiblePaths.Remove(visitedCell);
                }
            }

        }

        // var adjacentValues = Common.Grid.GetAdjacentValues(grid, start.Item1, start.Item2);


        throw new NotImplementedException();
    }

    public int RunPart2(string[] input)
    {
        _logger.LogInformation("Solving - 2023 - Day 10 - Part 2");
        _logger.LogInformation("Input contains {Input} values", input.Length);

        throw new NotImplementedException();
    }
}