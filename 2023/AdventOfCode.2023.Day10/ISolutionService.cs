namespace AdventOfCode._2023.Day10;

public interface ISolutionService
{
    public int RunPart1(string[] input);
    public Cell[,] GetAllDistances(Cell[,] grid, bool forwards = true);
    public int RunPart2(string[] input);
}

public enum Direction
{
    North,
    South,
    East,
    West
}

public class Cell : IComparable<Cell>
{
    public char Value { get; set; }
    public int X { get; set; }
    public int Y { get; set; }
    public int Distance { get; set; } = 0;
    public int DistanceToS { get; set; } = 0;

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
        var grid = new Cell[input[0].Length, input.Length];

        for (var y = 0; y < input.Length; y++)
        {
            string line = input[y];
            for (var x = 0; x < line.Length; x++)
            {
                var cell = new Cell
                {
                    Value = line[x],
                    X = x,
                    Y = y
                };

                grid[x, y] = cell;
            }
        }

        return grid;
    }

    public void PrintGrid(Cell[,] grid, int type = 0)
    {
        // var space = " ";
        var padding = 3;
        var sb = new StringBuilder();
        for (var y = 0; y < grid.GetLength(1); y++)
        {
            for (var x = 0; x < grid.GetLength(0); x++)
            {

                if (grid[x, y].Distance > 0 && type == 0)
                {
                    // var value = "#";
                    var value = (grid[x, y].Distance % 100).ToString();
                    // var value = (grid[x, y].DistanceToS % 100).ToString();
                    sb.Append(value.PadLeft(padding));
                }
                else if (grid[x, y].DistanceToS > 0 && type == 1)
                {
                    // var value = "#";
                    // var value = (grid[x, y].Distance % 100).ToString();
                    var value = (grid[x, y].DistanceToS % 100).ToString();
                    sb.Append(value.PadLeft(padding));
                }
                else
                {
                    var value = grid[x, y].Value.ToString();
                    // sb.Append(grid[x, y].Value + space);
                    sb.Append(value.PadLeft(padding));
                }
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

        // for each cell, find distance to S, using manhattan distance
        // the cell that is the furthest away is the answer (that has a distance > 0)

        Cell furthest = new Cell();
        var distances = GetAllDistances(grid, true);
        for (var y = 0; y < distances.GetLength(1); y++)
        {
            for (var x = 0; x < distances.GetLength(0); x++)
            {
                if (distances[x, y].Distance > furthest.Distance)
                {
                    furthest = distances[x, y];
                }
            }
        }

        _logger.LogInformation("Furthest is {X}, {Y} with distance {Distance}, steps {Distance}", furthest.X, furthest.Y, furthest.DistanceToS, furthest.Distance);

        return furthest.Distance;
    }

    public Cell Find(Cell[,] grid, char c)
    {
        for (var y = 0; y < grid.GetLength(1); y++)
        {
            for (var x = 0; x < grid.GetLength(0); x++)
            {
                if (grid[x, y].Value.Equals(c))
                {
                    return grid[x, y];
                }
            }
        }

        throw new Exception($"Could not find {c} in grid");
    }

    public Cell[,] GetAllDistances(Cell[,] grid, bool forwards = true)
    {
        // find S
        var start = Find(grid, 'S');
        _logger.LogInformation("Start is at {X}, {Y}", start.X, start.Y);

        var possiblePaths = new LinkedList<Cell>();
        possiblePaths.AddFirst(start);

        var visited = new HashSet<Cell>();

        var northConnectors = new List<char> { '|', 'F', '7' };
        var southConnectors = new List<char> { '|', 'L', 'J' };
        var westConnectors = new List<char> { '-', 'F', 'L' };
        var eastConnectors = new List<char> { '-', '7', 'J' };

        int step = 0;

        // loop through all possible paths, find next possible paths
        while (possiblePaths.Count > 0)
        {
            // can go in two directions, forward or backwards, by starting with Last or First
            var current = forwards ? possiblePaths.Last.Value : possiblePaths.First.Value;

            var x = current.X;
            var y = current.Y;

            // check if position is valid
            if (x < 0 || x >= grid.GetLength(0) || y < 0 || y >= grid.GetLength(1))
            {
                possiblePaths.Remove(current);
                continue;
            }

            visited.Add(current);
            possiblePaths.Remove(current);

            var distance = grid[x, y].Distance + 1;

            Cell? north = null;
            Cell? south = null;
            Cell? east = null;
            Cell? west = null;

            // north
            if (y - 1 >= 0)
            {
                north = grid[x, y - 1];
            }
            // south
            if (y + 1 < grid.GetLength(1))
            {
                south = grid[x, y + 1];
            }
            // west
            if (x - 1 >= 0)
            {
                west = grid[x - 1, y];
            }
            // east
            if (x + 1 < grid.GetLength(0))
            {
                east = grid[x + 1, y];
            }

            var directions = new List<Direction>();

            var type = grid[x, y].Value;
            switch (type)
            {
                case 'S':

                    directions.Add(Direction.North);
                    directions.Add(Direction.South);
                    directions.Add(Direction.East);
                    directions.Add(Direction.West);
                    break;

                case '|':
                {
                    directions.Add(Direction.North);
                    directions.Add(Direction.South);
                    break;
                }
                case '-':

                    directions.Add(Direction.East);
                    directions.Add(Direction.West);
                    break;

                case 'L':

                    directions.Add(Direction.North);
                    directions.Add(Direction.East);
                    break;

                case 'J':

                    directions.Add(Direction.North);
                    directions.Add(Direction.West);
                    break;

                case 'F':

                    directions.Add(Direction.South);
                    directions.Add(Direction.East);
                    break;

                case '7':

                    directions.Add(Direction.South);
                    directions.Add(Direction.West);
                    break;
            }

            // process directions
            if (directions.Contains(Direction.North))
            {
                // north
                if (north != null && !visited.Contains(north) && northConnectors.Contains(north.Value))
                {
                    grid[x, y - 1].Distance = distance;
                    grid[x, y - 1].DistanceToS = Math.Abs(start.X - x) + Math.Abs(start.Y - (y - 1));
                    possiblePaths.AddFirst(grid[x, y - 1]);
                }
            }
            if (directions.Contains(Direction.South))
            {
                // south
                if (south != null && !visited.Contains(south) && southConnectors.Contains(south.Value))
                {
                    grid[x, y + 1].Distance = distance;
                    grid[x, y + 1].DistanceToS = Math.Abs(start.X - x) + Math.Abs(start.Y - (y - 1));
                    possiblePaths.AddFirst(grid[x, y + 1]);
                }
            }
            if (directions.Contains(Direction.West))
            {
                // west
                if (west != null && !visited.Contains(west) && westConnectors.Contains(west.Value))
                {
                    grid[x - 1, y].Distance = distance;
                    grid[x - 1, y].DistanceToS = Math.Abs(start.X - x) + Math.Abs(start.Y - (y - 1));
                    possiblePaths.AddFirst(grid[x - 1, y]);
                }
            }
            if (directions.Contains(Direction.East))
            {
                // east
                if (east != null && !visited.Contains(east) && eastConnectors.Contains(east.Value))
                {
                    grid[x + 1, y].Distance = distance;
                    grid[x + 1, y].DistanceToS = Math.Abs(start.X - x) + Math.Abs(start.Y - (y - 1));
                    possiblePaths.AddFirst(grid[x + 1, y]);
                }
            }

            // _logger.LogInformation("Step {Step}", step);
            // _logger.LogInformation("--------------------");
            // PrintGrid(grid);
            // _logger.LogInformation("--------------------");
            step++;

        }

        PrintGrid(grid);

        return grid;
    }

    public int RunPart2(string[] input)
    {
        _logger.LogInformation("Solving - 2023 - Day 10 - Part 2");
        _logger.LogInformation("Input contains {Input} values", input.Length);

        throw new NotImplementedException();
    }
}