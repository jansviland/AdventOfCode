namespace AdventOfCode._2022.Day3.Tests;

public class Tests : TestBed<TestFixture>
{
    private readonly ISolutionService _solutionService;

    private readonly string[] _input = new[]
    {
        "vJrwpWtwJgWrhcsFMMfFFhFp",
        "jqHRNqRjqzjGDLGLrsFMfFZSrLrFZsSL",
        "PmmdzqPrVvPwwTWBwg",
        "wMqvLMZHhHMvwLHjbvcjnnSBnvTQFn",
        "ttgJtRGJQctTZtZT",
        "CrZsJsPPZsGzwwsLwLmpwMDw"
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
        var result = _solutionService!.RunPart1(_input);

        // assert
        Assert.Equal(157, result);
    }

    [Fact]
    public void ParseStringPart1Test()
    {
        // arrange
        // act
        // assert
        Assert.Equal('p', _solutionService.ParseStringPart1(_input[0]));
        Assert.Equal('L', _solutionService.ParseStringPart1(_input[1]));
        Assert.Equal('P', _solutionService.ParseStringPart1(_input[2]));
        Assert.Equal('v', _solutionService.ParseStringPart1(_input[3]));
        Assert.Equal('t', _solutionService.ParseStringPart1(_input[4]));
        Assert.Equal('s', _solutionService.ParseStringPart1(_input[5]));
    }

    [Fact]
    public void Part2Test()
    {
        _testOutputHelper.WriteLine("Running unit test - Part 2");

        // arrange
        // act
        var result = _solutionService!.RunPart2(_input);

        // assert
        Assert.Equal(45000, result);
    }
}