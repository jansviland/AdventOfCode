namespace AdventOfCode._2022.Day14.Tests;

public class Tests : TestBed<TestFixture>
{
    private readonly ISolutionService _solutionService;

    private readonly string[] _input = new[]
    {
        "498,4 -> 498,6 -> 496,6",
        "503,4 -> 502,4 -> 502,9 -> 494,9",
    };

    public Tests(ITestOutputHelper testOutputHelper, TestFixture fixture) : base(testOutputHelper, fixture)
    {
        _solutionService = _fixture.GetService<ISolutionService>(_testOutputHelper)!;
    }

    [Fact]
    public void ParseInputTest()
    {
        var result = _solutionService.ParseInput(_input);
        Assert.Equal(494, result.XMin);
        Assert.Equal(503, result.XMax);
        Assert.Equal(4, result.YMin);
        Assert.Equal(9, result.YMax);

        // get first row
        var length = result.XMax - result.XMin + 1;

        string[] firstRow = new string[length];
        for (var i = 0; i < length; i++)
        {
            firstRow[i] = result.values[i, 0];
            // for (var j = 0; j < result.GetLength(1); j++)
            // {
            //     firstRow[j] = result[i, j];
            // }
        }

        Assert.Equal(new[] { ".", ".", ".", ".", ".", ".", "+", ".", ".", "." }, firstRow);
    }

    [Fact]
    public void Part1Test()
    {
        _testOutputHelper.WriteLine("Running unit test - Part 1");

        // arrange
        // act
        var result = _solutionService.RunPart1(_input);

        // assert
        Assert.Equal(24, result);
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