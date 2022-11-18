namespace AdventOfCode._2022.Day1;

internal static class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("args: " + string.Join(", ", args));

        var result = Day1Solution.Solve(123);
        Console.WriteLine("Result: " + result);
    }
}