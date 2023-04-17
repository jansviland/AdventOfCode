namespace AdventOfCode._2022.Day12.Common.Models;

public enum GridElementType
{
    Empty,
    Snake,
    Food,
    OutOfBounds,
    Path
    // Wall
}

public class GridElement
{
    public int Row { get; set; } // row
    public int Column { get; set; } // column
    public GridElementType Type { get; set; }
    public string Value { get; set; }
    public int Step { get; set; } = -1; // number of steps to get to this position, can also be called "Cost"
    public GridElement? Previous { get; set; }
    public int Distance { get; set; } // manhattan distance to end goal, also called "Heuristic"
    public int TotalCost { get; set; } // Step (cost) + Distance (heuristic) = TotalCost

    public GridElement()
    {
        Type = GridElementType.Empty;
        Value = "";
    }

    public GridElement(GridElementType type, string value, int row, int column)
    {
        Type = type;
        Value = value;
        Row = row;
        Column = column;
    }
}