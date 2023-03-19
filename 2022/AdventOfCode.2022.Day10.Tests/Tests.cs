namespace AdventOfCode._2022.Day10.Tests;

public class Tests : TestBed<TestFixture>
{
    private readonly ISolutionService _solutionService;

    private readonly string[] _input;


    public Tests(ITestOutputHelper testOutputHelper, TestFixture fixture) : base(testOutputHelper, fixture)
    {
        _solutionService = _fixture.GetService<ISolutionService>(_testOutputHelper)!;
        _input = File.ReadAllLines("Assets/test-input.txt");
    }

    [Fact]
    public void ShouldGetTestInput()
    {
        Assert.Equal(146, _input.Length);
    }

    [Fact]
    public void GetCycleResult1()
    {
        // arrange
        string[] input = new[]
        {
            "noop",
            "addx 3",
            "addx -5",
        };

        // act
        var result = _solutionService.GetRegisterXValuePerCycle(input);

        // assert
        Assert.Equal(1, result[0]); // start with value 1
        Assert.Equal(1, result[1]); // During the first cycle, X is 1.
        Assert.Equal(1, result[2]); // the addx 3 instruction begins execution. During the second cycle, X is still 1.
        Assert.Equal(1, result[3]); // After the third cycle, the addx 3 instruction finishes execution, setting X to 4.
        Assert.Equal(4, result[4]); // the addx -5 instruction begins execution. During the fourth cycle, X is still 4.
        Assert.Equal(4, result[5]); // During the fifth cycle, X is still 4.
        Assert.Equal(-1, result[6]); // After the fifth cycle, the addx -5 instruction finishes execution, setting X to -1.
    }

    [Fact]
    public void GetCycleResult2()
    {
        // act
        var result = _solutionService.GetRegisterXValuePerCycle(_input);

        // assert
        Assert.Equal(21, result[20]); // During the 20th cycle, register X has the value 21
        Assert.Equal(19, result[60]); // During the 60th cycle, register X has the value 19, so the signal
        Assert.Equal(18, result[100]);
        Assert.Equal(21, result[140]);
        Assert.Equal(16, result[180]);
        Assert.Equal(18, result[220]);
    }

    [Fact]
    public void Part1Test()
    {
        _testOutputHelper.WriteLine("Running unit test - Part 1");

        // arrange
        // act
        var result = _solutionService.RunPart1(_input);

        // assert
        Assert.Equal(13140, result);
    }

    [Fact]
    public void GetCrtOutputTest()
    {
        // arrange
        // var result = _solutionService.GetRegisterXValuePerCycle(_input);

        // act
        var output = _solutionService.GetCrtOutput(_input);

        // assert
        var expect = new[]
        {
            "##..##..##..##..##..##..##..##..##..##..",
            "###...###...###...###...###...###...###.",
            "####....####....####....####....####....",
            "#####.....#####.....#####.....#####.....",
            "######......######......######......####",
            "#######.......#######.......#######.....",
        };

        Assert.Equal(expect, output);
    }

    // [Fact]
    // public void Part2Test()
    // {
    //     _testOutputHelper.WriteLine("Running unit test - Part 2");
    //
    //     // arrange
    //     // act
    //     var result = _solutionService.RunPart2(_input);
    //
    //     // assert
    //     Assert.Equal(45000, result);
    // }
}