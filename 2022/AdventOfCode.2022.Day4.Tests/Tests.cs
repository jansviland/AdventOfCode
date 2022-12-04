namespace AdventOfCode._2022.Day4.Tests;

public class Tests : TestBed<TestFixture>
{
    private readonly ISolutionService _solutionService;

    private readonly string[] _input = new[]
    {
        "2-4,6-8",
        "2-3,4-5",
        "5-7,7-9",
        "2-8,3-7",
        "6-6,4-6",
        "2-6,4-8"
    };

    public Tests(ITestOutputHelper testOutputHelper, TestFixture fixture) : base(testOutputHelper, fixture)
    {
        _solutionService = _fixture.GetService<ISolutionService>(_testOutputHelper)!;
    }

    [Fact]
    public void Part1Test()
    {
        _testOutputHelper.WriteLine("Running unit test - Part 1");

        // arrange
        // act
        var result = _solutionService!.RunPart1(_input);

        // assert
        Assert.Equal(2, result);
    }

    [Fact]
    public void StringOverlapTest()
    {
        // arrange
        // act
        // assert
        Assert.Equal(false, _solutionService!.StringsOverlap(_input[0]));
        Assert.Equal(false, _solutionService!.StringsOverlap(_input[1]));
        Assert.Equal(false, _solutionService!.StringsOverlap(_input[2]));
        Assert.Equal(true, _solutionService!.StringsOverlap(_input[3]));
        Assert.Equal(true, _solutionService!.StringsOverlap(_input[4]));
        Assert.Equal(false, _solutionService!.StringsOverlap(_input[5]));
    }

    [Fact]
    public void Part2Test()
    {
        _testOutputHelper.WriteLine("Running unit test - Part 2");

        // arrange
        // act
        var result = _solutionService!.RunPart2(_input);

        // assert
        Assert.Equal(45000, result);
    }
}