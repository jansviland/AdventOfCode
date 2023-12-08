namespace AdventOfCode._2023.Day8.Tests;

public class Tests : TestBed<TestFixture>
{
    private readonly ISolutionService _solutionService;

    private readonly string[] _input;
    private readonly string[] _inputPart2;

    public Tests(ITestOutputHelper testOutputHelper, TestFixture fixture) : base(testOutputHelper, fixture)
    {
        _solutionService = _fixture.GetService<ISolutionService>(_testOutputHelper)!;

        _input = File.ReadAllLines("Assets/test-input.txt");
        _inputPart2 = File.ReadAllLines("Assets/test-input-part2.txt");
    }

    [Fact]
    public void Part1Test()
    {
        // arrange
        _testOutputHelper.WriteLine("Running unit test - 2023 - Day 8 - Part 1");

        // act
        var result = _solutionService.RunPart1(_input);

        // assert
        Assert.Equal(2, result);
    }

    [Fact]
    public void Part2Test()
    {
        // arrange
        _testOutputHelper.WriteLine("Running unit test - 2023 - Day 8 - Part 2");

        // act
        var result = _solutionService.RunPart2(_inputPart2);

        // assert
        Assert.Equal(6, result);
    }
}