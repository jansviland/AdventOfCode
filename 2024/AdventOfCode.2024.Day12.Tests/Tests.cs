using AdventOfCode._2024.Day12;

namespace AdventOfCodeAdventOfCode._2024.Day12.Tests;

public class Tests : TestBed<TestFixture>
{
    private readonly ISolutionService _solutionService;

    private readonly string[] _input;
    private readonly string[] _inputLarge;

    public Tests(ITestOutputHelper testOutputHelper, TestFixture fixture) : base(testOutputHelper, fixture)
    {
        _solutionService = _fixture.GetService<ISolutionService>(_testOutputHelper)!;
        _input = File.ReadAllLines("Assets/test-input.txt");
        _inputLarge = File.ReadAllLines("Assets/test-input-large.txt");
    }

    [Fact]
    public void Part1Test()
    {
        // arrange
        _testOutputHelper.WriteLine($"Running unit test - Part 1");

        // act
        var result = _solutionService.RunPart1(_input);

        // assert
        Assert.Equal(140, result);
    }
    
    [Fact]
    public void Part1Test_Large()
    {
        // arrange
        _testOutputHelper.WriteLine($"Running unit test - Part 1 - Large");

        // act
        var result = _solutionService.RunPart1(_inputLarge);

        // assert
        Assert.Equal(1930, result);
    }

    [Fact]
    public void Part2Test()
    {
        // arrange
        _testOutputHelper.WriteLine($"Running unit test - Part 2");

        // act
        var result = _solutionService.RunPart2(_input);

        // assert
        Assert.Equal(31, result);
    }
}