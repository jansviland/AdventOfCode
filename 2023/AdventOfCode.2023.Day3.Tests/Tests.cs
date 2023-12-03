namespace AdventOfCode._2023.Day3.Tests;

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
    public void Part1Test()
    {
        // arrange
        _testOutputHelper.WriteLine("Running unit test - Part 1");

        // act
        var result = _solutionService.RunPart1(_input);

        // assert
        Assert.Equal(4361, result);
    }

    [Fact]
    public void Part1TestEdgeCases1()
    {
        // arrange
        var input = new string[]
        {
            "...",
            "...",
            ".*4"
        };

        // act
        var result = _solutionService.RunPart1(input);

        // assert
        Assert.Equal(4, result);
    }

    [Fact]
    public void Part1TestEdgeCases2()
    {
        // arrange
        var input = new string[]
        {
            "...",
            "...",
            "4*."
        };

        // act
        var result = _solutionService.RunPart1(input);

        // assert
        Assert.Equal(4, result);
    }
    [Fact]
    public void Part1TestEdgeCases3()
    {
        // arrange
        var input = new string[]
        {
            "4..",
            "*..",
            "..."
        };

        // act
        var result = _solutionService.RunPart1(input);

        // assert
        Assert.Equal(4, result);
    }

    [Fact]
    public void Part1TestEdgeCases4()
    {
        // arrange
        var input = new string[]
        {
            ".*4",
            "...",
            "..."
        };

        // act
        var result = _solutionService.RunPart1(input);

        // assert
        Assert.Equal(4, result);
    }

    [Fact]
    public void Part1TestEdgeCases5()
    {
        // arrange
        var input = new string[]
        {
            "...",
            ".*.",
            "123"
        };

        // act
        var result = _solutionService.RunPart1(input);

        // assert
        Assert.Equal(123, result);
    }

    [Fact]
    public void Part2Test()
    {
        // arrange
        _testOutputHelper.WriteLine("Running unit test - Part 2");

        // act
        var result = _solutionService.RunPart2(_input);

        // assert
        Assert.Equal(467835, result);
    }
}