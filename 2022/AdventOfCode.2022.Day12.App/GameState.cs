using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using AdventOfCode._2022.Day12.Common.Models;

namespace AdventOfCode._2022.Day12.App;

public class GameState
{
    private int Rows { get; }
    private int Columns { get; }
    public GridElement[,] Grid { get; }
    public Direction Direction { get; set; }
    public int Score { get; private set; }
    public bool IsGameOver { get; private set; }

    /// <summary>
    /// We use a linked list because it allows us to easily remove the tail of the snake.
    /// The first element in the list is the head of the snake. The last element is the tail.
    /// </summary>
    private readonly LinkedList<Position> _snakePositions = new LinkedList<Position>();

    /// <summary>
    /// If the player changes direction, we don't want to change direction immediately.
    /// We store the direction changes in a buffer and apply them at the end of the game loop.
    /// This way the user can enter left and down in quick succession and the snake will turn first left and then down.
    /// </summary>
    private readonly LinkedList<Direction> _directionChangeBuffer = new LinkedList<Direction>();

    /// <summary>
    /// Will be used to figure out where to place the food
    /// </summary>
    private readonly Random _random = new Random();

    public GameState(int rows, int columns)
    {
        Rows = rows;
        Columns = columns;

        // init grid with new GridElement objects
        Grid = new GridElement[rows, columns];
        for (var r = 0; r < Rows; r++)
        {
            for (var c = 0; c < Columns; c++)
            {
                Grid[r, c] = new GridElement();
            }
        }

        Direction = Direction.Right;
        Score = 0;
        IsGameOver = false;

        AddSnake();
        AddFood();
    }

    private void AddSnake()
    {
        var r = Rows / 2; // middle row

        // start the snake in the middle of the grid, 3 cells wide, column 1, facing right
        for (var c = 1; c <= 3; c++)
        {
            Grid[r, c].Type = GridElementType.Snake;
            _snakePositions.AddFirst(new Position(r, c));
        }
    }

    /// <summary>
    /// Get a list of all the empty positions in the grid.
    /// </summary>
    private IEnumerable<Position> EmptyPositions()
    {
        for (var r = 0; r < Rows; r++)
        {
            for (var c = 0; c < Columns; c++)
            {
                if (Grid[r, c].Type == GridElementType.Empty)
                {
                    yield return new Position(r, c);
                }
            }
        }
    }

    private void AddFood()
    {
        // get list of all empty positions
        var emptyPositions = new List<Position>(EmptyPositions());
        if (emptyPositions.Count == 0)
        {
            // TODO: set game over?
            return;
        }

        // pick a random empty position in the list
        var position = emptyPositions[_random.Next(0, emptyPositions.Count)];

        // place food at that position
        Grid[position.Row, position.Column].Type = GridElementType.Food;
    }

    public Position GetSnakeHeadPosition()
    {
        if (_snakePositions.FirstOrDefault() == null)
        {
            throw new InvalidOperationException("Snake has no head");
        }

        return _snakePositions.First!.Value;
    }

    private Position GetSnakeTailPosition()
    {
        if (_snakePositions.LastOrDefault() == null)
        {
            throw new InvalidOperationException("Snake has no tail");
        }

        return _snakePositions.Last!.Value;
    }

    public IEnumerable<Position> GetSnakePositions()
    {
        return _snakePositions;
    }

    private void AddSnakeHead(Position position)
    {
        Grid[position.Row, position.Column].Type = GridElementType.Snake;
        _snakePositions.AddFirst(position);
    }

    private void RemoveSnakeTail()
    {
        var tailPosition = _snakePositions.Last.Value;
        Grid[tailPosition.Row, tailPosition.Column].Type = GridElementType.Empty;
        _snakePositions.RemoveLast();
    }

    private Direction GetLastDirectionChange()
    {
        if (_directionChangeBuffer.Count == 0)
        {
            // return current direction if there are no direction changes in the buffer
            return Direction;
        }

        var lastDirectionChange = _directionChangeBuffer.Last.Value;
        _directionChangeBuffer.RemoveLast();

        return lastDirectionChange;
    }

    private bool CanChangeDirection(Direction newDirection)
    {
        if (_directionChangeBuffer.Count == 2)
        {
            // only allow two direction changes per game loop
            return false;
        }

        var lastDirectionChange = GetLastDirectionChange();

        // we can only change direction if the new direction is not the opposite of the current direction
        // and we only add the direction change to the buffer if it is not the same as the last direction change
        return newDirection != lastDirectionChange && newDirection != lastDirectionChange.GetOppositeDirection();
    }

    public void ChangeDirection(Direction direction)
    {
        if (!CanChangeDirection(direction))
        {
            return;
        }

        // we do not change direction immediately, but instead add the direction change to a buffer.
        _directionChangeBuffer.AddLast(direction);
    }

    private bool OutOfBounds(Position position)
    {
        return position.Row < 0 || position.Row >= Rows || position.Column < 0 || position.Column >= Columns;
    }

    private GridElementType WillHit(Position newSnakeHeadPosition)
    {
        if (OutOfBounds(newSnakeHeadPosition))
        {
            return GridElementType.OutOfBounds;
        }

        // Special case: if the snake is about to hit it's own tail, we allow this.
        // Since the tail will be removed as we move forward, this position will be empty.
        if (newSnakeHeadPosition == GetSnakeTailPosition())
        {
            return GridElementType.Empty;
        }

        return Grid[newSnakeHeadPosition.Row, newSnakeHeadPosition.Column].Type;
    }

    public void Move()
    {
        // BUG: if you click up and left very fast as soon as you start. The snake crash into itself...
        // set time between update to a large number, and debug the game loop

        if (_directionChangeBuffer.Count > 0)
        {
            // if there are any direction changes in the buffer, apply the last one
            Direction = GetLastDirectionChange();
        }

        var newSnakeHeadPosition = GetSnakeHeadPosition().GetNextPosition(Direction);

        var willHit = WillHit(newSnakeHeadPosition);
        switch (willHit)
        {
            case GridElementType.Empty:
                AddSnakeHead(newSnakeHeadPosition);
                RemoveSnakeTail();
                break;
            case GridElementType.Food:
                AddSnakeHead(newSnakeHeadPosition);
                AddFood();
                Score++;
                break;
            case GridElementType.Snake:
            case GridElementType.OutOfBounds:
                IsGameOver = true;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}