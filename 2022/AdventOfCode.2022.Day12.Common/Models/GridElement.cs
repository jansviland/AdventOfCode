namespace AdventOfCode._2022.Day12.Common.Models;

public enum GridElementType
{
    Empty,
    Snake,
    Food,
    OutOfBounds,
    // Wall
}

public class GridElement
{
    public int Row { get; set; } // row
    public int Column { get; set; } // column
    public GridElementType Type { get; set; }
    public string Value { get; set; }
    public int Step { get; set; } = -1; // number of steps to get to this position
    public GridElement? Previous { get; set; }

    public GridElement()
    {
        this.Type = GridElementType.Empty;
        this.Value = "";
    }

    public GridElement(GridElementType type, string value, int row, int column)
    {
        Type = type;
        Value = value;
        Row = row;
        Column = column;
    }
}