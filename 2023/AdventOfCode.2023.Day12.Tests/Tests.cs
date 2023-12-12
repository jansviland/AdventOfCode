namespace AdventOfCode._2023.Day12.Tests;

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
    [InlineData("???.### 1,1,3", 1)]
    [InlineData(".??..??...?##. 1,1,3", 4)]
    [InlineData("?#?#?#?#?#?#?#? 1,3,1,6", 1)]
    [InlineData("????.#...#... 4,1,1", 1)]
    [InlineData("????.######..#####. 1,6,5", 4)]
    [InlineData("?###???????? 3,2,1", 10)]
    public void GetPossibleArrangementsTest(string line, int count)
    {
        // arrange
        _testOutputHelper.WriteLine("Running unit test - 2023 - Day 12 - GetPossibleArrangements");

        // act
        var result = _solutionService.GetPossibleArrangements(line);

        // assert
        Assert.Equal(count, result.Length);
    }

    [Theory]
    [InlineData(".# 1", ".#?.#?.#?.#?.# 1,1,1,1,1")]
    [InlineData("???.### 1,1,3", "???.###????.###????.###????.###????.### 1,1,3,1,1,3,1,1,3,1,1,3,1,1,3")]
    public void UnFoldTest(string line, string expected)
    {
        // arrange
        _testOutputHelper.WriteLine("Running unit test - 2023 - Day 12 - UnFold");

        // act
        var result = _solutionService.UnFold(line);

        // assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void Part1Test()
    {
        // arrange
        _testOutputHelper.WriteLine("Running unit test - 2023 - Day 12 - Part 1");

        // act
        var result = _solutionService.RunPart1(_input);

        // assert
        Assert.Equal(21, result);
    }

    [Fact(Skip = "Fill fry the computer)")]
    public void Part2Test()
    {
        // arrange
        _testOutputHelper.WriteLine("Running unit test - 2023 - Day 12 - Part 2");

        // act
        var result = _solutionService.RunPart2(_input);

        // assert
        Assert.Equal(525152, result);
    }
}