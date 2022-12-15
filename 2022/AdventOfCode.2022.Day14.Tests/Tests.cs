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
        Assert.Equal(0, result.YMin);
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

        Assert.Equal(new[] { null, null, null, null, null, null, "+", null, null, null }, firstRow);

        // TODO: test second row
    }

    [Fact]
    public void CreatePrintableOutputTest()
    {
        // arrange
        var grid = _solutionService.ParseInput(_input);

        // act
        var result = _solutionService.CreatePrintableOutput(grid);

        // assert
        Assert.Equal(_input[0], result[0]);
        Assert.Equal(_input[1], result[1]);
        Assert.Equal(_input[2], result[2]);
        Assert.Equal(_input[3], result[3]);
        Assert.Equal(_input[4], result[4]);
        Assert.Equal(_input[5], result[5]);
        Assert.Equal(_input[6], result[6]);
        Assert.Equal(_input[7], result[7]);
        Assert.Equal(_input[8], result[8]);
        Assert.Equal(_input[9], result[9]);
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