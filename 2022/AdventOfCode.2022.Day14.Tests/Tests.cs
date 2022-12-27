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

        var strings = _solutionService.CreatePrintableOutput(result);

        // do not format this
        Assert.Equal(new string?[] { null, null, null, null, null, null, "+" , null, null, null }, strings[0]);
        Assert.Equal(new string?[] { null, null, null, null, null, null, null, null, null, null }, strings[1]);
        Assert.Equal(new string?[] { null, null, null, null, null, null, null, null, null, null }, strings[2]);
        Assert.Equal(new string?[] { null, null, null, null, null, null, null, null, null, null }, strings[3]);
        Assert.Equal(new string?[] { null, null, null, null, "#" , null, null, null, "#" , "#"  }, strings[4]);
        Assert.Equal(new string?[] { null, null, null, null, "#" , null, null, null, "#" , null }, strings[5]);
        Assert.Equal(new string?[] { null, null, "#" , "#" , "#" , null, null, null, "#" , null }, strings[6]);
        Assert.Equal(new string?[] { null, null, null, null, null, null, null, null, "#" , null }, strings[7]);
        Assert.Equal(new string?[] { null, null, null, null, null, null, null, null, "#" , null }, strings[8]);
        Assert.Equal(new string?[] { "#" , "#" , "#" , "#" , "#" , "#" , "#" , "#" , "#" , null }, strings[9]);
    }

    [Fact]
    public void ParseInputPart2Test()
    {
        var result = _solutionService.ParseInputPart2(_input);

        Assert.Equal(494, result.XMin);
        Assert.Equal(503, result.XMax);
        Assert.Equal(0, result.YMin);
        Assert.Equal(11, result.YMax);

        var strings = _solutionService.CreatePrintableOutput(result);

        // do not format this
        Assert.Equal(new string?[] { null, null, null, null, null, null, "+" , null, null, null }, strings[0]);
        Assert.Equal(new string?[] { null, null, null, null, null, null, null, null, null, null }, strings[1]);
        Assert.Equal(new string?[] { null, null, null, null, null, null, null, null, null, null }, strings[2]);
        Assert.Equal(new string?[] { null, null, null, null, null, null, null, null, null, null }, strings[3]);
        Assert.Equal(new string?[] { null, null, null, null, "#" , null, null, null, "#" , "#"  }, strings[4]);
        Assert.Equal(new string?[] { null, null, null, null, "#" , null, null, null, "#" , null }, strings[5]);
        Assert.Equal(new string?[] { null, null, "#" , "#" , "#" , null, null, null, "#" , null }, strings[6]);
        Assert.Equal(new string?[] { null, null, null, null, null, null, null, null, "#" , null }, strings[7]);
        Assert.Equal(new string?[] { null, null, null, null, null, null, null, null, "#" , null }, strings[8]);
        Assert.Equal(new string?[] { "#" , "#" , "#" , "#" , "#" , "#" , "#" , "#" , "#" , null }, strings[9]);
        Assert.Equal(new string?[] { null, null, null, null, null, null, null, null, null, null }, strings[10]);
        Assert.Equal(new string?[] { "#" , "#" , "#" , "#" , "#" , "#" , "#" , "#" , "#" , "#"  }, strings[11]);
    }

    [Fact]
    public void CreateSequenceTest()
    {
        var startGrid = _solutionService.ParseInput(_input);
        var frames = _solutionService.CreateSequence(startGrid);

        var strings = _solutionService.CreatePrintableOutput(frames[0]);

        // do not format this
        Assert.Equal(new string?[] { null, null, null, null, null, null, "+" , null, null, null }, strings[0]);
        Assert.Equal(new string?[] { null, null, null, null, null, null, null, null, null, null }, strings[1]);
        Assert.Equal(new string?[] { null, null, null, null, null, null, null, null, null, null }, strings[2]);
        Assert.Equal(new string?[] { null, null, null, null, null, null, null, null, null, null }, strings[3]);
        Assert.Equal(new string?[] { null, null, null, null, "#" , null, null, null, "#" , "#"  }, strings[4]);
        Assert.Equal(new string?[] { null, null, null, null, "#" , null, null, null, "#" , null }, strings[5]);
        Assert.Equal(new string?[] { null, null, "#" , "#" , "#" , null, null, null, "#" , null }, strings[6]);
        Assert.Equal(new string?[] { null, null, null, null, null, null, null, null, "#" , null }, strings[7]);
        Assert.Equal(new string?[] { null, null, null, null, null, null, null, null, "#" , null }, strings[8]);
        Assert.Equal(new string?[] { "#" , "#" , "#" , "#" , "#" , "#" , "#" , "#" , "#" , null }, strings[9]);

        strings = _solutionService.CreatePrintableOutput(frames[1]);

        // do not format this
        Assert.Equal(new string?[] { null, null, null, null, null, null, "+" , null, null, null }, strings[0]);
        Assert.Equal(new string?[] { null, null, null, null, null, null, "o" , null, null, null }, strings[1]);
        Assert.Equal(new string?[] { null, null, null, null, null, null, null, null, null, null }, strings[2]);
        Assert.Equal(new string?[] { null, null, null, null, null, null, null, null, null, null }, strings[3]);
        Assert.Equal(new string?[] { null, null, null, null, "#" , null, null, null, "#" , "#"  }, strings[4]);
        Assert.Equal(new string?[] { null, null, null, null, "#" , null, null, null, "#" , null }, strings[5]);
        Assert.Equal(new string?[] { null, null, "#" , "#" , "#" , null, null, null, "#" , null }, strings[6]);
        Assert.Equal(new string?[] { null, null, null, null, null, null, null, null, "#" , null }, strings[7]);
        Assert.Equal(new string?[] { null, null, null, null, null, null, null, null, "#" , null }, strings[8]);
        Assert.Equal(new string?[] { "#" , "#" , "#" , "#" , "#" , "#" , "#" , "#" , "#" , null }, strings[9]);

        strings = _solutionService.CreatePrintableOutput(frames[2]);

        // do not format this
        Assert.Equal(new string?[] { null, null, null, null, null, null, "+" , null, null, null }, strings[0]);
        Assert.Equal(new string?[] { null, null, null, null, null, null, null, null, null, null }, strings[1]);
        Assert.Equal(new string?[] { null, null, null, null, null, null, "o" , null, null, null }, strings[2]);
        Assert.Equal(new string?[] { null, null, null, null, null, null, null, null, null, null }, strings[3]);
        Assert.Equal(new string?[] { null, null, null, null, "#" , null, null, null, "#" , "#"  }, strings[4]);
        Assert.Equal(new string?[] { null, null, null, null, "#" , null, null, null, "#" , null }, strings[5]);
        Assert.Equal(new string?[] { null, null, "#" , "#" , "#" , null, null, null, "#" , null }, strings[6]);
        Assert.Equal(new string?[] { null, null, null, null, null, null, null, null, "#" , null }, strings[7]);
        Assert.Equal(new string?[] { null, null, null, null, null, null, null, null, "#" , null }, strings[8]);
        Assert.Equal(new string?[] { "#" , "#" , "#" , "#" , "#" , "#" , "#" , "#" , "#" , null }, strings[9]);

        strings = _solutionService.CreatePrintableOutput(frames[3]);

        // do not format this
        Assert.Equal(new string?[] { null, null, null, null, null, null, "+" , null, null, null }, strings[0]);
        Assert.Equal(new string?[] { null, null, null, null, null, null, null, null, null, null }, strings[1]);
        Assert.Equal(new string?[] { null, null, null, null, null, null, null, null, null, null }, strings[2]);
        Assert.Equal(new string?[] { null, null, null, null, null, null, "o" , null, null, null }, strings[3]);
        Assert.Equal(new string?[] { null, null, null, null, "#" , null, null, null, "#" , "#"  }, strings[4]);
        Assert.Equal(new string?[] { null, null, null, null, "#" , null, null, null, "#" , null }, strings[5]);
        Assert.Equal(new string?[] { null, null, "#" , "#" , "#" , null, null, null, "#" , null }, strings[6]);
        Assert.Equal(new string?[] { null, null, null, null, null, null, null, null, "#" , null }, strings[7]);
        Assert.Equal(new string?[] { null, null, null, null, null, null, null, null, "#" , null }, strings[8]);
        Assert.Equal(new string?[] { "#" , "#" , "#" , "#" , "#" , "#" , "#" , "#" , "#" , null }, strings[9]);

        strings = _solutionService.CreatePrintableOutput(frames[8]);

        // do not format this
        Assert.Equal(new string?[] { null, null, null, null, null, null, "+" , null, null, null }, strings[0]);
        Assert.Equal(new string?[] { null, null, null, null, null, null, null, null, null, null }, strings[1]);
        Assert.Equal(new string?[] { null, null, null, null, null, null, null, null, null, null }, strings[2]);
        Assert.Equal(new string?[] { null, null, null, null, null, null, null, null, null, null }, strings[3]);
        Assert.Equal(new string?[] { null, null, null, null, "#" , null, null, null, "#" , "#"  }, strings[4]);
        Assert.Equal(new string?[] { null, null, null, null, "#" , null, null, null, "#" , null }, strings[5]);
        Assert.Equal(new string?[] { null, null, "#" , "#" , "#" , null, null, null, "#" , null }, strings[6]);
        Assert.Equal(new string?[] { null, null, null, null, null, null, null, null, "#" , null }, strings[7]);
        Assert.Equal(new string?[] { null, null, null, null, null, null, "o" , null, "#" , null }, strings[8]);
        Assert.Equal(new string?[] { "#" , "#" , "#" , "#" , "#" , "#" , "#" , "#" , "#" , null }, strings[9]);
    }

    [Fact]
    public void CreateSequenceTest2()
    {
        var startGrid = _solutionService.ParseInput(_input);
        var frames = _solutionService.CreateSequence(startGrid);

        Assert.Equal(0, frames[0].SandCount);

        var strings = _solutionService.CreatePrintableOutput(frames[17]);

        // do not format this
        Assert.Equal(new string?[] { null, null, null, null, null, null, "+" , null, null, null }, strings[0]);
        Assert.Equal(new string?[] { null, null, null, null, null, null, null, null, null, null }, strings[1]);
        Assert.Equal(new string?[] { null, null, null, null, null, null, null, null, null, null }, strings[2]);
        Assert.Equal(new string?[] { null, null, null, null, null, null, null, null, null, null }, strings[3]);
        Assert.Equal(new string?[] { null, null, null, null, "#" , null, null, null, "#" , "#"  }, strings[4]);
        Assert.Equal(new string?[] { null, null, null, null, "#" , null, null, null, "#" , null }, strings[5]);
        Assert.Equal(new string?[] { null, null, "#" , "#" , "#" , null, null, null, "#" , null }, strings[6]);
        Assert.Equal(new string?[] { null, null, null, null, null, null, null, null, "#" , null }, strings[7]);
        Assert.Equal(new string?[] { null, null, null, null, null, "o" , "o" , null, "#" , null }, strings[8]);
        Assert.Equal(new string?[] { "#" , "#" , "#" , "#" , "#" , "#" , "#" , "#" , "#" , null }, strings[9]);

        Assert.Equal(2, frames[26].SandCount);

        strings = _solutionService.CreatePrintableOutput(frames[26]);

        // do not format this
        Assert.Equal(new string?[] { null, null, null, null, null, null, "+" , null, null, null }, strings[0]);
        Assert.Equal(new string?[] { null, null, null, null, null, null, null , null, null, null }, strings[1]);
        Assert.Equal(new string?[] { null, null, null, null, null, null, null, null, null, null }, strings[2]);
        Assert.Equal(new string?[] { null, null, null, null, null, null, null, null, null, null }, strings[3]);
        Assert.Equal(new string?[] { null, null, null, null, "#" , null, null, null, "#" , "#"  }, strings[4]);
        Assert.Equal(new string?[] { null, null, null, null, "#" , null, null, null, "#" , null }, strings[5]);
        Assert.Equal(new string?[] { null, null, "#" , "#" , "#" , null, null, null, "#" , null }, strings[6]);
        Assert.Equal(new string?[] { null, null, null, null, null, null, null, null, "#" , null }, strings[7]);
        Assert.Equal(new string?[] { null, null, null, null, null, "o" , "o" , "o" , "#" , null }, strings[8]);
        Assert.Equal(new string?[] { "#" , "#" , "#" , "#" , "#" , "#" , "#" , "#" , "#" , null }, strings[9]);

        Assert.Equal(3, frames[34].SandCount);

        strings = _solutionService.CreatePrintableOutput(frames[34]);

        // do not format this
        Assert.Equal(new string?[] { null, null, null, null, null, null, "+" , null, null, null }, strings[0]);
        Assert.Equal(new string?[] { null, null, null, null, null, null, null, null, null, null }, strings[1]);
        Assert.Equal(new string?[] { null, null, null, null, null, null, null , null, null, null }, strings[2]);
        Assert.Equal(new string?[] { null, null, null, null, null, null, null, null, null, null }, strings[3]);
        Assert.Equal(new string?[] { null, null, null, null, "#" , null, null, null, "#" , "#"  }, strings[4]);
        Assert.Equal(new string?[] { null, null, null, null, "#" , null, null, null, "#" , null }, strings[5]);
        Assert.Equal(new string?[] { null, null, "#" , "#" , "#" , null, null, null, "#" , null }, strings[6]);
        Assert.Equal(new string?[] { null, null, null, null, null, null, "o" , null, "#" , null }, strings[7]);
        Assert.Equal(new string?[] { null, null, null, null, null, "o" , "o" , "o" , "#" , null }, strings[8]);
        Assert.Equal(new string?[] { "#" , "#" , "#" , "#" , "#" , "#" , "#" , "#" , "#" , null }, strings[9]);

        strings = _solutionService.CreatePrintableOutput(frames[43]);

        // do not format this
        Assert.Equal(new string?[] { null, null, null, null, null, null, "+" , null, null, null }, strings[0]);
        Assert.Equal(new string?[] { null, null, null, null, null, null, null, null, null, null }, strings[1]);
        Assert.Equal(new string?[] { null, null, null, null, null, null, null , null, null, null }, strings[2]);
        Assert.Equal(new string?[] { null, null, null, null, null, null, null, null, null, null }, strings[3]);
        Assert.Equal(new string?[] { null, null, null, null, "#" , null, null, null, "#" , "#"  }, strings[4]);
        Assert.Equal(new string?[] { null, null, null, null, "#" , null, null, null, "#" , null }, strings[5]);
        Assert.Equal(new string?[] { null, null, "#" , "#" , "#" , null, null, null, "#" , null }, strings[6]);
        Assert.Equal(new string?[] { null, null, null, null, null, null, "o" , null, "#" , null }, strings[7]);
        Assert.Equal(new string?[] { null, null, null, null, "o" , "o" , "o" , "o" , "#" , null }, strings[8]);
        Assert.Equal(new string?[] { "#" , "#" , "#" , "#" , "#" , "#" , "#" , "#" , "#" , null }, strings[9]);

        strings = _solutionService.CreatePrintableOutput(frames[8]);

        // do not format this
        Assert.Equal(new string?[] { null, null, null, null, null, null, "+" , null, null, null }, strings[0]);
        Assert.Equal(new string?[] { null, null, null, null, null, null, null, null, null, null }, strings[1]);
        Assert.Equal(new string?[] { null, null, null, null, null, null, null, null, null, null }, strings[2]);
        Assert.Equal(new string?[] { null, null, null, null, null, null, null, null, null, null }, strings[3]);
        Assert.Equal(new string?[] { null, null, null, null, "#" , null, null, null, "#" , "#"  }, strings[4]);
        Assert.Equal(new string?[] { null, null, null, null, "#" , null, null, null, "#" , null }, strings[5]);
        Assert.Equal(new string?[] { null, null, "#" , "#" , "#" , null, null, null, "#" , null }, strings[6]);
        Assert.Equal(new string?[] { null, null, null, null, null, null, null, null, "#" , null }, strings[7]);
        Assert.Equal(new string?[] { null, null, null, null, null, null, "o" , null, "#" , null }, strings[8]);
        Assert.Equal(new string?[] { "#" , "#" , "#" , "#" , "#" , "#" , "#" , "#" , "#" , null }, strings[9]);
    }

    [Fact]
    public void CreateSequence_DoNotShowAllSteps_Test()
    {
        var startGrid = _solutionService.ParseInput(_input);
        var frames = _solutionService.CreateSequence(startGrid, false);

        var strings = _solutionService.CreatePrintableOutput(frames[0]);

        // do not format this
        Assert.Equal(new string?[] { null, null, null, null, null, null, "+" , null, null, null }, strings[0]);
        Assert.Equal(new string?[] { null, null, null, null, null, null, null, null, null, null }, strings[1]);
        Assert.Equal(new string?[] { null, null, null, null, null, null, null, null, null, null }, strings[2]);
        Assert.Equal(new string?[] { null, null, null, null, null, null, null, null, null, null }, strings[3]);
        Assert.Equal(new string?[] { null, null, null, null, "#" , null, null, null, "#" , "#"  }, strings[4]);
        Assert.Equal(new string?[] { null, null, null, null, "#" , null, null, null, "#" , null }, strings[5]);
        Assert.Equal(new string?[] { null, null, "#" , "#" , "#" , null, null, null, "#" , null }, strings[6]);
        Assert.Equal(new string?[] { null, null, null, null, null, null, null, null, "#" , null }, strings[7]);
        Assert.Equal(new string?[] { null, null, null, null, null, null, null, null, "#" , null }, strings[8]);
        Assert.Equal(new string?[] { "#" , "#" , "#" , "#" , "#" , "#" , "#" , "#" , "#" , null }, strings[9]);

        strings = _solutionService.CreatePrintableOutput(frames[1]);

        // do not format this
        Assert.Equal(new string?[] { null, null, null, null, null, null, "+" , null, null, null }, strings[0]);
        Assert.Equal(new string?[] { null, null, null, null, null, null, null , null, null, null }, strings[1]);
        Assert.Equal(new string?[] { null, null, null, null, null, null, null, null, null, null }, strings[2]);
        Assert.Equal(new string?[] { null, null, null, null, null, null, null, null, null, null }, strings[3]);
        Assert.Equal(new string?[] { null, null, null, null, "#" , null, null, null, "#" , "#"  }, strings[4]);
        Assert.Equal(new string?[] { null, null, null, null, "#" , null, null, null, "#" , null }, strings[5]);
        Assert.Equal(new string?[] { null, null, "#" , "#" , "#" , null, null, null, "#" , null }, strings[6]);
        Assert.Equal(new string?[] { null, null, null, null, null, null, null, null, "#" , null }, strings[7]);
        Assert.Equal(new string?[] { null, null, null, null, null, null, "o" , null, "#" , null }, strings[8]);
        Assert.Equal(new string?[] { "#" , "#" , "#" , "#" , "#" , "#" , "#" , "#" , "#" , null }, strings[9]);

        strings = _solutionService.CreatePrintableOutput(frames[2]);

        // do not format this
        Assert.Equal(new string?[] { null, null, null, null, null, null, "+" , null, null, null }, strings[0]);
        Assert.Equal(new string?[] { null, null, null, null, null, null, null, null, null, null }, strings[1]);
        Assert.Equal(new string?[] { null, null, null, null, null, null, null, null, null, null }, strings[2]);
        Assert.Equal(new string?[] { null, null, null, null, null, null, null, null, null, null }, strings[3]);
        Assert.Equal(new string?[] { null, null, null, null, "#" , null, null, null, "#" , "#"  }, strings[4]);
        Assert.Equal(new string?[] { null, null, null, null, "#" , null, null, null, "#" , null }, strings[5]);
        Assert.Equal(new string?[] { null, null, "#" , "#" , "#" , null, null, null, "#" , null }, strings[6]);
        Assert.Equal(new string?[] { null, null, null, null, null, null, null, null, "#" , null }, strings[7]);
        Assert.Equal(new string?[] { null, null, null, null, null, "o" , "o" , null, "#" , null }, strings[8]);
        Assert.Equal(new string?[] { "#" , "#" , "#" , "#" , "#" , "#" , "#" , "#" , "#" , null }, strings[9]);

        strings = _solutionService.CreatePrintableOutput(frames[3]);

        // do not format this
        Assert.Equal(new string?[] { null, null, null, null, null, null, "+" , null, null, null }, strings[0]);
        Assert.Equal(new string?[] { null, null, null, null, null, null, null, null, null, null }, strings[1]);
        Assert.Equal(new string?[] { null, null, null, null, null, null, null, null, null, null }, strings[2]);
        Assert.Equal(new string?[] { null, null, null, null, null, null, null, null, null, null }, strings[3]);
        Assert.Equal(new string?[] { null, null, null, null, "#" , null, null, null, "#" , "#"  }, strings[4]);
        Assert.Equal(new string?[] { null, null, null, null, "#" , null, null, null, "#" , null }, strings[5]);
        Assert.Equal(new string?[] { null, null, "#" , "#" , "#" , null, null, null, "#" , null }, strings[6]);
        Assert.Equal(new string?[] { null, null, null, null, null, null, null, null, "#" , null }, strings[7]);
        Assert.Equal(new string?[] { null, null, null, null, null, "o" , "o" , "o" , "#" , null }, strings[8]);
        Assert.Equal(new string?[] { "#" , "#" , "#" , "#" , "#" , "#" , "#" , "#" , "#" , null }, strings[9]);

        strings = _solutionService.CreatePrintableOutput(frames[4]);

        // do not format this
        Assert.Equal(new string?[] { null, null, null, null, null, null, "+" , null, null, null }, strings[0]);
        Assert.Equal(new string?[] { null, null, null, null, null, null, null, null, null, null }, strings[1]);
        Assert.Equal(new string?[] { null, null, null, null, null, null, null, null, null, null }, strings[2]);
        Assert.Equal(new string?[] { null, null, null, null, null, null, null, null, null, null }, strings[3]);
        Assert.Equal(new string?[] { null, null, null, null, "#" , null, null, null, "#" , "#"  }, strings[4]);
        Assert.Equal(new string?[] { null, null, null, null, "#" , null, null, null, "#" , null }, strings[5]);
        Assert.Equal(new string?[] { null, null, "#" , "#" , "#" , null, null, null, "#" , null }, strings[6]);
        Assert.Equal(new string?[] { null, null, null, null, null, null, "o" , null, "#" , null }, strings[7]);
        Assert.Equal(new string?[] { null, null, null, null, null, "o" , "o" , "o" , "#" , null }, strings[8]);
        Assert.Equal(new string?[] { "#" , "#" , "#" , "#" , "#" , "#" , "#" , "#" , "#" , null }, strings[9]);
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
        Assert.Equal(93, result);
    }
}