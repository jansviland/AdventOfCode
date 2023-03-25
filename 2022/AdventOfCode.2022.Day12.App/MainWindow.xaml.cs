using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using AdventOfCode._2022.Day12.App.Models;
using AdventOfCode._2022.Day12.Common.Models;

namespace AdventOfCode._2022.Day12.App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
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

        private readonly int _rows = 20;
        private readonly int _columns = 20;
        private readonly Grid[,] _grid;

        private bool _isGameLoopRunning;
        private GameState _gameState;

        public MainWindow()
        {
            InitializeComponent();
            _grid = CreateGrid();
            _gameState = new GameState(_rows, _columns);
        }

        // public MainWindow(ISolutionService solutionService)
        // {
        //     _solutionService = solutionService;
        //
        //     InitializeComponent();
        //
        //     string[] input = File.ReadAllLines("Assets/sample-input.txt");
        //
        //     // parse input
        //     var grid = _solutionService.ParseInput(input);
        //
        //     // set rows and columns
        //     _rows = grid.GetLength(0);
        //     _columns = grid.GetLength(1);
        //
        //     // create UI elements
        //     _grid = CreateGrid();
        //
        //     // update game state
        //     _gameState = new GameState(grid);
        //
        //     // update UI elements with correct images
        //     DrawGrid();
        //     Overlay.Visibility = Visibility.Hidden;
        // }

        private Grid[,] CreateGrid()
        {
            // var images = new Image[_rows, _columns];
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
                    // images[row, column] = image;

                    TextBlock textBlock = new TextBlock();
                    textBlock.Text = "a";
                    textBlock.Foreground = Brushes.White;
                    textBlock.FontSize = 14;
                    textBlock.Margin = new Thickness(5);
                    textBlock.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
                    textBlock.VerticalAlignment = System.Windows.VerticalAlignment.Center;

                    Grid grid = new Grid();
                    grid.Children.Add(image);
                    grid.Children.Add(textBlock);

                    // add to UniformGrid in the MainWindow
                    GameGrid.Children.Add(grid);
                    gridElements[row, column] = grid;
                }
            }

            // return images;
            return gridElements;
        }

        private async Task DrawDeadSnake()
        {
            var snakePositions = new List<Position>(_gameState.GetSnakePositions());

            for (var i = 0; i < snakePositions.Count(); i++)
            {
                var position = snakePositions[i];
                var imageSource = i == 0 ? Images.DeadHead : Images.DeadBody;

                var image = _grid[position.Row, position.Column].Children[0] as Image;
                image.Source = imageSource;

                // _gridImages[position.Row, position.Column].Source = imageSource;

                await Task.Delay(100);
            }
        }

        private void DrawSnakeHead()
        {
            var head = _gameState.GetSnakeHeadPosition();
            var image = _grid[head.Row, head.Column].Children[0] as Image;
            image.Source = Images.Head;

            // var image = _gridImages[head.Row, head.Column];
            // image.Source = Images.Head;

            var rotation = _directionToRotation[_gameState.Direction];
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
            OverlayText.Text = "PRESS ANY KEY TO PLAY AGAIN";
        }

        private async Task GameLoop()
        {
            while (!_gameState.IsGameOver)
            {
                await Task.Delay(100);
                _gameState.Move();
                Draw();
            }
        }

        private void Draw()
        {
            DrawGrid();
            DrawSnakeHead();
            ScoreText.Text = $"SCORE: {_gameState.Score}";
        }

        private void DrawGrid()
        {
            for (var r = 0; r < _rows; r++)
            {
                for (var c = 0; c < _columns; c++)
                {
                    var gridElement = _gameState.Grid[r, c];
                    var image = _grid[r, c].Children[0] as Image;

                    image.Source = _gridValueToImage[gridElement.Type];
                    image.RenderTransform = Transform.Identity; // reset rotation

                    // _grid[r, c].Source = _gridValueToImage[gridElement.Type];
                    // _grid[r, c].RenderTransform = Transform.Identity; // reset rotation
                }
            }
        }

        private void MainWindow_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (_gameState.IsGameOver)
            {
                return;
            }

            switch (e.Key)
            {
                case Key.Up:
                    _gameState.ChangeDirection(Direction.Up);
                    break;
                case Key.Down:
                    _gameState.ChangeDirection(Direction.Down);
                    break;
                case Key.Left:
                    _gameState.ChangeDirection(Direction.Left);
                    break;
                case Key.Right:
                    _gameState.ChangeDirection(Direction.Right);
                    break;
            }
        }

        private async Task RunGame()
        {
            Draw();
            await ShowCountDown();
            Overlay.Visibility = Visibility.Hidden;
            await GameLoop();
            await ShowGameOver();

            _gameState = new GameState(_rows, _columns);
        }

        private async void MainWindow_OnPreviewKeyDown(object sender, KeyEventArgs e)
        {
            // if overlay is visible, don't allow any key presses to go through
            if (Overlay.Visibility == Visibility.Visible)
            {
                e.Handled = true;
            }

            // if the game loop is not running, start it
            if (!_isGameLoopRunning)
            {
                _isGameLoopRunning = true;
                await RunGame();
                _isGameLoopRunning = false;
            }
        }
    }
}