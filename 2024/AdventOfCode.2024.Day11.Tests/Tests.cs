using AdventOfCode._2024.Day11;

namespace AdventOfCodeAdventOfCode._2024.Day11.Tests;

public class Tests : TestBed<TestFixture>
{
    private readonly ISolutionService _solutionService;
    private readonly Helper _helper = new();

    private readonly string[] _input;

    public Tests(ITestOutputHelper testOutputHelper, TestFixture fixture) : base(testOutputHelper, fixture)
    {
        _solutionService = _fixture.GetService<ISolutionService>(_testOutputHelper)!;
        _input = File.ReadAllLines("Assets/test-input.txt");
    }

    [Fact]
    public void Blink_1Blink_Test()
    {
        var stones = new List<uint> { 125, 17 };
        var expected = new List<uint> { 253000, 1, 7 };

        Assert.Equal(expected, _solutionService.Blink(stones, 1));
    }

    [Fact]
    public void Blink_4Blinks_Test()
    {
        var stones = new List<uint> { 125, 17 };
        var expected = new List<uint> { 512, 72, 2024, 2, 0, 2, 4, 2867, 6032 };

        Assert.Equal(expected, _solutionService.Blink(stones, 4));
    }

    [Fact]
    public void Part1Test()
    {
        // arrange
        _testOutputHelper.WriteLine($"Running unit test - {_helper.GetYear()} - Day {_helper.GetDay()} - Part 1");

        // act
        var result = _solutionService.RunPart1(_input);

        // assert
        Assert.Equal(55312, result);
    }

    [Fact]
    public void Part2Test()
    {
        // arrange
        _testOutputHelper.WriteLine($"Running unit test - {_helper.GetYear()} - Day {_helper.GetDay()} - Part 2");

        // act
        var result = _solutionService.RunPart2(_input);

        // assert
        Assert.Equal(31, result);
    }
}
