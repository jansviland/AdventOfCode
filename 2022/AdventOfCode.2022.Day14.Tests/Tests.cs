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

        var rows = _solutionService.ConvertToStrings(result);

        // do not format this
        Assert.Equal(new string?[] { null, null, null, null, null, null, "+" , null, null, null }, rows[0]);
        Assert.Equal(new string?[] { null, null, null, null, null, null, null, null, null, null }, rows[1]);
        Assert.Equal(new string?[] { null, null, null, null, null, null, null, null, null, null }, rows[2]);
        Assert.Equal(new string?[] { null, null, null, null, "#" , null, null, null, "#" , "#"  }, rows[3]);
        Assert.Equal(new string?[] { null, null, null, null, "#" , null, null, null, "#" , null }, rows[4]);
        Assert.Equal(new string?[] { null, null, "#" , "#" , "#" , null, null, null, "#" , null }, rows[5]);
        Assert.Equal(new string?[] { null, null, null, null, null, null, null, null, "#" , null }, rows[6]);
        Assert.Equal(new string?[] { null, null, null, null, null, null, null, null, "#" , null }, rows[7]);
        Assert.Equal(new string?[] { "#" , "#" , "#" , "#" , "#" , "#" , "#" , "#" , "#" , null }, rows[8]);
    }

    [Fact]
    public void CreateSequenceTest()
    {
        var startGrid = _solutionService.ParseInput(_input);
        var sequence = _solutionService.CreateSequence(startGrid);

        var rows = _solutionService.ConvertToStrings(sequence[0]);

        // do not format this
        Assert.Equal(new string?[] { null, null, null, null, null, null, "+" , null, null, null }, rows[0]);
        Assert.Equal(new string?[] { null, null, null, null, null, null, null, null, null, null }, rows[1]);
        Assert.Equal(new string?[] { null, null, null, null, null, null, null, null, null, null }, rows[2]);
        Assert.Equal(new string?[] { null, null, null, null, "#" , null, null, null, "#" , "#"  }, rows[3]);
        Assert.Equal(new string?[] { null, null, null, null, "#" , null, null, null, "#" , null }, rows[4]);
        Assert.Equal(new string?[] { null, null, "#" , "#" , "#" , null, null, null, "#" , null }, rows[5]);
        Assert.Equal(new string?[] { null, null, null, null, null, null, null, null, "#" , null }, rows[6]);
        Assert.Equal(new string?[] { null, null, null, null, null, null, null, null, "#" , null }, rows[7]);
        Assert.Equal(new string?[] { "#" , "#" , "#" , "#" , "#" , "#" , "#" , "#" , "#" , null }, rows[8]);

        rows = _solutionService.ConvertToStrings(sequence[1]);

        // do not format this
        Assert.Equal(new string?[] { null, null, null, null, null, null, "+" , null, null, null }, rows[0]);
        Assert.Equal(new string?[] { null, null, null, null, null, null, "o" , null, null, null }, rows[1]);
        Assert.Equal(new string?[] { null, null, null, null, null, null, null, null, null, null }, rows[2]);
        Assert.Equal(new string?[] { null, null, null, null, "#" , null, null, null, "#" , "#"  }, rows[3]);
        Assert.Equal(new string?[] { null, null, null, null, "#" , null, null, null, "#" , null }, rows[4]);
        Assert.Equal(new string?[] { null, null, "#" , "#" , "#" , null, null, null, "#" , null }, rows[5]);
        Assert.Equal(new string?[] { null, null, null, null, null, null, null, null, "#" , null }, rows[6]);
        Assert.Equal(new string?[] { null, null, null, null, null, null, null, null, "#" , null }, rows[7]);
        Assert.Equal(new string?[] { "#" , "#" , "#" , "#" , "#" , "#" , "#" , "#" , "#" , null }, rows[8]);

        rows = _solutionService.ConvertToStrings(sequence[2]);

        // do not format this
        Assert.Equal(new string?[] { null, null, null, null, null, null, "+" , null, null, null }, rows[0]);
        Assert.Equal(new string?[] { null, null, null, null, null, null, null, null, null, null }, rows[1]);
        Assert.Equal(new string?[] { null, null, null, null, null, null, "o" , null, null, null }, rows[2]);
        Assert.Equal(new string?[] { null, null, null, null, "#" , null, null, null, "#" , "#"  }, rows[3]);
        Assert.Equal(new string?[] { null, null, null, null, "#" , null, null, null, "#" , null }, rows[4]);
        Assert.Equal(new string?[] { null, null, "#" , "#" , "#" , null, null, null, "#" , null }, rows[5]);
        Assert.Equal(new string?[] { null, null, null, null, null, null, null, null, "#" , null }, rows[6]);
        Assert.Equal(new string?[] { null, null, null, null, null, null, null, null, "#" , null }, rows[7]);
        Assert.Equal(new string?[] { "#" , "#" , "#" , "#" , "#" , "#" , "#" , "#" , "#" , null }, rows[8]);

        rows = _solutionService.ConvertToStrings(sequence[3]);

        // do not format this
        Assert.Equal(new string?[] { null, null, null, null, null, null, "+" , null, null, null }, rows[0]);
        Assert.Equal(new string?[] { null, null, null, null, null, null, null, null, null, null }, rows[1]);
        Assert.Equal(new string?[] { null, null, null, null, null, null, null, null, null, null }, rows[2]);
        Assert.Equal(new string?[] { null, null, null, null, "#" , null, "o" , null, "#" , "#"  }, rows[3]);
        Assert.Equal(new string?[] { null, null, null, null, "#" , null, null, null, "#" , null }, rows[4]);
        Assert.Equal(new string?[] { null, null, "#" , "#" , "#" , null, null, null, "#" , null }, rows[5]);
        Assert.Equal(new string?[] { null, null, null, null, null, null, null, null, "#" , null }, rows[6]);
        Assert.Equal(new string?[] { null, null, null, null, null, null, null, null, "#" , null }, rows[7]);
        Assert.Equal(new string?[] { "#" , "#" , "#" , "#" , "#" , "#" , "#" , "#" , "#" , null }, rows[8]);

        rows = _solutionService.ConvertToStrings(sequence[7]);

        // do not format this
        Assert.Equal(new string?[] { null, null, null, null, null, null, "+" , null, null, null }, rows[0]);
        Assert.Equal(new string?[] { null, null, null, null, null, null, null, null, null, null }, rows[1]);
        Assert.Equal(new string?[] { null, null, null, null, null, null, null, null, null, null }, rows[2]);
        Assert.Equal(new string?[] { null, null, null, null, "#" , null, null, null, "#" , "#"  }, rows[3]);
        Assert.Equal(new string?[] { null, null, null, null, "#" , null, null, null, "#" , null }, rows[4]);
        Assert.Equal(new string?[] { null, null, "#" , "#" , "#" , null, null, null, "#" , null }, rows[5]);
        Assert.Equal(new string?[] { null, null, null, null, null, null, null, null, "#" , null }, rows[6]);
        Assert.Equal(new string?[] { null, null, null, null, null, null, "o" , null, "#" , null }, rows[7]);
        Assert.Equal(new string?[] { "#" , "#" , "#" , "#" , "#" , "#" , "#" , "#" , "#" , null }, rows[8]);
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