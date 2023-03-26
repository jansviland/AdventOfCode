using AdventOfCode._2022.Day12.Common.Models;

namespace AdventOfCode._2022.Day12;

public interface ISolutionService
{
    public int RunPart1(string[] input);
    public GridElement[,] ParseInput(string[] input);
    public int RunPart2(string[] input);

    // move all grid and path related methods to a common library
    // https://en.wikipedia.org/wiki/Pathfinding

    /// <summary>
    /// Updates the grid with the number of steps to each location.
    /// </summary>
    /// <returns>Returns the final GridElement that contains the number of steps to each location and the final path</returns>
    public GridElement? FindShortestPath(GridElement[,] grid);

    public GridElement? FindShortestPath(GridElement[,] grid, GridElement start, GridElement end);
    public GridElement FindGridElement(GridElement[,] grid, string value);
    public List<GridElement> GetNeighbors(GridElement[,] grid, GridElement element);

    /// <summary>
    /// https://www.omnicalculator.com/math/manhattan-distance#what-is-the-manhattan-distance
    ///
    /// Get the Manhattan distance between two points. This means that you can not move diagonally. Only up, down, left and right.
    /// Similar to if you were to walk through a city and had to walk around any buildings in your way (like in New York City).
    /// </summary>
    public int GetManhattanDistance(GridElement[,] grid, GridElement start, GridElement end);

    public LinkedList<Position> FindPath(string[] input); // each position from the start to the end
}

public class SolutionService : ISolutionService
{
    private readonly ILogger<SolutionService> _logger;

    public SolutionService(ILogger<SolutionService> logger)
    {
        _logger = logger;
    }

    public int RunPart1(string[] input)
    {
        _logger.LogInformation("Solving day 12");
        _logger.LogInformation("Input contains {Input} values", input.Length);

        var grid = ParseInput(input);

        var finalElement = FindShortestPath(grid);
        return finalElement!.Step;
    }

    public GridElement[,] ParseInput(string[] input)
    {
        var rowCount = input.Length;
        var columnCount = input[0].Length;
        var grid = new GridElement[rowCount, columnCount];

        for (var r = 0; r < rowCount; r++)
        {
            var column = input[r];

            for (var c = 0; c < columnCount; c++)
            {
                var character = column[c];
                var type = character switch
                {
                    'S' => GridElementType.Snake,
                    'E' => GridElementType.Food,
                    _ => GridElementType.Empty
                };
                var element = new GridElement(type, character.ToString(), r, c);

                grid[r, c] = element;
            }
        }

        return grid;
    }

    // public GridElement? FindShortestPath(GridElement[,] grid)
    // {
    //     // find start position
    //     var start = FindGridElement(grid, "S");
    //     start.Step = 0;
    //
    //     // find end position
    //     var end = FindGridElement(grid, "E");
    //
    //     // Automatically keep the list sorted by TotalCost
    //     // var openList = new SortedSet<GridElement>(Comparer<GridElement>.Create((a, b) => a.Distance.CompareTo(b.Distance)));
    //     // var openList = new SortedSet<GridElement>(Comparer<GridElement>.Create((a, b) => a.TotalCost.CompareTo(b.TotalCost)));
    //     // openList.Add(start);
    //
    //     var queue = new Queue<GridElement>();
    //     queue.Enqueue(start);
    //
    //     while (queue.Count > 0)
    //     {
    //         // BUG: this is not working correctly, when using orderBy, it is not finding the shortest path, should be 490, result is 514...
    //
    //         // order by distance to end goal (remove this to find every possible path)
    //         // queue = new Queue<GridElement>(queue.OrderBy(x => x.Distance));
    //
    //         var current = queue.Dequeue();
    //         // var current = openList.First();
    //         // openList.Remove(current);
    //
    //         var currentStep = current.Step;
    //
    //         foreach (var neighbor in GetNeighbors(grid, current))
    //         {
    //             // check to see if we have visited this position before, if we have (step is != -1), skip it
    //             // var element = grid[neighbor.Row, neighbor.Column];
    //
    //             var tentativeCost = current.Step + 1;
    //
    //             if (neighbor is { Type: GridElementType.Empty or GridElementType.Food, Step: -1}) // && !openList.Contains(neighbor) || tentativeCost < neighbor.Step
    //             {
    //                 var currentValue = (int)current.Value.First();
    //                 if (current.Value == "S")
    //                 {
    //                     currentValue = 97; // a
    //                 }
    //
    //                 var elementValue = (int)neighbor.Value.First();
    //                 if (neighbor.Value == "E")
    //                 {
    //                     elementValue = 122; // z
    //                 }
    //
    //                 // var diff = Math.Abs(currentValue - elementValue);
    //                 if (elementValue - currentValue > 1)
    //                 {
    //                     continue;
    //                 }
    //
    //                 // var cost = current.Cost + 1;
    //
    //                 neighbor.Previous = current;
    //                 neighbor.Step = currentStep + 1;
    //                 neighbor.Distance = GetManhattanDistance(grid, neighbor, end);
    //                 neighbor.TotalCost = neighbor.Step + neighbor.Distance;
    //
    //                 queue.Enqueue(neighbor); // add to queue to visit later
    //                 // openList.Add(neighbor); // add to queue to visit later
    //
    //                 // animate
    //                 // ScoreText.Text = $"STEP: {element.Step}";
    //                 //
    //                 // DrawGrid();
    //                 // await Task.Delay(10);
    //                 //
    //                 // // if element is the finish line, stop the loop and animate the path
    //                 if (neighbor.Value == "E")
    //                 {
    //                     // ShowFinalPath(element);
    //                     return neighbor;
    //                 }
    //             }
    //         }
    //     }
    //
    //     return null;
    // }

    public GridElement? FindShortestPath(GridElement[,] grid)
    {
        // find start position
        var start = FindGridElement(grid, "S");
        start.Step = 0;

        // find end position
        var end = FindGridElement(grid, "E");

        // adjecent positions
        var adjecentPositions = new List<Position>
        {
            new Position(0, 1),
            new Position(0, -1),
            new Position(1, 0),
            new Position(-1, 0),
        };

        var queue = new Queue<GridElement>();
        queue.Enqueue(start);

        while (queue.Count > 0)
        {
            // order by distance to end goal (remove this to find every possible path)
            // queue = new Queue<GridElement>(queue.OrderBy(x => x.Distance));
            queue = new Queue<GridElement>(queue.OrderBy(x => x.TotalCost));

            var current = queue.Dequeue();
            var currentStep = current.Step;

            foreach (var adjecentPosition in adjecentPositions)
            {
                var row = current.Row + adjecentPosition.Row; // move along x axis
                var column = current.Column + adjecentPosition.Column; // move along y axis

                // check if we are out of bounds
                if (row < 0 || row >= grid.GetLength(0) || column < 0 || column >= grid.GetLength(1))
                {
                    continue; // skip this position since it is out of bounds
                }

                // check to see if we have visited this position before, if we have (step is != -1), skip it
                var element = grid[row, column];
                if (element is { Type: GridElementType.Empty or GridElementType.Food, Step: -1 })
                {
                    var currentValue = (int)current.Value.First();
                    if (current.Value == "S")
                    {
                        currentValue = 97; // a
                    }

                    var elementValue = (int)element.Value.First();
                    if (element.Value == "E")
                    {
                        elementValue = 122; // z
                    }

                    // var diff = Math.Abs(currentValue - elementValue);
                    if (elementValue - currentValue > 1)
                    {
                        continue;
                    }

                    element.Previous = current;
                    element.Step = currentStep + 1;
                    element.Distance = GetManhattanDistance(grid, element, end);
                    element.TotalCost = element.Step + element.Distance;

                    queue.Enqueue(element); // add to queue to visit later

                    // animate
                    // ScoreText.Text = $"STEP: {element.Step}";
                    //
                    // DrawGrid();
                    // await Task.Delay(10);

                    // if element is the finish line, stop the loop and animate the path
                    if (element.Value == "E")
                    {
                        // ShowFinalPath(element);
                        return element;
                    }
                }
            }
        }

        return null;
    }

    public GridElement? FindShortestPath(GridElement[,] grid, GridElement start, GridElement end)
    {
        throw new NotImplementedException();
    }

    public GridElement FindGridElement(GridElement[,] grid, string value)
    {
        for (var r = 0; r < grid.GetLength(0); r++)
        {
            for (var c = 0; c < grid.GetLength(1); c++)
            {
                if (grid[r, c].Value == value)
                {
                    return grid[r, c];
                }
            }
        }

        throw new Exception("No start position found");
    }

    public List<GridElement> GetNeighbors(GridElement[,] grid, GridElement element)
    {
        var neighbors = new List<GridElement>();

        // adjecent positions
        var adjecentPositions = new List<Position>
        {
            new Position(0, 1),
            new Position(0, -1),
            new Position(1, 0),
            new Position(-1, 0),
        };

        foreach (var adjecentPosition in adjecentPositions)
        {
            var row = element.Row + adjecentPosition.Row; // move along x axis
            var column = element.Column + adjecentPosition.Column; // move along y axis

            // check if we are out of bounds
            if (row < 0 || row >= grid.GetLength(0) || column < 0 || column >= grid.GetLength(1))
            {
                continue; // skip this position since it is out of bounds
            }

            neighbors.Add(grid[row, column]);
        }

        return neighbors;
    }

    public int GetManhattanDistance(GridElement[,] grid, GridElement start, GridElement end)
    {
        return Math.Abs(start.Row - end.Row) + Math.Abs(start.Column - end.Column);
    }

    public LinkedList<Position> FindPath(string[] input)
    {
        throw new NotImplementedException();
    }

    public int RunPart2(string[] input)
    {
        _logger.LogInformation("Solving day 12 - Part 2");
        _logger.LogInformation("Input contains {Input} values", input.Length);

        throw new NotImplementedException();
    }
}