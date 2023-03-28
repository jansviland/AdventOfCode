using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using AdventOfCode._2022.Day12.App.Models;
using AdventOfCode._2022.Day12.Common.Models;

namespace AdventOfCode._2022.Day12.App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private readonly ISolutionService _solutionService;

        private readonly Dictionary<GridElementType, ImageSource> _gridValueToImage = new()
        {
            { GridElementType.Empty, Images.Empty },
            { GridElementType.Snake, Images.Body },
            { GridElementType.Food, Images.Food },
        };

        private readonly Dictionary<Direction, int> _directionToRotation = new()
        {
            { Direction.Up, 0 },
            { Direction.Down, 180 },
            { Direction.Right, 90 },
            { Direction.Left, 270 },
        };

        private int _rows = 20;
        private int _columns = 20;
        private Grid[,] _grid;

        private bool _isSnakeGameLoopRunning;
        private State _state;

        public MainWindow(ISolutionService solutionService)
        {
            _solutionService = solutionService;
            InitializeComponent();

            _grid = CreateGrid();
            _state = new State(_rows, _columns);

            DrawGrid();
        }

        private void StartAdventOfCode()
        {
            // string[] input = File.ReadAllLines("Assets/sample-input.txt");
            string[] input = File.ReadAllLines("Assets/input.txt");

            // parse input
            var grid = _solutionService.ParseInput(input);

            // update grid and calculate the step to each position
            // _solutionService.GetNumberOfStepsToEachLocation(grid);
            FindShortestPath(grid);

            // set rows and columns
            _rows = grid.GetLength(0);
            _columns = grid.GetLength(1);

            // create UI elements
            _grid = CreateGrid();

            // update game state
            _state = new State(grid);

            // update UI elements with correct images
            DrawGrid();

            Overlay.Visibility = Visibility.Hidden;
            ScoreText.Text = "STEP: 0";
        }

        // DUPLICATE CODE, also in solution service, but here we update the UI and animate
        // public async Task<GridElement?> FindShortestPath(GridElement[,] grid)
        // {
        //     // find start position
        //     var start = _solutionService.FindGridElement(grid, "S");
        //     start.Step = 0;
        //
        //     // find end position
        //     var end = _solutionService.FindGridElement(grid, "E");
        //
        //     // Automatically keep the list sorted by TotalCost
        //     var openList = new SortedSet<GridElement>(Comparer<GridElement>.Create((a, b) => a.TotalCost.CompareTo(b.TotalCost)));
        //     openList.Add(start);
        //
        //     // var queue = new Queue<GridElement>();
        //     // queue.Enqueue(start);
        //
        //     while (openList.Count > 0)
        //     {
        //         // BUG: this is not working correctly, when using orderBy, it is not finding the shortest path, should be 490, result is 514...
        //
        //         // order by distance to end goal (remove this to find every possible path)
        //         // queue = new Queue<GridElement>(queue.OrderBy(x => x.Distance));
        //
        //         // var current = queue.Dequeue();
        //         var current = openList.First();
        //         openList.Remove(current);
        //
        //         var currentStep = current.Step;
        //
        //         foreach (var adjecentPosition in _solutionService.GetNeighbors(grid, current))
        //         {
        //             // check to see if we have visited this position before, if we have (step is != -1), skip it
        //             var element = grid[adjecentPosition.Row, adjecentPosition.Column];
        //             if (element is { Type: GridElementType.Empty or GridElementType.Food, Step: -1 })
        //             {
        //                 var currentValue = (int)current.Value.First();
        //                 if (current.Value == "S")
        //                 {
        //                     currentValue = 97; // a
        //                 }
        //
        //                 var elementValue = (int)element.Value.First();
        //                 if (element.Value == "E")
        //                 {
        //                     elementValue = 122; // z
        //                 }
        //
        //                 if (elementValue - currentValue > 1)
        //                 {
        //                     continue;
        //                 }
        //
        //                 element.Previous = current;
        //                 element.Step = currentStep + 1;
        //                 element.Distance = _solutionService.GetManhattanDistance(grid, element, end);
        //                 element.TotalCost = element.Step + element.Distance;
        //
        //                 // queue.Enqueue(element); // add to queue to visit later
        //                 openList.Add(element); // add to queue to visit later
        //
        //                 // animate
        //                 ScoreText.Text = $"STEP: {element.Step}";
        //
        //                 DrawGrid();
        //                 await Task.Delay(10);
        //
        //                 // if element is the finish line, stop the loop and animate the path
        //                 if (element.Value == "E")
        //                 {
        //                     ShowFinalPath(element);
        //                     return element;
        //                 }
        //             }
        //         }
        //     }
        //
        //     return null;
        // }

        public async Task<GridElement?> FindShortestPath(GridElement[,] grid)
        {
            // find start position
            var start = _solutionService.FindGridElement(grid, "S");
            start.Step = 0;

            // find end position
            var end = _solutionService.FindGridElement(grid, "E");

            var queue = new Queue<GridElement>();
            queue.Enqueue(start);

            while (queue.Count > 0)
            {
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

                        // var diff = Math.Abs(currentValue - elementValue);
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

                        // animate
                        ScoreText.Text = $"STEP: {neighbour.Step}";

                        DrawGrid();
                        await Task.Delay(10);

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
                await Task.Delay(20);

                current = current.Previous;
            }

            await Task.Delay(1000);
            Overlay.Visibility = Visibility.Visible;
            OverlayText.Text = "Path Found!";
        }

        private async void StartSnakeGame()
        {
            _rows = 20;
            _columns = 20;

            _grid = CreateGrid();
            _state = new State(_rows, _columns);

            DrawGrid();

            // if the game loop is not running, start it
            if (!_isSnakeGameLoopRunning)
            {
                _isSnakeGameLoopRunning = true;
                await RunGame();
                _isSnakeGameLoopRunning = false;
            }
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
                    var image = new Image
                    {
                        Source = Images.Empty,
                        RenderTransformOrigin = new Point(0.5, 0.5) // center the rotation, otherwise it rotates around the top left corner
                    };

                    var textBlock = new TextBlock
                    {
                        Text = "", // set the value from the input.txt file
                        Foreground = Brushes.White,
                        FontSize = 14,
                        Margin = new Thickness(5),
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center
                    };

                    var grid = new Grid();
                    grid.Children.Add(image);
                    grid.Children.Add(textBlock);

                    grid.Tag = new Tuple<int, int>(row, column); // Store row and column information on the Grid element

                    // add click event to each grid element with the row and column as parameters
                    grid.MouseLeftButtonDown += GridElement_MouseLeftButtonDown;

                    // add to UniformGrid in the MainWindow
                    GameGrid.Children.Add(grid);
                    gridElements[row, column] = grid;
                }
            }

            return gridElements;
        }

        private void GridElement_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // check if type is of type Grid and if the Tag is of type Tuple<int, int>
            // if so, get the row and column index from the Tag
            if (sender is Grid clickedGrid && clickedGrid.Tag is Tuple<int, int>(var rowIndex, var columnIndex))
            {
                // Call your custom method with the row and column index
                HandleGridClick(rowIndex, columnIndex);
            }
        }

        private void HandleGridClick(int row, int column)
        {
            // Add your custom logic here based on the row and column of the clicked Grid element
            // MessageBox.Show($"Clicked on Grid at row {row} and column {column}");

            if (_isSnakeGameLoopRunning) return;

            // TODO: find neighbours of clicked grid element that we can move to
            var gridElement = _state.Grid[row, column];
            gridElement.Type = GridElementType.Food;

            DrawGrid();

            // TODO: add the neighbours to a list of possible moves
        }

        private void DrawGrid()
        {
            for (var r = 0; r < _rows; r++)
            {
                for (var c = 0; c < _columns; c++)
                {
                    var gridElement = _state.Grid[r, c];
                    var image = _grid[r, c].Children[0] as Image;

                    // TODO: instead of using type, use value, then create a rectangle instead of image
                    // the color can be based on the value

                    image!.Source = _gridValueToImage[gridElement.Type];
                    image.RenderTransform = Transform.Identity; // reset rotation

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

                    // textBlock.Text = gridElement.Value;
                    textBlock!.Text = gridElement.Step != -1 ? gridElement.Step.ToString() : gridElement.Value;
                }
            }
        }

        private async Task DrawDeadSnake()
        {
            var snakePositions = new List<Position>(_state.GetSnakePositions());

            for (var i = 0; i < snakePositions.Count(); i++)
            {
                var position = snakePositions[i];
                var imageSource = i == 0 ? Images.DeadHead : Images.DeadBody;

                var image = _grid[position.Row, position.Column].Children[0] as Image;
                image!.Source = imageSource;

                await Task.Delay(100);
            }
        }

        private void DrawSnakeHead()
        {
            var head = _state.GetSnakeHeadPosition();
            var image = _grid[head.Row, head.Column].Children[0] as Image;
            image!.Source = Images.Head;

            var rotation = _directionToRotation[_state.Direction];
            image.RenderTransform = new RotateTransform(rotation);
        }

        private async Task ShowCountDown()
        {
            for (var i = 3; i >= 0; i--)
            {
                OverlayText.Text = i.ToString();
                await Task.Delay(500);
            }
        }

        private async Task ShowGameOver()
        {
            await DrawDeadSnake();

            await Task.Delay(1000);
            Overlay.Visibility = Visibility.Visible;
            OverlayText.Text = "GAME OVER";

            await Task.Delay(2000);
            OverlayText.Text = "PRESS S TO PLAY AGAIN";
        }

        private async Task GameLoop()
        {
            while (!_state.IsGameOver)
            {
                await Task.Delay(100);
                _state.Move();
                Draw();
            }
        }

        private void Draw()
        {
            DrawGrid();
            DrawSnakeHead();
            ScoreText.Text = $"SCORE: {_state.Score}";
        }

        private async Task RunGame()
        {
            Draw();
            await ShowCountDown();
            Overlay.Visibility = Visibility.Hidden;
            await GameLoop();
            await ShowGameOver();

            // reset state
            _state = new State(_rows, _columns);
        }

        private void MainWindow_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (_state.IsGameOver)
            {
                return;
            }

            switch (e.Key)
            {
                case Key.Up:
                    _state.ChangeDirection(Direction.Up);
                    break;
                case Key.Down:
                    _state.ChangeDirection(Direction.Down);
                    break;
                case Key.Left:
                    _state.ChangeDirection(Direction.Left);
                    break;
                case Key.Right:
                    _state.ChangeDirection(Direction.Right);
                    break;
            }
        }

        private void MainWindow_OnPreviewKeyDown(object sender, KeyEventArgs e)
        {
            // if overlay is visible, don't allow any key presses to go through
            if (Overlay.Visibility == Visibility.Visible)
            {
                e.Handled = true;
            }

            // get key pressed
            var key = e.Key;
            switch (key)
            {
                case Key.S:
                    StartSnakeGame();
                    break;
                case Key.A:
                    StartAdventOfCode();
                    break;
                default:
                    return;
            }
        }
    }
}