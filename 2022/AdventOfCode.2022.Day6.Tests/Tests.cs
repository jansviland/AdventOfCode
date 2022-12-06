namespace AdventOfCode._2022.Day6.Tests;

public class Tests : TestBed<TestFixture>
{
    private readonly ISolutionService _solutionService;

    private readonly string[] _input = new[]
    {
        "mjqjpqmgbljsphdztnvjfqwrcgsmlb",
        "bvwbjplbgvbhsrlpgdmjqwftvncz",
        "nppdvjthqldpwncqszvftbrmjlhg",
        "nznrnfrfntjfmvfwmzdfjlvtqnbhcprsg",
        "zcfzfwzzqfrljwzlrfnpqdbhtmscgvjw",
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
        // assert
        Assert.Equal(7, _solutionService!.RunPart1(_input[0]));
        Assert.Equal(5, _solutionService!.RunPart1(_input[1]));
        Assert.Equal(6, _solutionService!.RunPart1(_input[2]));
        Assert.Equal(10, _solutionService!.RunPart1(_input[3]));
        Assert.Equal(11, _solutionService!.RunPart1(_input[4]));
    }

    [Fact]
    public void Part2Test()
    {
        _testOutputHelper.WriteLine("Running unit test - Part 2");

        // arrange
        // act
        // assert
        Assert.Equal(19, _solutionService!.RunPart2(_input[0]));
        Assert.Equal(23, _solutionService!.RunPart2(_input[1]));
        Assert.Equal(23, _solutionService!.RunPart2(_input[2]));
        Assert.Equal(29, _solutionService!.RunPart2(_input[3]));
        Assert.Equal(26, _solutionService!.RunPart2(_input[4]));
    }
}