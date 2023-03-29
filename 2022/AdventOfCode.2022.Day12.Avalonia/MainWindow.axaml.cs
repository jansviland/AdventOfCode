using System;
using AdventOfCode._2022.Day12.Common.Models;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Layout;
using Avalonia.Media;

namespace AdventOfCode._2022.Day12.Avalonia;

public partial class MainWindow : Window
{
    private bool _isSnakeGameLoopRunning;

    private int _rows = 20;
    private int _columns = 20;

    private Grid[,] _grid;
    private State _state;

    public MainWindow()
    {
        InitializeComponent();

#if DEBUG
        this.AttachDevTools();
#endif

        _grid = CreateGrid();
        _state = new State(_rows, _columns);

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
                // grid.MouseLeftButtonDown += GridElement_MouseLeftButtonDown;

                // add to UniformGrid in the MainWindow
                GameGrid.Children.Add(grid);
                gridElements[row, column] = grid;
            }
        }

        return gridElements;
    }

    private void DrawGrid()
    {
        // test
        var counter = 0;

        for (var r = 0; r < _rows; r++)
        {
            for (var c = 0; c < _columns; c++)
            {
                var gridElement = _state.Grid[r, c];

                // var image = _grid[r, c].Children[0] as Image;
                var image = _grid[r, c].Children[0] as Rectangle;
                image.Fill = gridElement.Type switch
                {
                    GridElementType.Empty => Brushes.White,
                    GridElementType.Snake => Brushes.Green,
                    GridElementType.Food => Brushes.Red,
                    _ => throw new ArgumentOutOfRangeException()
                };
                image.ClipToBounds = true;

                // TODO: instead of using type, use value, then create a rectangle instead of image
                // the color can be based on the value, the higher the value, the darker the color for example

                // image!.Source = _gridValueToImage[gridElement.Type];
                // image.RenderTransform = Transform.Identity; // reset rotation

                if (_isSnakeGameLoopRunning)
                {
                    continue;
                }

                var textBlock = _grid[r, c].Children[1] as TextBlock;

                if (_rows > 40)
                {
                    textBlock!.FontSize = 6;
                    textBlock.Margin = new Thickness(0);
                }

                // textBlock!.Text = gridElement.Step != -1 ? gridElement.Step.ToString() : gridElement.Value;

                // test
                textBlock!.Text = counter++.ToString();
            }
        }
    }
}