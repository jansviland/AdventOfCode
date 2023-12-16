using Common;

namespace AdventOfCode._2023.Day16;

public interface ISolutionService
{
    public long RunPart1(string[] input);
    public long RunPart2(string[] input);
}

public class SolutionService : ISolutionService
{
    private readonly ILogger<ISolutionService> _logger;

    public SolutionService(ILogger<SolutionService> logger)
    {
        _logger = logger;
    }

    public void PrintGrid(Cell[,] grid)
    {
        var numRows = grid.GetLength(0);
        var numCols = grid.GetLength(1);

        var sb = new StringBuilder();
        sb.AppendLine();

        for (var row = 0; row < numRows; row++)
        {
            for (var col = 0; col < numCols; col++)
            {
                var value = "";

                if (grid[row, col].Steps != -1)
                {
                    value = grid[row, col].Steps.ToString();
                    // sb.Append(grid[row, col].Steps);
                }
                else
                {
                    value = grid[row, col].Value.ToString();
                    // sb.Append(grid[row, col].Value);
                }

                sb.Append(value.PadLeft(3, ' '));
            }

            sb.AppendLine();
        }

        _logger.LogInformation(sb.ToString());
    }

    public long RunPart1(string[] input)
    {
        _logger.LogInformation("Solving - 2023 - Day 16 - Part 1");
        _logger.LogInformation("Input contains {Input} values", input.Length);

        var grid = Grid.CreateGrid(input);
        PrintGrid(grid);

        var paddedGrid = grid.ApplyPadding();

        // top left corner
        var start = paddedGrid[1, 1];
        // start.Value = '-';
        start.Steps = 0;
        start.Direction = Direction.Right;

        PrintGrid(paddedGrid);

        var possibleValues = new Queue<Cell>();
        possibleValues.Enqueue(start);

        var visited = new HashSet<Cell>();

        while (possibleValues.Any())
        {
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

                case '\'':
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
                            newPaths.Add(up);
                            newPaths.Add(down);
                            break;
                        case Direction.Right:
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
                            newPaths.Add(up);
                            newPaths.Add(down);
                            break;
                        case Direction.Down:
                            newPaths.Add(down);
                            newPaths.Add(up);
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
                if (visited.Contains(cell))
                {
                    continue;
                }

                cell.Previous = current;
                cell.Steps = current.Steps + 1;
                // cell.Direction = current.Direction;

                possibleValues.Enqueue(cell);
            }

            PrintGrid(paddedGrid);
        }


        throw new NotImplementedException();
    }

    public long RunPart2(string[] input)
    {
        _logger.LogInformation("Solving - 2023 - Day 16 - Part 2");
        _logger.LogInformation("Input contains {Input} values", input.Length);

        throw new NotImplementedException();
    }
}