namespace AdventOfCode._2025.Day04;

public class Helper
{
    public int GetYear()
    {
        var ns = GetType().Namespace;
        return int.Parse(ns?.Split('.').ElementAt(1).Replace("_", "")!);
    }
    
    public int GetDay()
    {
        var ns = GetType().Namespace;
        return int.Parse(ns?.Split('.').Last().Replace("Day", "")!);
    }
}
