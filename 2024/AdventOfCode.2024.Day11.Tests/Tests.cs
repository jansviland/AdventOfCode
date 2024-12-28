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

    // After 1 blink:
    // 253000 1 7
    //
    // After 2 blinks:
    // 253 0 2024 14168
    //
    // After 3 blinks:
    // 512072 1 20 24 28676032
    //
    // After 4 blinks:
    // 512 72 2024 2 0 2 4 2867 6032
    //
    // After 5 blinks:
    // 1036288 7 2 20 24 4048 1 4048 8096 28 67 60 32
    //
    // After 6 blinks:
    // 2097446912 14168 4048 2 0 2 4 40 48 2024 40 48 80 96 2 8 6 7 6 0 3 2
    
    [Fact]
    public void Blink_1Blink_Test()
    {
        var stones = new List<uint> { 125, 17 };
        var expected = new List<uint> { 253000, 1, 7 };

        Assert.Equal(expected, _solutionService.Blink(stones, 1));
    }
    
    [Fact]
    public void Blink_2Blinks_Test()
    {
        var stones = new List<uint> { 125, 17 };
        var expected = new List<uint> { 253, 0, 2024, 14168 };

        Assert.Equal(expected, _solutionService.Blink(stones, 2));
    }
    
    [Fact]
    public void Blink_3Blinks_Test()
    {
        var stones = new List<uint> { 125, 17 };
        var expected = new List<uint> { 512072, 1, 20, 24, 28676032 };

        Assert.Equal(expected, _solutionService.Blink(stones, 3));
    }

    [Fact]
    public void Blink_4Blinks_Test()
    {
        var stones = new List<uint> { 125, 17 };
        var expected = new List<uint> { 512, 72, 2024, 2, 0, 2, 4, 2867, 6032 };

        Assert.Equal(expected, _solutionService.Blink(stones, 4));
    }
    
    [Fact]
    public void Blink_5Blinks_Test()
    {
        var stones = new List<uint> { 125, 17 };
        var expected = new List<uint> { 1036288, 7, 2, 20, 24, 4048, 1, 4048, 8096, 28, 67, 60, 32 };

        Assert.Equal(expected, _solutionService.Blink(stones, 5));
    }

    [Fact]
    public void Blink_6Blinks_Test()
    {
        var stones = new List<uint> { 125, 17 };
        var expected = new List<uint> { 2097446912, 14168, 4048, 2, 0, 2, 4, 40, 48, 2024, 40, 48, 80, 96, 2, 8, 6, 7, 6, 0, 3, 2 };

        Assert.Equal(expected, _solutionService.Blink(stones, 6));
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
