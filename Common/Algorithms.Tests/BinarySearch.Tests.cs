namespace Algorithms.Tests;

public class UnitTest1
{
    [Fact]
    public void BinarySearch_SimpleIntList_ShouldFindIndex()
    {
        // arrange
        var list = new List<int> { 1, 2, 3, 4, 5, 6, 7 };
        var span = CollectionsMarshal.AsSpan(list);

        const int target = 5;

        // act
        var index = BinarySearch.FindIndex(span, target);

        // assert
        Assert.Equal(4, index);
    }
}