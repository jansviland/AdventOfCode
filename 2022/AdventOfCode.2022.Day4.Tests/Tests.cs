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
        var result = _solutionService.RunPart1(_input);

        // assert
        Assert.Equal(2, result);
    }

    [Fact]
    public void StringOverlapTest()
    {
        // arrange
        // act
        // assert
        Assert.False(_solutionService.StringsOverlap(_input[0]));
        Assert.False(_solutionService.StringsOverlap(_input[1]));
        Assert.False(_solutionService.StringsOverlap(_input[2]));
        Assert.True(_solutionService.StringsOverlap(_input[3]));
        Assert.True(_solutionService.StringsOverlap(_input[4]));
        Assert.False(_solutionService.StringsOverlap(_input[5]));
    }

    [Fact]
    public void StringOverlapTest2()
    {
        // arrange
        var input = "1-100,200-250";

        // act
        // assert
        Assert.False(_solutionService.StringsOverlap(input));
    }

    [Fact]
    public void Part2Test()
    {
        _testOutputHelper.WriteLine("Running unit test - Part 2");

        // arrange
        // act
        var result = _solutionService.RunPart2(_input);

        // assert
        Assert.Equal(4, result);
    }

    [Fact]
    public void FindOverlappingStringsTest1()
    {
        // arrange
        // act
        // assert
        var result = _solutionService.FindOverlappingStrings(_input[5]);

        Assert.Equal("4", result[0]);
        Assert.Equal("5", result[1]);
        Assert.Equal("6", result[2]);
    }

    [Fact]
    public void FindOverlappingStringsTest2()
    {
        // arrange
        // act
        // assert
        Assert.Empty(_solutionService.FindOverlappingStrings(_input[0]));
        Assert.Empty(_solutionService.FindOverlappingStrings(_input[1]));
        Assert.Single(_solutionService.FindOverlappingStrings(_input[2]));
        Assert.Equal(5, _solutionService.FindOverlappingStrings(_input[3]).Count());
        Assert.Single(_solutionService.FindOverlappingStrings(_input[4]));
        Assert.Equal(3, _solutionService.FindOverlappingStrings(_input[5]).Count());
    }
}