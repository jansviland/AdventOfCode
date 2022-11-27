namespace AdventOfCode._2021.Day1.Tests;

public class Tests : TestBed<TestFixture>
{
    private new readonly TestFixture _fixture;

    public Tests(ITestOutputHelper testOutputHelper, TestFixture fixture) : base(testOutputHelper, fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public void TestPart1()
    {
        _testOutputHelper.WriteLine("Running unit test 1");

        // arrange
        var service = _fixture.GetService<ISolutionService>(_testOutputHelper);
        var input = new[] { "199", "200", "208", "210", "200", "207", "240", "269", "260", "263" };

        // act
        var result = service!.Run(input);

        // assert
        Assert.Equal(7, result);
    }

    [Fact]
    public void TestSlidingWindow()
    {
        // arrange
        var service = _fixture.GetService<ISolutionService>(_testOutputHelper);
        var input = new[] { "199", "200", "208", "210", "200", "207", "240", "269", "260", "263" };

        // act

        // assert
        Assert.Equal(-1, service!.GetSlidingWindow(input, 0));
        Assert.Equal(-1, service!.GetSlidingWindow(input, 1));
        Assert.Equal(607, service!.GetSlidingWindow(input, 2));
        Assert.Equal(618, service!.GetSlidingWindow(input, 3));
        Assert.Equal(618, service!.GetSlidingWindow(input, 4));
        Assert.Equal(617, service!.GetSlidingWindow(input, 5));
        Assert.Equal(647, service!.GetSlidingWindow(input, 6));
        Assert.Equal(716, service!.GetSlidingWindow(input, 7));
        Assert.Equal(769, service!.GetSlidingWindow(input, 8));
        Assert.Equal(792, service!.GetSlidingWindow(input, 9));
    }

    [Fact]
    public void TestPart2()
    {
        _testOutputHelper.WriteLine("Running unit test 2");

        // arrange
        var service = _fixture.GetService<ISolutionService>(_testOutputHelper);
        var input = new[] { "199", "200", "208", "210", "200", "207", "240", "269", "260", "263" };

        // act
        var result = service!.RunPart2(input);

        // assert
        Assert.Equal(5, result);
    }
}