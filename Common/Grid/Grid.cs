namespace Grid;

public static class Grid
{
    // create a 2D array of chars
    public static char[,] CreateGrid(string[] input)
    {
        var grid = new char[input.Length, input[0].Length];

        for (var row = 0; row < input.Length; row++)
        {
            string line = input[row];
            for (var col = 0; col < line.Length; col++)
            {
                grid[row, col] = line[col];
            }
        }

        return grid;
    }

    public static Span<T> GetAdjacentValues<T>(this T[,] grid, int row, int col)
    {
        var adjacentValues = new Span<T>();

        int numRows = grid.GetLength(0);
        int numCols = grid.GetLength(1);

        // Define the relative offsets for adjacent cells (8 possible directions).
        int[] dx = { -1, -1, -1, 0, 0, 1, 1, 1 };
        int[] dy = { -1, 0, 1, -1, 1, -1, 0, 1 };

        // Check each adjacent cell.
        for (var i = 0; i < dx.Length; i++)
        {
            int newRow = row + dx[i];
            int newCol = col + dy[i];

            // Check if the adjacent cell is within the grid boundaries.
            if (newRow >= 0 && newRow < numRows && newCol >= 0 && newCol < numCols)
            {
                adjacentValues[i] = grid[newRow, newCol];
            }
        }

        return adjacentValues;
    }
}