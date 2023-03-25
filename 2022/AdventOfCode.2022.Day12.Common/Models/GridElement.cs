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
    public GridElementType Type { get; set; }
    public string Value { get; set; }

    public GridElement()
    {
        this.Type = GridElementType.Empty;
        this.Value = "";
    }

    public GridElement(GridElementType type, string value)
    {
        Type = type;
        Value = value;
    }
}