using System.Collections.Concurrent;
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
    
    /// <summary>
    /// The first stone, 0, becomes a stone marked 1.
    /// The second stone, 1, is multiplied by 2024 to become 2024.
    /// The third stone, 10, is split into a stone marked 1 followed by a stone marked 0.
    /// The fourth stone, 99, is split into two stones marked 9.
    /// The fifth stone, 999, is replaced by a stone marked 2021976.
    /// </summary>
    /// <param name="n">stones engraved number</param>
    /// <param name="expected">Number of stones produced</param>
    [Theory]
    [InlineData(0, 1)]
    [InlineData(1, 1)]
    [InlineData(10, 2)]
    [InlineData(99, 2)]
    [InlineData(999, 1)]
    public void Blink_Test(long n, long expected)
    {
        // we use a concurrent dictionary type that is thread safe
        var cache = new ConcurrentDictionary<(long, int), long>();

        Assert.Equal(expected, _solutionService.Blink(n, 1, cache));
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

    // [Fact]
    // public void Part2Test()
    // {
    //     // arrange
    //     _testOutputHelper.WriteLine($"Running unit test - {_helper.GetYear()} - Day {_helper.GetDay()} - Part 2");
    //
    //     // act
    //     var result = _solutionService.RunPart2(_input);
    //
    //     // assert
    //     Assert.Equal(55312, result);
    // }
}
