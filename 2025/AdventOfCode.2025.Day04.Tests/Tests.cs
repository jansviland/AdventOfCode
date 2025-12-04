using AdventOfCode._2025.Day04;

namespace AdventOfCodeAdventOfCode._2025.Day04.Tests;

public class Tests : TestBed<TestFixture>
{
    private readonly ISolutionService _solutionService;
    private readonly Helper _helper = new();

    private readonly string[] _inputTest;

    public Tests(ITestOutputHelper testOutputHelper, TestFixture fixture) : base(testOutputHelper, fixture)
    {
        _solutionService = _fixture.GetService<ISolutionService>(_testOutputHelper)!;
        _inputTest = File.ReadAllLines("Assets/test-input.txt");
    }

    [Fact]
    public void Part1Test()
    {
        // arrange
        _testOutputHelper.WriteLine($"Running unit test - {_helper.GetYear()} - Day {_helper.GetDay()} - Part 1");

        // act
        var result = _solutionService.RunPart1(_inputTest);

        // assert
        Assert.Equal(13, result);
    }

    // [Theory]
    // [InlineData("819", 2, 89)]
    // [InlineData("1819", 3, 819)]
    // public void HigestValueComboTest(string str, int total, long result)
    // {
    //     Assert.Equal(result, _solutionService.HighestValueCombo(str, total));
    // }

    [Fact]
    public void Part2Test()
    {
        // arrange
        _testOutputHelper.WriteLine($"Running unit test - {_helper.GetYear()} - Day {_helper.GetDay()} - Part 2");

        // act
        var result = _solutionService.RunPart2(_inputTest);

        // assert
        Assert.Equal(3121910778619, result);
    }
}
