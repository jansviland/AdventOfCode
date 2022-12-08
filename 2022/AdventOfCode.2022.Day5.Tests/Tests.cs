namespace AdventOfCode._2022.Day5.Tests;

public class Tests : TestBed<TestFixture>
{
    private readonly ISolutionService _solutionService;
    private readonly string[] _input = new[]
    {
        "    [D]    ",
        "[N] [C]    ",
        "[Z] [M] [P]",
        " 1   2   3 ",
        "",
        "move 1 from 2 to 1",
        "move 3 from 1 to 3",
        "move 2 from 2 to 1",
        "move 1 from 1 to 2",
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
        Assert.Equal("CMZ", result);
    }

    [Fact]
    public void ParseInputTest()
    {
        // arrange
        // act
        var result = _solutionService.ParseInput(_input);

        // assert

        // total stacks
        Assert.Equal(3, result.Count);

        // number of crates in each stack
        Assert.Equal(2, result[0].Count);
        Assert.Equal(3, result[1].Count);
        Assert.Single(result[2]);

        // correct crate at the top of each stack
        Assert.Equal("N", result[0].Peek().Name);
        Assert.Equal("D", result[1].Peek().Name);
        Assert.Equal("P", result[2].Peek().Name);

        // contains the correct crates
        Assert.NotNull(result[0].FirstOrDefault(x => x.Name == "N"));
        Assert.NotNull(result[0].FirstOrDefault(x => x.Name == "Z"));

        Assert.NotNull(result[1].FirstOrDefault(x => x.Name == "D"));
        Assert.NotNull(result[1].FirstOrDefault(x => x.Name == "C"));
        Assert.NotNull(result[1].FirstOrDefault(x => x.Name == "M"));

        Assert.NotNull(result[2].FirstOrDefault(x => x.Name == "P"));
    }

    [Fact]
    public void CreatePrintableOutputTest()
    {
        // arrange
        var stacks = _solutionService.ParseInput(_input);

        // act
        var result = _solutionService.CreatePrintableOutput(stacks);

        // assert
        Assert.Equal(_input[0], result[0]);
        Assert.Equal(_input[1], result[1]);
        Assert.Equal(_input[2], result[2]);
        Assert.Equal(_input[3], result[3]);
    }

    [Fact]
    public void MoveTest1()
    {
        // arrange
        var originalStack = _solutionService.ParseInput(_input);

        // act
        var move1Result = _solutionService.MoveCrate(originalStack, _input[5]);

        // assert
        Assert.Equal("D", move1Result[0].Peek().Name);
        Assert.Equal("C", move1Result[1].Peek().Name);
        Assert.Equal("P", move1Result[2].Peek().Name);
    }

    [Fact]
    public void MoveTest2()
    {
        // arrange
        var originalStack = _solutionService.ParseInput(_input);

        // act
        var move1Result = _solutionService.MoveCrate(originalStack, _input[5]);
        var move2Result = _solutionService.MoveCrate(move1Result, _input[6]);

        // assert
        Assert.Null(move2Result[0].Peek().Name);
        Assert.Equal("C", move2Result[1].Peek().Name);
        Assert.Equal("Z", move2Result[2].Peek().Name);
    }

    [Fact]
    public void Part2Test()
    {
        _testOutputHelper.WriteLine("Running unit test - Part 2");

        // arrange
        // act
        var result = _solutionService.RunPart2(_input);

        // assert
        Assert.Equal("abc", result);
    }
}