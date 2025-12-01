using AdventOfCode._2025.Day01;

namespace AdventOfCodeAdventOfCode._2025.Day01.Tests;

public class Tests : TestBed<TestFixture>
{
    private readonly ISolutionService _solutionService;
    private readonly Helper _helper = new();

    private readonly string[] _inputTest;
    private readonly string[] _input;

    public Tests(ITestOutputHelper testOutputHelper, TestFixture fixture) : base(testOutputHelper, fixture)
    {
        _solutionService = _fixture.GetService<ISolutionService>(_testOutputHelper)!;
        _inputTest = File.ReadAllLines("Assets/test-input.txt");
        _input = File.ReadAllLines("Assets/test.txt");
    }

    [Fact]
    public void Part1Test()
    {
        // arrange
        _testOutputHelper.WriteLine($"Running unit test - {_helper.GetYear()} - Day {_helper.GetDay()} - Part 1");

        // act
        var result = _solutionService.RunPart1(_inputTest);

        // assert
        Assert.Equal(3, result);
    }
    
    [Fact]
    public void Part1Test2()
    {
        // arrange
        _testOutputHelper.WriteLine($"Running unit test - {_helper.GetYear()} - Day {_helper.GetDay()} - Part 1");

        // act
        var result = _solutionService.RunPart1(_input);

        // assert
        Assert.Equal(1011, result);
    }

    [Fact]
    public void Part2Test()
    {
        // arrange
        _testOutputHelper.WriteLine($"Running unit test - {_helper.GetYear()} - Day {_helper.GetDay()} - Part 2");

        // act
        var result = _solutionService.RunPart2(_input);

        // assert
        Assert.Equal(6, result);
    }
}