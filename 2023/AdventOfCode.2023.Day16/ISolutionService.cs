using Common;

namespace AdventOfCode._2023.Day16;

public interface ISolutionService
{
    public long RunPart1(string[] input, bool animated = false);
    public long RunPart2(string[] input);
}

public class SolutionService : ISolutionService
{
    private readonly ILogger<ISolutionService> _logger;

    public SolutionService(ILogger<SolutionService> logger)
    {
        _logger = logger;
    }

    public void PrintGrid(Cell[,] grid, bool animated = false)
    {
        var numRows = grid.GetLength(0);
        var numCols = grid.GetLength(1);

        var sb = new StringBuilder();

        if (animated)
        {
            Console.Clear();
            Console.SetCursorPosition(0, 0);
        }

        for (var y = 0; y < numCols; y++)
        {
            sb.Append(y.ToString().PadLeft(3, ' '));

            for (var x = 0; x < numRows; x++)
            {
                var value = grid[x, y].Value.ToString();
                if (grid[x, y].Value == 'X')
                {
                    value = "X";
                }
                else if (grid[x, y].Steps != -1)
                {
                    value = (grid[x, y].Steps % 100).ToString();
                }

                sb.Append(value.PadLeft(3, ' '));
            }

            sb.AppendLine();
        }

        if (animated)
        {
            Console.WriteLine(sb.ToString());
            Thread.Sleep(10);
            sb.Clear();
        }
        else
        {
            _logger.LogInformation(sb.ToString());
            sb.Clear();
        }
    }

    public long RunPart1(string[] input, bool animated = false)
    {
        _logger.LogInformation("Solving - 2023 - Day 16 - Part 1");
        _logger.LogInformation("Input contains {Input} values", input.Length);

        var grid = Grid.CreateGrid(input);
        // PrintGrid(grid);

        var paddedGrid = grid.ApplyPadding();

        // top left corner
        var start = paddedGrid[1, 1];
        // start.Value = '-';
        start.Steps = 0;
        start.Direction = Direction.Right;

        var visited = new HashSet<Cell>();
        var possibleValues = new Queue<Cell>();

        possibleValues.Enqueue(start);

        var totalSteps = 0;

        while (possibleValues.Any())
        {
            // Not a good fix, but it works, we can get stuck in a loop.
            // and when we always process the cells in the same order, we never get out of the loop.
            // take a random cell from the stack
            // var random = new Random();
            // var index = random.Next(possibleValues.Count);
            // var current = possibleValues.ElementAt(index);
            // possibleValues.Remove(current);

            var current = possibleValues.Dequeue();
            visited.Add(current);

            var newPaths = new List<Cell>();

            if (current.Value == 'X')
            {
                continue;
            }

            var up = paddedGrid[current.X, current.Y - 1];
            up.Direction = Direction.Up;
            var down = paddedGrid[current.X, current.Y + 1];
            down.Direction = Direction.Down;
            var right = paddedGrid[current.X + 1, current.Y];
            right.Direction = Direction.Right;
            var left = paddedGrid[current.X - 1, current.Y];
            left.Direction = Direction.Left;

            switch (current.Value)
            {
                case '.':
                    // If the beam encounters empty space (.), it continues in the same direction.
                    switch (current.Direction)
                    {
                        case Direction.Up:
                            newPaths.Add(up);
                            break;
                        case Direction.Down:
                            newPaths.Add(down);
                            break;
                        case Direction.Left:
                            newPaths.Add(left);
                            break;
                        case Direction.Right:
                            newPaths.Add(right);
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                    break;

                // If the beam encounters a mirror (/ or \), the beam is reflected 90 degrees depending on the angle of the mirror.
                // For instance, a rightward-moving beam that encounters a / mirror would continue upward in the mirror's column, while a rightward-moving beam that encounters a \ mirror
                // would continue downward from the mirror's column.
                case '/':
                    switch (current.Direction)
                    {
                        case Direction.Up:
                            newPaths.Add(right);
                            break;
                        case Direction.Down:
                            newPaths.Add(left);
                            break;
                        case Direction.Left:
                            newPaths.Add(down);
                            break;
                        case Direction.Right:
                            newPaths.Add(up);
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                    break;

                case '\\':
                    switch (current.Direction)
                    {
                        case Direction.Up:
                            newPaths.Add(left);
                            break;
                        case Direction.Down:
                            newPaths.Add(right);
                            break;
                        case Direction.Left:
                            newPaths.Add(up);
                            break;
                        case Direction.Right:
                            newPaths.Add(down);
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();

                    }
                    break;

                // If the beam encounters the pointy end of a splitter (| or -), the beam passes through the splitter as if the splitter were empty space.
                // For instance, a rightward-moving beam that encounters a - splitter would continue in the same direction.

                case '|':
                    switch (current.Direction)
                    {
                        case Direction.Up:
                            newPaths.Add(up);
                            break;
                        case Direction.Down:
                            newPaths.Add(down);
                            break;
                        case Direction.Left:

                            if (visited.Contains(up) && visited.Contains(down))
                            {
                                continue;
                            }

                            visited.Add(up);
                            visited.Add(down);

                            newPaths.Add(up);
                            newPaths.Add(down);
                            break;
                        case Direction.Right:


                            if (visited.Contains(up) && visited.Contains(down))
                            {
                                continue;
                            }
                            visited.Add(up);
                            visited.Add(down);

                            newPaths.Add(up);
                            newPaths.Add(down);
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                    break;

                case '-':
                    switch (current.Direction)
                    {
                        case Direction.Up:

                            if (visited.Contains(right) && visited.Contains(left))
                            {
                                continue;
                            }

                            visited.Add(right);
                            visited.Add(left);

                            newPaths.Add(right);
                            newPaths.Add(left);
                            break;
                        case Direction.Down:

                            if (visited.Contains(right) && visited.Contains(left))
                            {
                                continue;
                            }

                            visited.Add(right);
                            visited.Add(left);

                            newPaths.Add(right);
                            newPaths.Add(left);
                            break;
                        case Direction.Left:
                            newPaths.Add(left);
                            break;
                        case Direction.Right:
                            newPaths.Add(right);
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                    break;

            }

            foreach (var cell in newPaths)
            {
                cell.Previous = current;
                cell.Steps = current.Steps + 1;

                possibleValues.Enqueue(cell);
            }

            if (animated)
            {
                if (totalSteps % 10 == 0)
                {
                    PrintGrid(paddedGrid, true);
                }
            }

            // PrintGrid(paddedGrid, true);
            totalSteps += 1;
        }

        // PrintGrid(paddedGrid);

        var count = 0;
        for (var col = 1; col < paddedGrid.GetLength(1) - 1; col++)
        {
            for (var row = 1; row < paddedGrid.GetLength(0) - 1; row++)
            {
                // if (paddedGrid[row, col].Value == 'X')
                // {
                //     break;
                // }
                //
                // if (paddedGrid[row, col].Steps > -1)
                // {
                //     count += 1;
                // }
                if (paddedGrid[row, col].Steps > -1)
                {
                    count += 1;
                }
            }
        }

        return count;
    }

    public long RunPart2(string[] input)
    {
        _logger.LogInformation("Solving - 2023 - Day 16 - Part 2");
        _logger.LogInformation("Input contains {Input} values", input.Length);


        throw new NotImplementedException();
    }
}