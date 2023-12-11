namespace AdventOfCode._2023.Day11.Tests;

public class Tests : TestBed<TestFixture>
{
    private readonly ISolutionService _solutionService;

    private readonly string[] _input;
    private readonly string[] _input2;

    public Tests(ITestOutputHelper testOutputHelper, TestFixture fixture) : base(testOutputHelper, fixture)
    {
        _solutionService = _fixture.GetService<ISolutionService>(_testOutputHelper)!;
        _input = File.ReadAllLines("Assets/test-input.txt");
        _input2 = File.ReadAllLines("Assets/test-input2.txt");
    }

    
    [Fact]
    public void Part1ExpandGridTest()
    {
        // arrange
        _testOutputHelper.WriteLine("Running unit test - 2023 - Day 11 - Part 1");

        // act
        string[] result = _solutionService.ExpandGrid(_input);

        // assert
        Assert.Equivalent(_input2, result);
    }

    [Fact]
    public void Part1Test()
    {
        // arrange
        _testOutputHelper.WriteLine("Running unit test - 2023 - Day 11 - Part 1");

        // act
        var result = _solutionService.RunPart1(_input);

        // assert
        Assert.Equal(374, result);
    }

    // [Fact]
    // public void Part2Test()
    // {
    //     // arrange
    //     _testOutputHelper.WriteLine("Running unit test - 2023 - Day 11 - Part 2");
    //
    //     // act
    //     var result = _solutionService.RunPart2(_input);
    //
    //     // assert
    //     Assert.Equal(1030, result);
    // }
}