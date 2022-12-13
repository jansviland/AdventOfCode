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

        // stacks contains the correct crates
        // stack 1
        Assert.NotNull(result[0].FirstOrDefault(x => x.Name == "N"));
        Assert.NotNull(result[0].FirstOrDefault(x => x.Name == "Z"));

        // stack 2
        Assert.NotNull(result[1].FirstOrDefault(x => x.Name == "D"));
        Assert.NotNull(result[1].FirstOrDefault(x => x.Name == "C"));
        Assert.NotNull(result[1].FirstOrDefault(x => x.Name == "M"));

        // stack 3
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
    public void MoveCratesOneAtATimeTest1()
    {
        // arrange
        var originalStack = _solutionService.ParseInput(_input);

        // act
        var stack = _solutionService.MoveCratesOneAtATime(originalStack, _input[5]); // "move 1 from 2 to 1"

        // assert
        // number of crates in each stack
        Assert.Equal(3, stack[0].Count);
        Assert.Equal(2, stack[1].Count);
        Assert.Single(stack[2]);

        // correct crate at the top of each stack
        Assert.Equal("D", stack[0].Peek().Name);
        Assert.Equal("C", stack[1].Peek().Name);
        Assert.Equal("P", stack[2].Peek().Name);

        // stacks contains the correct crates
        // stack 1
        Assert.NotNull(stack[0].FirstOrDefault(x => x.Name == "D"));
        Assert.NotNull(stack[0].FirstOrDefault(x => x.Name == "N"));
        Assert.NotNull(stack[0].FirstOrDefault(x => x.Name == "Z"));

        // stack 2
        Assert.NotNull(stack[1].FirstOrDefault(x => x.Name == "C"));
        Assert.NotNull(stack[1].FirstOrDefault(x => x.Name == "M"));

        // stack 3
        Assert.NotNull(stack[2].FirstOrDefault(x => x.Name == "P"));
    }

    [Fact]
    public void MoveCratesOneAtATimeTest2()
    {
        // arrange
        var originalStack = _solutionService.ParseInput(_input);

        // act
        var stack = _solutionService.MoveCratesOneAtATime(originalStack, _input[5]); // "move 1 from 2 to 1"
        _solutionService.MoveCratesOneAtATime(stack, _input[6]); // "move 3 from 1 to 3"

        // assert
        // number of crates in each stack
        Assert.Empty(stack[0]);
        Assert.Equal(2, stack[1].Count);
        Assert.Equal(4, stack[2].Count);

        // correct crate at the top of each stack
        Assert.Equal("C", stack[1].Peek().Name);
        Assert.Equal("Z", stack[2].Peek().Name);

        // stacks contains the correct crates
        // stack 2
        Assert.NotNull(stack[1].FirstOrDefault(x => x.Name == "C"));
        Assert.NotNull(stack[1].FirstOrDefault(x => x.Name == "M"));

        // stack 3
        Assert.NotNull(stack[2].FirstOrDefault(x => x.Name == "Z"));
        Assert.NotNull(stack[2].FirstOrDefault(x => x.Name == "N"));
        Assert.NotNull(stack[2].FirstOrDefault(x => x.Name == "D"));
        Assert.NotNull(stack[2].FirstOrDefault(x => x.Name == "P"));
    }

    [Fact]
    public void MoveCratesMultipleAtATimeTest1()
    {
        // arrange
        var originalStack = _solutionService.ParseInput(_input);

        // act
        var stack = _solutionService.MoveCratesMultipleAtATime(originalStack, _input[5]); // "move 1 from 2 to 1"
        _solutionService.MoveCratesMultipleAtATime(stack, _input[6]); // "move 3 from 1 to 3"

        // assert
        // correct crate at the top of each stack
        Assert.Equal("C", stack[1].Peek().Name);
        Assert.Equal("D", stack[2].Peek().Name);
    }

    [Fact]
    public void MoveCratesMultipleAtATimeTest2()
    {
        // arrange
        var originalStack = _solutionService.ParseInput(_input);

        // act
        var stack = _solutionService.MoveCratesMultipleAtATime(originalStack, _input[5]); // "move 1 from 2 to 1"
        _solutionService.MoveCratesMultipleAtATime(stack, _input[6]); // "move 3 from 1 to 3"
        _solutionService.MoveCratesMultipleAtATime(stack, _input[7]); // "move 2 from 2 to 1"

        // assert
        // correct crate at the top of each stack
        Assert.Equal("C", stack[0].Peek().Name);
        Assert.Equal("D", stack[2].Peek().Name);
    }

    [Fact]
    public void MoveCratesMultipleAtATimeTest3()
    {
        // arrange
        var originalStack = _solutionService.ParseInput(_input);

        // act
        var stack = _solutionService.MoveCratesMultipleAtATime(originalStack, _input[5]); // "move 1 from 2 to 1"
        _solutionService.MoveCratesMultipleAtATime(stack, _input[6]); // "move 3 from 1 to 3"
        _solutionService.MoveCratesMultipleAtATime(stack, _input[7]); // "move 2 from 2 to 1"
        _solutionService.MoveCratesMultipleAtATime(stack, _input[8]); // "move 1 from 1 to 2"

        // assert
        // correct crate at the top of each stack
        Assert.Equal("M", stack[0].Peek().Name);
        Assert.Equal("C", stack[1].Peek().Name);
        Assert.Equal("D", stack[2].Peek().Name);
    }

    [Fact]
    public void Part2Test()
    {
        _testOutputHelper.WriteLine("Running unit test - Part 2");

        // arrange
        // act
        var result = _solutionService.RunPart2(_input);

        // assert
        Assert.Equal("MCD", result);
    }
}