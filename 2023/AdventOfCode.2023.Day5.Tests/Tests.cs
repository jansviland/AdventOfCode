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

    [Theory]
    [InlineData(79, 81)]
    [InlineData(14, 14)]
    [InlineData(55, 57)]
    [InlineData(13, 13)]
    public void TestMapping1(long seed, long result)
    {
        // arrange
        var distanceMap = new DistanceMap
        {
            Type = 0,
            Mapping = new List<DistanceMapping>
            {
                new DistanceMapping
                {
                    DestinationRangeStart = 50,
                    SourceRangeStart = 98,
                    RangeLength = 2
                },
                new DistanceMapping
                {
                    DestinationRangeStart = 52,
                    SourceRangeStart = 50,
                    RangeLength = 48
                }
            }
        };

        Assert.Equal(result, _solutionService.GetNewPosition(seed, distanceMap));
    }

    [Theory]
    [InlineData(81, 81)]
    [InlineData(14, 53)]
    [InlineData(57, 57)]
    [InlineData(13, 52)]
    public void TestMapping2(long seed, long result)
    {
        // arrange
        var distanceMap = new DistanceMap
        {
            Type = 1,
            Mapping = new List<DistanceMapping>
            {
                new DistanceMapping
                {
                    DestinationRangeStart = 0,
                    SourceRangeStart = 15,
                    RangeLength = 37
                },
                new DistanceMapping
                {
                    DestinationRangeStart = 37,
                    SourceRangeStart = 52,
                    RangeLength = 2
                },
                new DistanceMapping
                {
                    DestinationRangeStart = 39,
                    SourceRangeStart = 0,
                    RangeLength = 15
                },
            }
        };

        Assert.Equal(result, _solutionService.GetNewPosition(seed, distanceMap));
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

    [Fact]
    public void Part2Test()
    {
        // arrange
        _testOutputHelper.WriteLine("Running unit test - Part 2");

        // act
        long result = _solutionService.RunPart2(_input);

        // assert
        Assert.Equal(46, result);
    }
}