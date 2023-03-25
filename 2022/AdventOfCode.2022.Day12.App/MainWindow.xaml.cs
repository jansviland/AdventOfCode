using System.Collections.Generic;
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
    public partial class MainWindow : Window
    {
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

        private const int Rows = 20;
        private const int Columns = 20;
        private readonly Image[,] _gridImages;

        private bool _isGameLoopRunning;
        private GameState _gameState;

        public MainWindow()
        {
            InitializeComponent();

            _gridImages = CreateGrid();
            _gameState = new GameState(Rows, Columns);
        }

        private Image[,] CreateGrid()
        {
            var images = new Image[Rows, Columns];
            GameGrid.Rows = Rows;
            GameGrid.Columns = Columns;
            GameGrid.Width = GameGrid.Height * (Columns / (double)Rows);

            for (var row = 0; row < Rows; row++)
            {
                for (var column = 0; column < Columns; column++)
                {
                    // set all grid positions to empty with the image for empty to begin with
                    var image = new Image
                    {
                        Source = Images.Empty,
                        RenderTransformOrigin = new Point(0.5, 0.5) // center the rotation, otherwise it rotates around the top left corner
                    };
                    images[row, column] = image;

                    // add to UniformGrid in the MainWindow
                    GameGrid.Children.Add(image);
                }
            }

            return images;
        }

        private async Task DrawDeadSnake()
        {
            var snakePositions = new List<Position>(_gameState.GetSnakePositions());

            for (var i = 0; i < snakePositions.Count(); i++)
            {
                var position = snakePositions[i];
                var imageSource = i == 0 ? Images.DeadHead : Images.DeadBody;

                _gridImages[position.Row, position.Column].Source = imageSource;

                await Task.Delay(100);
            }
        }

        private void DrawSnakeHead()
        {
            var head = _gameState.GetSnakeHeadPosition();
            var image = _gridImages[head.Row, head.Column];
            image.Source = Images.Head;

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
            for (var r = 0; r < Rows; r++)
            {
                for (var c = 0; c < Columns; c++)
                {
                    var gridElement = _gameState.Grid[r, c];
                    _gridImages[r, c].Source = _gridValueToImage[gridElement.Type];
                    _gridImages[r, c].RenderTransform = Transform.Identity; // reset rotation
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

            _gameState = new GameState(Rows, Columns);
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