namespace Common;

public enum Direction
{
    Unknown,
    Up,
    Down,
    Left,
    Right
}

public class Cell
{
    public int X { get; set; }
    public int Y { get; set; }
    public char Value { get; set; }
    public long Steps { get; set; } = -1;
    public Cell? Previous { get; set; }
    public Direction Direction { get; set; } = Direction.Unknown;

    public override string ToString()
    {
        return $"{X},{Y} - {Value}";
    }
}

public static class Grid
{
    public static Cell[,] ApplyPadding(this Cell[,] grid, char paddingValue = 'X')
    {
        var numRows = grid.GetLength(0);
        var numCols = grid.GetLength(1);

        var newGrid = new Cell[numRows + 2, numCols + 2];

        for (var row = 0; row < numRows + 2; row++)
        {
            for (var col = 0; col < numCols + 2; col++)
            {
                if (row == 0 || row == numRows + 1 || col == 0 || col == numCols + 1)
                {
                    newGrid[row, col] = new Cell
                    {
                        X = row,
                        Y = col,
                        Value = paddingValue
                    };
                }
                else
                {
                    // newGrid[row, col] = grid[row - 1, col - 1];
                    newGrid[row, col] = new Cell
                    {
                        X = row,
                        Y = col,
                        Value = grid[row - 1, col - 1].Value
                    };
                }
            }
        }

        return newGrid;
    }

    // create a 2D array
    public static Cell[,] CreateGrid(string[] input)
    {
        var grid = new Cell[input.Length, input[0].Length];

        for (var row = 0; row < input.Length; row++)
        {
            string line = input[row];
            for (var col = 0; col < line.Length; col++)
            {
                grid[row, col] = new Cell
                {
                    X = row,
                    Y = col,
                    Value = line[col]
                };
            }
        }

        return grid;
    }

    public static List<T> GetRightValue<T>(this T[,] grid, int row, int col)
    {
        var rightValues = new List<T>();

        int numRows = grid.GetLength(0);
        int numCols = grid.GetLength(1);

        if (numCols - 1 > col)
        {
            rightValues.Add(grid[row, col + 1]);
        }

        return rightValues;
    }

    public static List<T> GetAdjacentValues<T>(this T[,] grid, int row, int col)
    {
        var adjacentValues = new List<T>();

        int numRows = grid.GetLength(0);
        int numCols = grid.GetLength(1);

        // Define the relative offsets for adjacent cells (4 possible directions).
        int[] dx = { -1, 1, 0, 0 }; // left, right, up, down
        int[] dy = { 0, 0, -1, 1 };

        // Check each adjacent cell.
        for (var i = 0; i < dx.Length; i++)
        {
            int newRow = row + dx[i];
            int newCol = col + dy[i];

            // Check if the adjacent cell is within the grid boundaries.
            if (newRow >= 0 && newRow < numRows && newCol >= 0 && newCol < numCols)
            {
                // adjacentValues[i] = grid[newRow, newCol];
                adjacentValues.Add(grid[newRow, newCol]);
            }
        }

        return adjacentValues;
    }

    // includes diagonals
    public static List<T> GetAllAdjacentValues<T>(this T[,] grid, int row, int col)
    {
        var adjacentValues = new List<T>();

        int numRows = grid.GetLength(0);
        int numCols = grid.GetLength(1);

        // Define the relative offsets for adjacent cells (8 possible directions).
        int[] dx = { -1, -1, -1, 0, 0, 1, 1, 1 }; // left and up, left, left and down, up, down, right and up, right, right and down
        int[] dy = { -1, 0, 1, -1, 1, -1, 0, 1 };

        // Check each adjacent cell.
        for (var i = 0; i < dx.Length; i++)
        {
            int newRow = row + dx[i];
            int newCol = col + dy[i];

            // Check if the adjacent cell is within the grid boundaries.
            if (newRow >= 0 && newRow < numRows && newCol >= 0 && newCol < numCols)
            {
                // adjacentValues[i] = grid[newRow, newCol];
                adjacentValues.Add(grid[newRow, newCol]);
            }
        }

        return adjacentValues;
    }

    /// <summary>
    /// return row and column of the first occurrence of c in grid
    /// </summary>
    public static (int, int) Find<T>(this T[,] grid, T c) where T : IComparable<T>
    {
        for (var x = 0; x < grid.GetLength(0); x++)
        {
            for (var y = 0; y < grid.GetLength(1); y++)
            {
                if (grid[x, y].Equals(c))
                {
                    // return grid[row, col];
                    return (x, y);
                    // return (col, row);
                }
            }
        }

        throw new Exception($"Could not find {c} in grid");
    }
}