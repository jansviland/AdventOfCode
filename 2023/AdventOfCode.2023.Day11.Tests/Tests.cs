namespace AdventOfCode._2023.Day11.Tests;

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
        _testOutputHelper.WriteLine("Running unit test - 2023 - Day 11 - Part 1");

        // act
        var result = _solutionService.RunPart1(_input);

        // assert
        Assert.Equal(374, result);
    }

    [Fact]
    public void Part2Test()
    {
        // arrange
        _testOutputHelper.WriteLine("Running unit test - 2023 - Day 11 - Part 2");

        // act
        var result = _solutionService.RunPart2(_input);

        // assert
        Assert.Equal(5905, result);
    }
}