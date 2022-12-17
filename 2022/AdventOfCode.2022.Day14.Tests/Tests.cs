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

        var rows = _solutionService.CreatePrintableOutput(result);

        // do not format this
        Assert.Equal(new string?[] { null, null, null, null, null, null, "+" , null, null, null }, rows[0]);
        Assert.Equal(new string?[] { null, null, null, null, null, null, null, null, null, null }, rows[1]);
        Assert.Equal(new string?[] { null, null, null, null, null, null, null, null, null, null }, rows[2]);
        Assert.Equal(new string?[] { null, null, null, null, null, null, null, null, null, null }, rows[3]);
        Assert.Equal(new string?[] { null, null, null, null, "#" , null, null, null, "#" , "#"  }, rows[4]);
        Assert.Equal(new string?[] { null, null, null, null, "#" , null, null, null, "#" , null }, rows[5]);
        Assert.Equal(new string?[] { null, null, "#" , "#" , "#" , null, null, null, "#" , null }, rows[6]);
        Assert.Equal(new string?[] { null, null, null, null, null, null, null, null, "#" , null }, rows[7]);
        Assert.Equal(new string?[] { null, null, null, null, null, null, null, null, "#" , null }, rows[8]);
        Assert.Equal(new string?[] { "#" , "#" , "#" , "#" , "#" , "#" , "#" , "#" , "#" , null }, rows[9]);
    }

    [Fact]
    public void CreateSequenceTest()
    {
        var startGrid = _solutionService.ParseInput(_input);
        var frames = _solutionService.CreateSequence(startGrid);

        Assert.Equal(0, frames[0].SandCount);
        Assert.Equal(0, frames[0].Step);

        var strings = _solutionService.CreatePrintableOutput(frames[0]);

        // do not format this
        Assert.Equal(new string?[] { null, null, null, null, null, null, "+" , null, null, null }, strings[0]);
        Assert.Equal(new string?[] { null, null, null, null, null, null, null, null, null, null }, strings[1]);
        Assert.Equal(new string?[] { null, null, null, null, null, null, null, null, null, null }, strings[2]);
        Assert.Equal(new string?[] { null, null, null, null, "#" , null, null, null, "#" , "#"  }, strings[3]);
        Assert.Equal(new string?[] { null, null, null, null, "#" , null, null, null, "#" , null }, strings[4]);
        Assert.Equal(new string?[] { null, null, "#" , "#" , "#" , null, null, null, "#" , null }, strings[5]);
        Assert.Equal(new string?[] { null, null, null, null, null, null, null, null, "#" , null }, strings[6]);
        Assert.Equal(new string?[] { null, null, null, null, null, null, null, null, "#" , null }, strings[7]);
        Assert.Equal(new string?[] { "#" , "#" , "#" , "#" , "#" , "#" , "#" , "#" , "#" , null }, strings[8]);

        Assert.Equal(1, frames[1].SandCount);
        Assert.Equal(1, frames[1].Step);

        strings = _solutionService.CreatePrintableOutput(frames[1]);

        // do not format this
        Assert.Equal(new string?[] { null, null, null, null, null, null, "+" , null, null, null }, strings[0]);
        Assert.Equal(new string?[] { null, null, null, null, null, null, "o" , null, null, null }, strings[1]);
        Assert.Equal(new string?[] { null, null, null, null, null, null, null, null, null, null }, strings[2]);
        Assert.Equal(new string?[] { null, null, null, null, "#" , null, null, null, "#" , "#"  }, strings[3]);
        Assert.Equal(new string?[] { null, null, null, null, "#" , null, null, null, "#" , null }, strings[4]);
        Assert.Equal(new string?[] { null, null, "#" , "#" , "#" , null, null, null, "#" , null }, strings[5]);
        Assert.Equal(new string?[] { null, null, null, null, null, null, null, null, "#" , null }, strings[6]);
        Assert.Equal(new string?[] { null, null, null, null, null, null, null, null, "#" , null }, strings[7]);
        Assert.Equal(new string?[] { "#" , "#" , "#" , "#" , "#" , "#" , "#" , "#" , "#" , null }, strings[8]);

        Assert.Equal(1, frames[2].SandCount);
        Assert.Equal(2, frames[2].Step);

        strings = _solutionService.CreatePrintableOutput(frames[2]);

        // do not format this
        Assert.Equal(new string?[] { null, null, null, null, null, null, "+" , null, null, null }, strings[0]);
        Assert.Equal(new string?[] { null, null, null, null, null, null, null, null, null, null }, strings[1]);
        Assert.Equal(new string?[] { null, null, null, null, null, null, "o" , null, null, null }, strings[2]);
        Assert.Equal(new string?[] { null, null, null, null, "#" , null, null, null, "#" , "#"  }, strings[3]);
        Assert.Equal(new string?[] { null, null, null, null, "#" , null, null, null, "#" , null }, strings[4]);
        Assert.Equal(new string?[] { null, null, "#" , "#" , "#" , null, null, null, "#" , null }, strings[5]);
        Assert.Equal(new string?[] { null, null, null, null, null, null, null, null, "#" , null }, strings[6]);
        Assert.Equal(new string?[] { null, null, null, null, null, null, null, null, "#" , null }, strings[7]);
        Assert.Equal(new string?[] { "#" , "#" , "#" , "#" , "#" , "#" , "#" , "#" , "#" , null }, strings[8]);

        strings = _solutionService.CreatePrintableOutput(frames[3]);

        Assert.Equal(1, frames[3].SandCount);
        Assert.Equal(3, frames[3].Step);

        // do not format this
        Assert.Equal(new string?[] { null, null, null, null, null, null, "+" , null, null, null }, strings[0]);
        Assert.Equal(new string?[] { null, null, null, null, null, null, null, null, null, null }, strings[1]);
        Assert.Equal(new string?[] { null, null, null, null, null, null, null, null, null, null }, strings[2]);
        Assert.Equal(new string?[] { null, null, null, null, "#" , null, "o" , null, "#" , "#"  }, strings[3]);
        Assert.Equal(new string?[] { null, null, null, null, "#" , null, null, null, "#" , null }, strings[4]);
        Assert.Equal(new string?[] { null, null, "#" , "#" , "#" , null, null, null, "#" , null }, strings[5]);
        Assert.Equal(new string?[] { null, null, null, null, null, null, null, null, "#" , null }, strings[6]);
        Assert.Equal(new string?[] { null, null, null, null, null, null, null, null, "#" , null }, strings[7]);
        Assert.Equal(new string?[] { "#" , "#" , "#" , "#" , "#" , "#" , "#" , "#" , "#" , null }, strings[8]);

        strings = _solutionService.CreatePrintableOutput(frames[7]);

        // do not format this
        Assert.Equal(new string?[] { null, null, null, null, null, null, "+" , null, null, null }, strings[0]);
        Assert.Equal(new string?[] { null, null, null, null, null, null, null, null, null, null }, strings[1]);
        Assert.Equal(new string?[] { null, null, null, null, null, null, null, null, null, null }, strings[2]);
        Assert.Equal(new string?[] { null, null, null, null, "#" , null, null, null, "#" , "#"  }, strings[3]);
        Assert.Equal(new string?[] { null, null, null, null, "#" , null, null, null, "#" , null }, strings[4]);
        Assert.Equal(new string?[] { null, null, "#" , "#" , "#" , null, null, null, "#" , null }, strings[5]);
        Assert.Equal(new string?[] { null, null, null, null, null, null, null, null, "#" , null }, strings[6]);
        Assert.Equal(new string?[] { null, null, null, null, null, null, "o" , null, "#" , null }, strings[7]);
        Assert.Equal(new string?[] { "#" , "#" , "#" , "#" , "#" , "#" , "#" , "#" , "#" , null }, strings[8]);
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