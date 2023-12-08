namespace Algorithms.Tests;

public class EuclideanAlgorithmTests
{
    [Fact]
    public void GreatestCommonDivisorTest()
    {
        // arrange
        var input = new int[] { 10, 15 };
        Span<int> span = input.AsSpan();

        // act
        long result = EuclideanAlgorithm.GreatestCommonDivisor(span);

        // assert
        Assert.Equal(5, result);
    }

    [Fact]
    public void LeastCommonMultipleTest()
    {
        // arrange
        var input = new int[] { 10, 15 };
        Span<int> span = input.AsSpan();

        // act
        long result = EuclideanAlgorithm.LeastCommonMultiple(span);

        // assert
        Assert.Equal(30, result);
    }

    [Fact]
    public void LeastCommonMultipleTest2()
    {
        // arrange
        var input = new int[] { 15529, 17873, 12599, 21389, 17287, 13771, 15529 };
        Span<int> span = input.AsSpan();

        // act
        long result = EuclideanAlgorithm.LeastCommonMultiple(span);

        // assert
        Assert.Equal(8245452805243, result);
    }

    [Fact]
    public void LeastCommonMultipleTest3()
    {
        // arrange
        var input = new long[] { 15529, 17873, 12599, 21389, 17287, 13771, 15529 };
        Span<long> span = input.AsSpan();

        // act
        long result = EuclideanAlgorithm.LeastCommonMultiple(span);

        // assert
        Assert.Equal(8245452805243, result);
    }
}