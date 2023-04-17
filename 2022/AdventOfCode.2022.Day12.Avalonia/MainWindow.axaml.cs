using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AdventOfCode._2022.Day12.Common.Models;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Input;
using Avalonia.Layout;
using Avalonia.Media;

namespace AdventOfCode._2022.Day12.Avalonia;

public partial class MainWindow : Window
{
    private readonly ISolutionService _solutionService;

    // use Dictionary to avoid having to create a new SolidColorBrush, ImageSource, etc. every time, which is expensive

    // pre-calculate the colors for the cells based on the value
    private readonly Dictionary<int, SolidColorBrush> _valueToColor = new();

    // private readonly Dictionary<GridElementType, ImageSource> _gridValueToImage = new()
    // {
    //     { GridElementType.Empty, Images.Empty },
    //     { GridElementType.Snake, Images.Body },
    //     { GridElementType.Food, Images.Food },
    // };
    //
    // private readonly Dictionary<Direction, int> _directionToRotation = new()
    // {
    //     { Direction.Up, 0 },
    //     { Direction.Down, 180 },
    //     { Direction.Right, 90 },
    //     { Direction.Left, 270 },
    // };

    private bool _isSnakeGameLoopRunning;

    private int _rows = 20;
    private int _columns = 20;

    private Grid[,] _grid;
    private State _state;

    public MainWindow()
    {
        InitializeComponent();
    }

    public MainWindow(ISolutionService solutionService) : this()
    {
        _solutionService = solutionService;

        // InitializeComponent();

#if DEBUG
        this.AttachDevTools();
#endif

        _grid = CreateGrid();
        _state = new State(_rows, _columns);

        PopulateValueToColorDictionary(0, 500);

        DrawGrid();

        // ClientSizeProperty.Changed.Subscribe(size =>
        // {
        //     var x = (size.OldValue.Value.Width - size.NewValue.Value.Width) / 2;
        //     var y = (size.OldValue.Value.Height - size.NewValue.Value.Height) / 2;
        //
        //     Console.WriteLine($"width: {size.NewValue.Value.Width}");
        //     Console.WriteLine($"height: {size.NewValue.Value.Height}");
        //
        //     // TODO: set the height and width so the elements don't get uneven sizes or find the perfect width and height so it looks good
        //     
        //     Position = new PixelPoint(Position.X + (int)x, Position.Y + (int)y);
        // });
    }

    private async void StartAdventOfCode()
    {
        // var input = await File.ReadAllLinesAsync("Assets/sample-input.txt");
        var input = await File.ReadAllLinesAsync("Assets/input.txt");

        // parse input
        var grid = _solutionService.ParseInput(input);

        // set rows and columns
        _rows = grid.GetLength(0);
        _columns = grid.GetLength(1);

        // create UI elements
        _grid = CreateGrid();

        // update game state
        _state = new State(grid);

        // update UI elements with correct images
        DrawGrid();

        Overlay.IsVisible = false;
        ScoreText.Text = "STEP: 0";

        // update grid and calculate the step to each position
        // _solutionService.GetNumberOfStepsToEachLocation(grid);
        // await FindShortestPath(grid);
    }

    // DUPLICATE CODE, also in solution service, but here we update the UI and animate
    public async Task<GridElement?> FindShortestPath(GridElement[,] grid, GridElement start)
    {
        var count = 0;

        // find start position
        // var start = _solutionService.FindGridElement(grid, "S");
        // start.Step = 0;

        // find end position
        var end = _solutionService.FindGridElement(grid, "E");

        var queue = new Queue<GridElement>();
        queue.Enqueue(start);

        while (queue.Count > 0)
        {
            // TODO: this should not be needed, should be able to use SortedList that is kept sorted on insert
            // re-order by total cost
            queue = new Queue<GridElement>(queue.OrderBy(x => x.TotalCost));

            var current = queue.Dequeue();
            foreach (var neighbour in _solutionService.GetNeighbors(grid, current))
            {
                if (neighbour is { Type: GridElementType.Empty or GridElementType.Food, Step: -1 })
                {
                    var currentValue = (int)current.Value.First();
                    if (current.Value == "S")
                    {
                        currentValue = 97; // a
                    }

                    var neighbourValue = (int)neighbour.Value.First();
                    if (neighbour.Value == "E")
                    {
                        neighbourValue = 122; // z
                    }

                    // if the neighbour is more than 1 step away, it is not allowed to go "up"
                    // still allowed to go "down", so we can go from "z" to "a" for example
                    if (neighbourValue - currentValue > 1)
                    {
                        continue;
                    }

                    neighbour.Previous = current;
                    neighbour.Step = current.Step + 1;
                    neighbour.Distance = _solutionService.GetManhattanDistance(grid, neighbour, end);
                    neighbour.TotalCost = neighbour.Step + neighbour.Distance;

                    queue.Enqueue(neighbour); // add to queue to visit later
                    // queue.Add(neighbour.TotalCost, neighbour); // add to queue to visit later

                    // only animate every 5th step
                    count++;
                    if (count % 5 == 0)
                    {
                        Console.WriteLine($"count: {count}");
                        // animate
                        ScoreText.Text = $"STEP: {neighbour.Step}";

                        DrawGrid();
                        await Task.Delay(2);
                    }

                    // if element is the finish line, stop the loop and animate the path
                    if (neighbour.Value == "E")
                    {
                        ShowFinalPath(neighbour);
                        return neighbour;
                    }
                }
            }
        }

        return null;
    }

    private async void ShowFinalPath(GridElement element)
    {
        var current = element;
        while (current.Previous != null)
        {
            current.Type = GridElementType.Snake;
            DrawGrid();

            await Task.Delay(5);

            current = current.Previous;
        }

        await Task.Delay(1000);

        Overlay.IsVisible = true;
        OverlayText.Text = "Path Found!";
    }

    private Grid[,] CreateGrid()
    {
        GameGrid.Children.Clear();

        var gridElements = new Grid[_rows, _columns];

        GameGrid.Rows = _rows;
        GameGrid.Columns = _columns;
        GameGrid.Width = GameGrid.Height * (_columns / (double)_rows);

        for (var row = 0; row < _rows; row++)
        {
            for (var column = 0; column < _columns; column++)
            {
                // set all grid positions to empty with the image for empty to begin with
                // var image = new Image
                // {
                //     Source = Images.Empty,
                //     RenderTransformOrigin = new Point(0.5, 0.5) // center the rotation, otherwise it rotates around the top left corner
                //     RenderTransformOrigin = new RelativePoint(0.5, 0.5, RelativeUnit.Relative), // center the rotation, otherwise it rotates around the top left corner
                // };

                var image = new Rectangle() { };
                var textBlock = new TextBlock
                {
                    Text = "", // set the value from the input.txt file
                    // Foreground = Brushes.White,
                    FontSize = 5,
                    Margin = new Thickness(2),
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center
                };

                var grid = new Grid();
                grid.UseLayoutRounding = true;
                grid.Margin = new Thickness(0);

                // grid.Margin = new Thickness(0.5);
                // grid.Background = Brushes.Black;

                grid.Children.Add(image);
                grid.Children.Add(textBlock);

                grid.Tag = new Tuple<int, int>(row, column); // Store row and column information on the Grid element

                // add click event to each grid element with the row and column as parameters

                // add to UniformGrid in the MainWindow
                GameGrid.Children.Add(grid);
                gridElements[row, column] = grid;
            }
        }

        return gridElements;
    }

    private void DrawGrid()
    {
        for (var r = 0; r < _rows; r++)
        {
            for (var c = 0; c < _columns; c++)
            {
                var gridElement = _state.Grid[r, c];

                // var image = _grid[r, c].Children[0] as Image;
                var image = _grid[r, c].Children[0] as Rectangle;
                image.Fill = gridElement.Type switch
                {
                    // define color based on value
                    GridElementType.Empty => gridElement.Step != -1 ? _valueToColor[gridElement.Step] : Brushes.White,
                    GridElementType.Snake => Brushes.Black,
                    GridElementType.Food => Brushes.Red,
                    _ => throw new ArgumentOutOfRangeException()
                };
                image.ClipToBounds = true;

                // image!.Source = _gridValueToImage[gridElement.Type];
                // image.RenderTransform = Transform.Identity; // reset rotation

                if (_isSnakeGameLoopRunning)
                {
                    continue;
                }

                image.PointerPressed += (sender, args) =>
                {
                    // stop FindShortestPath if it is running and start a new one

                    // find path from clicked element to end
                    FindShortestPath(_state.Grid, gridElement);
                };

                var textBlock = _grid[r, c].Children[1] as TextBlock;

                if (_rows > 40)
                {
                    textBlock!.FontSize = 6;
                    textBlock.Margin = new Thickness(0);
                }

                textBlock!.Text = gridElement.Step != -1 ? gridElement.Step.ToString() : gridElement.Value;
            }
        }
    }

    private void PopulateValueToColorDictionary(int min, int max)
    {
        for (var i = min; i < max; i++)
        {
            _valueToColor.Add(i, new SolidColorBrush(GetColorFromValue(i, min, max)));
        }
    }

    private static Color GetColorFromValue(int value, int min, int max)
    {
        var t = (float)(value - min) / (max - min);

        var redValue = (byte)(255 * (1 - t));
        const byte greenValue = 255;

        return Color.FromArgb(255, redValue, greenValue, redValue);
    }

    private void InputElement_OnKeyDown(object? sender, KeyEventArgs e)
    {
        // if overlay is visible, don't allow any key presses to go through
        if (Overlay.IsVisible)
        {
            e.Handled = true;
        }

        // get key pressed
        var key = e.Key;
        switch (key)
        {
            case Key.S:
                // StartSnakeGame();
                break;
            case Key.A:

                // TODO: do not start game, instead you can click on the grid element to set the starting point
                StartAdventOfCode();

                // TODO: mark all possible starting points

                break;
            default:
                return;
        }
    }
}