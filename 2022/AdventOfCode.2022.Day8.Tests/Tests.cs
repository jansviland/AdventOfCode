namespace AdventOfCode._2022.Day8.Tests;

public class Tests : TestBed<TestFixture>
{
    private readonly ISolutionService _solutionService;

    private readonly string[] _input = new[]
    {
        "30373",
        "25512",
        "65332",
        "33549",
        "35390",
    };

    public Tests(ITestOutputHelper testOutputHelper, TestFixture fixture) : base(testOutputHelper, fixture)
    {
        _solutionService = _fixture.GetService<ISolutionService>(_testOutputHelper)!;
    }

    [Fact]
    public void Part1Test()
    {
        _testOutputHelper.WriteLine("Running unit test - Part 1");

        // arrange
        // act
        var result = _solutionService.RunPart1(_input);

        // assert
        Assert.Equal(21, result);
    }

    [Fact]
    public void ParseInputTest()
    {
        // arrange
        // act
        var result = _solutionService.ParseInput(_input);

        // assert
        Assert.Equal(25, result.Length);

        Assert.Equal(3, result[0, 0]);
        Assert.Equal(3, result[0, 4]);
        Assert.Equal(3, result[4, 0]);
        Assert.Equal(0, result[4, 4]);

        Assert.Equal(2, result[0, 1]);
        Assert.Equal(6, result[0, 2]);
        Assert.Equal(3, result[0, 3]);
        Assert.Equal(3, result[0, 4]);

        Assert.Equal(5, result[2, 1]);
        Assert.Equal(3, result[2, 2]);
        Assert.Equal(5, result[2, 3]);
        Assert.Equal(3, result[2, 4]);
    }

    [Fact]
    public void Part2Test()
    {
        _testOutputHelper.WriteLine("Running unit test - Part 2");

        // arrange
        // act
        var result = _solutionService.RunPart2(_input);

        // assert
        Assert.Equal(45000, result);
    }
}