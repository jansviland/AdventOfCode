namespace AdventOfCode._2023.Day10;

public interface ISolutionService
{
    public int RunPart1(string[] input);
    public Cell[,] GetAllDistances(Cell[,] grid, bool forwards = true);
    public bool IsInside(Cell cell, Cell[,] grid);
    public Cell[,] CreateGrid(string[] input);
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
    public bool IsPipe()
    {
        var pipeValues = new char[] { '|', 'F', '7', '-', 'L', 'J', };
        return pipeValues.Contains(Value);
    }

    public bool IsInsidePipe { get; set; } = false;

    public char Value { get; set; }
    public int X { get; set; }
    public int Y { get; set; }
    public int Steps { get; set; } = 0;
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
        var countFill = 0;

        const int padding = 3;
        var sb = new StringBuilder();
        for (var y = 0; y < grid.GetLength(1); y++)
        {
            for (var x = 0; x < grid.GetLength(0); x++)
            {
                if (grid[x, y].Steps > 0 && type == 0)
                {
                    var value = (grid[x, y].Steps % 100).ToString();
                    sb.Append(value.PadLeft(padding));

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(value.PadLeft(padding));
                }
                else if (grid[x, y].DistanceToS > 0 && type == 1)
                {
                    var value = (grid[x, y].DistanceToS % 100).ToString();
                    sb.Append(value.PadLeft(padding));

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(value.PadLeft(padding));
                }
                else
                {
                    // sb.Append(grid[x, y].Value + space);
                    var value = grid[x, y].Value.ToString();

                    if (grid[x, y].IsPipe())
                    {
                        // var value = grid[x, y].Value.ToString();
                        sb.Append(value.PadLeft(padding));

                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write(value.PadLeft(padding));
                    }
                    else
                    {
                        // sb.Append("#".PadLeft(padding));
                        sb.Append(value.PadLeft(padding));

                        if (grid[x, y].IsInsidePipe)
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            countFill++;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.White;
                        }

                        // Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("#".PadLeft(padding));
                    }
                }
            }


            Console.ForegroundColor = ConsoleColor.White;

            var countFillText = " --- " + countFill + " ---";
            countFill = 0;

            // Console.WriteLine(countFillText);
            // Console.WriteLine();

            // for testing (this shows up when running unit tests)
            _logger.LogInformation(sb + countFillText);

            // for running in console

            sb.Clear();
        }

        // Console.ForegroundColor = ConsoleColor.White;
    }

    public int RunPart1(string[] input)
    {
        _logger.LogInformation("Solving - 2023 - Day 10 - Part 1");
        _logger.LogInformation("Input contains {Input} values", input.Length);

        var grid = CreateGrid(input);

        // for each cell, find distance to S, using manhattan distance
        // the cell that is the furthest away is the answer (that has a distance > 0)

        Cell furthest = new Cell();
        Cell[,] distances = GetAllDistances(grid, true);
        for (var y = 0; y < distances.GetLength(1); y++)
        {
            for (var x = 0; x < distances.GetLength(0); x++)
            {
                if (distances[x, y].Steps > furthest.Steps)
                {
                    furthest = distances[x, y];
                }
            }
        }

        PrintGrid(grid);

        _logger.LogInformation("Furthest is {X}, {Y} with distance {Distance}, steps {Distance}", furthest.X, furthest.Y, furthest.DistanceToS, furthest.Steps);

        return furthest.Steps;
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

        // int step = 0;

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

            var stepsTaken = grid[x, y].Steps + 1;

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
                    grid[x, y - 1].Steps = stepsTaken;
                    grid[x, y - 1].DistanceToS = Math.Abs(start.X - x) + Math.Abs(start.Y - (y - 1));
                    possiblePaths.AddFirst(grid[x, y - 1]);
                }
            }
            if (directions.Contains(Direction.South))
            {
                // south
                if (south != null && !visited.Contains(south) && southConnectors.Contains(south.Value))
                {
                    grid[x, y + 1].Steps = stepsTaken;
                    grid[x, y + 1].DistanceToS = Math.Abs(start.X - x) + Math.Abs(start.Y - (y - 1));
                    possiblePaths.AddFirst(grid[x, y + 1]);
                }
            }
            if (directions.Contains(Direction.West))
            {
                // west
                if (west != null && !visited.Contains(west) && westConnectors.Contains(west.Value))
                {
                    grid[x - 1, y].Steps = stepsTaken;
                    grid[x - 1, y].DistanceToS = Math.Abs(start.X - x) + Math.Abs(start.Y - (y - 1));
                    possiblePaths.AddFirst(grid[x - 1, y]);
                }
            }
            if (directions.Contains(Direction.East))
            {
                // east
                if (east != null && !visited.Contains(east) && eastConnectors.Contains(east.Value))
                {
                    grid[x + 1, y].Steps = stepsTaken;
                    grid[x + 1, y].DistanceToS = Math.Abs(start.X - x) + Math.Abs(start.Y - (y - 1));
                    possiblePaths.AddFirst(grid[x + 1, y]);
                }
            }

            // _logger.LogInformation("Step {Step}", step);
            // _logger.LogInformation("--------------------");
            // PrintGrid(grid);
            // _logger.LogInformation("--------------------");
            // step++;

        }

        // PrintGrid(grid);

        return grid;
    }

    public bool IsInside(Cell cell, Cell[,] grid)
    {
        PrintGrid(grid);

        var possiblePaths = new LinkedList<Cell>();
        possiblePaths.AddFirst(cell);

        var visited = new HashSet<Cell>();

        int[] dy = { 0, 0, -1, 1, };
        int[] dx = { -1, 1, 0, 0, };
        Direction[] directions = { Direction.West, Direction.East, Direction.North, Direction.South, };

        while (possiblePaths.Count > 0)
        {
            // move in all directions and look for .
            var current = possiblePaths.Last.Value;
            for (var i = 0; i < dx.Length; i++)
            {
                var x = current.X + dx[i];
                var y = current.Y + dy[i];
                var direction = directions[i];
                var step = current.Steps + 1;

                // check if position is valid
                if (x < 0 || x >= grid.GetLength(0) || y < 0 || y >= grid.GetLength(1))
                {
                    // found edge of grid
                    return false;
                }

                visited.Add(current);
                possiblePaths.Remove(current);

                // if (current.IsPipe())
                // {

                switch (current.Value)
                {
                    case '.': // can move in all directions
                        break;
                    case '|': // can move north and south
                        if (direction != Direction.North && direction != Direction.South)
                            continue;
                        break;
                    case '-': // can move east and west
                        if (direction != Direction.East && direction != Direction.West)
                            continue;
                        break;
                    case 'L': // can move north and east
                        if (direction != Direction.North && direction != Direction.East)
                            continue;
                        break;
                    case 'J': // can move north and west
                        if (direction != Direction.North && direction != Direction.West)
                            continue;
                        break;
                    case 'F': // can move south and east
                        if (direction != Direction.South && direction != Direction.East)
                            continue;
                        break;
                    case '7': // can move south and west
                        if (direction != Direction.South && direction != Direction.West)
                            continue;
                        break;
                }
                // }

                // does not work correctly, if you go along a pipe upwards, you can not go left or right
                if (grid[x, y].Value.Equals('.') && !visited.Contains(grid[x, y]))
                {
                    grid[x, y].Steps = step;
                    possiblePaths.AddFirst(grid[x, y]);
                }

                // TODO:
                // In fact, there doesn't even need to be a full tile path to the outside for tiles to count as outside the loop -
                // squeezing between pipes is also allowed! Here, I is still within the loop and O is still outside the loop:

                // TODO: if we are going up or down, we can go along pipe 'F' and '7' and 'L' and 'J', '|'
                var upDownPipeValues = new char[] { '|', 'F', '7', 'L', 'J', };
                if ((direction == Direction.North || direction == Direction.South) && upDownPipeValues.Contains(grid[x, y].Value) && !visited.Contains(grid[x, y]))
                {
                    grid[x, y].Steps = step;
                    possiblePaths.AddFirst(grid[x, y]);
                }

                var leftRightPipeValues = new char[] { '-', 'F', '7', 'L', 'J', };
                if ((direction == Direction.East || direction == Direction.West) && leftRightPipeValues.Contains(grid[x, y].Value) && !visited.Contains(grid[x, y]))
                {
                    grid[x, y].Steps = step;
                    possiblePaths.AddFirst(grid[x, y]);
                }
            }

            _logger.LogInformation("--------------------");
            PrintGrid(grid);
        }

        return true;
    }

    public int RunPart2(string[] input)
    {
        _logger.LogInformation("Solving - 2023 - Day 10 - Part 2");
        _logger.LogInformation("Input contains {Input} values", input.Length);

        var grid = CreateGrid(input);

        // TODO: find another way to do this, instead of checking if one value can find a path out of the grid
        // instead, "fill" the grid and count the number of cells that are not filled.

        // Cell[,] distances = GetAllDistances(grid, true);

        // PrintGrid(grid, 2);

        // flood fill algorithm?
        // for each . element, check if it's "inside" a pipe, meaning it has a number to the left and right, and above and below
        // continue to move left, right, top, bottom, until you hit a pipe. If you hit a pipe in all four directions, it's inside a pipe
        var count = 0;
        for (var y = 0; y < grid.GetLength(1); y++)
        {
            for (var x = 0; x < grid.GetLength(0); x++)
            {
                var cell = grid[x, y];
                if (cell.Value.Equals('.'))
                {
                    if (IsInside(cell, grid))
                    {
                        count++;
                        cell.IsInsidePipe = true;
                    }
                }
            }
        }

        PrintGrid(grid);

        return count;
    }
}