namespace AdventOfCode._2022.Day1.Tests;

public class Day1
{
    [Fact]
    public void Test1()
    {
        // arrange
        // act
        var result = Day1Solution.Solve(123);

        // assert
        Assert.Equal(123, result);
    }
}