using AdventOfCode._2024.Day15;

namespace AdventOfCodeAdventOfCode._2024.Day15.Tests;

public class Tests : TestBed<TestFixture>
{
    private readonly ISolutionService _solutionService;
    private readonly Helper _helper = new();

    private readonly string[] _input1;
    private readonly string[] _input2;

    public Tests(ITestOutputHelper testOutputHelper, TestFixture fixture) : base(testOutputHelper, fixture)
    {
        _solutionService = _fixture.GetService<ISolutionService>(_testOutputHelper)!;
        _input1 = File.ReadAllLines("Assets/test-input.txt");
        _input2 = File.ReadAllLines("Assets/test-input2.txt");
    }

    [Fact]
    public void Part1Test_SmallInput()
    {
        // arrange
        _testOutputHelper.WriteLine($"Running unit test - {_helper.GetYear()} - Day {_helper.GetDay()} - Part 1");

        // act
        var result = _solutionService.RunPart1(_input2);

        // assert
        Assert.Equal(2028, result);
    }
    
    [Fact]
    public void Part1Test_LargeInput()
    {
        // arrange
        _testOutputHelper.WriteLine($"Running unit test - {_helper.GetYear()} - Day {_helper.GetDay()} - Part 1");

        // act
        var result = _solutionService.RunPart1(_input1);

        // assert
        Assert.Equal(10092, result);
    }

    [Fact]
    public void Part2Test()
    {
        // arrange
        _testOutputHelper.WriteLine($"Running unit test - {_helper.GetYear()} - Day {_helper.GetDay()} - Part 2");

        // act
        var result = _solutionService.RunPart2(_input1);

        // assert
        Assert.Equal(31, result);
    }
}