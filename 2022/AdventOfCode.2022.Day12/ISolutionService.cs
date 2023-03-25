using System.Net.WebSockets;
using AdventOfCode._2022.Day12.Common.Models;

namespace AdventOfCode._2022.Day12;

public interface ISolutionService
{
    public int RunPart1(string[] input);
    public GridElement[,] ParseInput(string[] input);

    // https://en.wikipedia.org/wiki/Pathfinding
    public void GetNumberOfStepsToEachLocation(GridElement[,] grid);
    public LinkedList<Position> FindPath(string[] input); // each position from the start to the end
    public int RunPart2(string[] input);
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

        throw new NotImplementedException();
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

    public void GetNumberOfStepsToEachLocation(GridElement[,] grid)
    {
        // find start position
        var start = FindStart(grid);
        start.Step = 0;

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
                if (element is { Type: GridElementType.Empty, Step: -1 })
                {
                    var currentValue = (int)current.Value.First();
                    var elementValue = (int)element.Value.First();

                    var diff = Math.Abs(currentValue - elementValue);
                    if (diff > 1)
                    {
                        continue;
                    }

                    element.Step = currentStep + 1;
                    queue.Enqueue(element); // add to queue to visit later
                }
            }
        }
    }

    private GridElement FindStart(GridElement[,] grid)
    {
        for (var r = 0; r < grid.GetLength(0); r++)
        {
            for (var c = 0; c < grid.GetLength(1); c++)
            {
                if (grid[r, c].Type == GridElementType.Snake || grid[r, c].Value == "S") // or value S
                {
                    return grid[r, c];
                }
            }
        }

        throw new Exception("No start position found");
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