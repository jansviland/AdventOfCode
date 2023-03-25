namespace AdventOfCode._2022.Day12.Common.Models;

public class Direction
{
    public static readonly Direction Left = new Direction(0, -1);
    public static readonly Direction Right = new Direction(0, 1);
    public static readonly Direction Up = new Direction(-1, 0);
    public static readonly Direction Down = new Direction(1, 0);

    public int RowOffset { get; }
    public int ColumnOffset { get; }

    private Direction(int rowOffset, int columnOffset)
    {
        RowOffset = rowOffset;
        ColumnOffset = columnOffset;
    }

    public Direction GetOppositeDirection()
    {
        return new Direction(-RowOffset, -ColumnOffset);
    }

    protected bool Equals(Direction other)
    {
        return RowOffset == other.RowOffset && ColumnOffset == other.ColumnOffset;
    }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Direction)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(RowOffset, ColumnOffset);
    }

    public static bool operator ==(Direction left, Direction right)
    {
        return EqualityComparer<Direction>.Default.Equals(left, right);
    }

    public static bool operator !=(Direction left, Direction right)
    {
        return !(left == right);
    }
}