namespace AdventOfCode._2021.Day1.Tests;

public class Tests : TestBed<TestFixture>
{
    private new readonly TestFixture _fixture;

    public Tests(ITestOutputHelper testOutputHelper, TestFixture fixture) : base(testOutputHelper, fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public void Test1()
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
    public void Test2()
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