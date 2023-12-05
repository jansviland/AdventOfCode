namespace AdventOfCode._2023.Day5.Tests;

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
        _testOutputHelper.WriteLine("Running unit test - Part 1");

        // act
        long result = _solutionService.RunPart1(_input);

        // assert
        Assert.Equal(35, result);
    }

    [Fact(Skip = "Not implemented yet")]
    public void Part2Test()
    {
        // arrange
        _testOutputHelper.WriteLine("Running unit test - Part 2");

        // act
        int result = _solutionService.RunPart2(_input);

        // assert
        Assert.Equal(30, result);
    }
}