namespace AdventOfCode._2022.Day1.Tests;

public class Tests : TestBed<TestFixture>
{
    private new readonly TestFixture _fixture;
    private readonly string[] _input = new[] { "1000", "2000", "3000", "", "4000", "", "5000", "6000", "", "7000", "8000", "9000", "", "10000" };

    public Tests(ITestOutputHelper testOutputHelper, TestFixture fixture) : base(testOutputHelper, fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public void SolutionExampleTest()
    {
        // arrange
        var service = _fixture.GetService<ISolutionService>(_testOutputHelper);

        // act
        _testOutputHelper.WriteLine("Running unit test 1");

        var result = service!.Run(_input);

        // assert
        Assert.Equal(4, result);
    }


    [Fact]
    public void GroupByElfTest()
    {
        // arrange
        var service = _fixture.GetService<ISolutionService>(_testOutputHelper);

        // act
        var result = service!.GroupByElf(_input);

        // assert
        Assert.Equal(6000, result[1]);
        Assert.Equal(4000, result[2]);
        Assert.Equal(11000, result[3]);
        Assert.Equal(24000, result[4]);
        Assert.Equal(10000, result[5]);
    }
}