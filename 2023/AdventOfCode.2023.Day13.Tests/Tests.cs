namespace AdventOfCode._2023.Day13.Tests;

public class Tests : TestBed<TestFixture>
{
    private readonly ISolutionService _solutionService;

    private readonly string[] _input;
    private readonly string[] _inputFirst;
    private readonly string[] _inputSecond;

    public Tests(ITestOutputHelper testOutputHelper, TestFixture fixture) : base(testOutputHelper, fixture)
    {
        _solutionService = _fixture.GetService<ISolutionService>(_testOutputHelper)!;
        _input = File.ReadAllLines("Assets/test-input.txt");
        _inputFirst = File.ReadAllLines("Assets/test-input-first.txt");
        _inputSecond = File.ReadAllLines("Assets/test-input-second.txt");
    }

    [Fact]
    public void Part1Test()
    {
        // arrange
        _testOutputHelper.WriteLine("Running unit test - 2023 - Day 13 - Part 1");

        // act
        var result = _solutionService.RunPart1(_input);

        // assert
        Assert.Equal(405, result);
    }

    [Fact]
    public void Part1OnlyFirstTest()
    {
        // arrange
        _testOutputHelper.WriteLine("Running unit test - 2023 - Day 13 - Part 1");

        // act
        var result = _solutionService.RunPart1(_inputFirst);

        // assert
        Assert.Equal(5, result);
    }


    [Fact]
    public void Part1OnlySecondTest()
    {
        // arrange
        _testOutputHelper.WriteLine("Running unit test - 2023 - Day 13 - Part 1");

        // act
        var result = _solutionService.RunPart1(_inputSecond);

        // assert
        Assert.Equal(400, result);
    }

    [Fact]
    public void Part2Test()
    {
        // arrange
        _testOutputHelper.WriteLine("Running unit test - 2023 - Day 13 - Part 2");

        // act
        var result = _solutionService.RunPart2(_input);

        // assert
        Assert.Equal(5905, result);
    }
}