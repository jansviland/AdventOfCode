using AdventOfCode._2024.Day09;

namespace AdventOfCodeAdventOfCode._2024.Day09.Tests;

public class Tests : TestBed<TestFixture>
{
    private readonly ISolutionService _solutionService;
    private readonly Helper _helper = new();

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
        _testOutputHelper.WriteLine($"Running unit test - {_helper.GetYear()} - Day {_helper.GetDay()} - Part 1");

        // act
        var result = _solutionService.RunPart1(_input);

        // assert
        Assert.Equal(1928UL, result);
    }

    [Theory]
    [InlineData("12345", "[0]..[1][1][1]....[2][2][2][2][2]")]
    [InlineData("2333133121414131402", "[0][0]...[1][1][1]...[2]...[3][3][3].[4][4].[5][5][5][5].[6][6][6][6].[7][7][7].[8][8][8][8][9][9]")]
    public void ParseLineTest(string line, string expected)
    {
        var result = _solutionService.ParseLine(line);
        Assert.Equal(expected, result);
    }
    
    [Theory]
    [InlineData("[0]..[1][1][1]....[2][2][2][2][2]", "[0][2][2][1][1][1][2][2][2]......")]
    public void ReOrderTest(string line, string expected)
    {
        var result = _solutionService.ReOrder(line);
        Assert.Equal(expected, result);
    }
    
    [Theory]
    [InlineData("[0][0][9][9][8][1][1][1][8][8][8][2][7][7][7][3][3][3][6][4][4][6][5][5][5][5][6][6]............", 1928)]
    public void CalcChecksumTest(string line, ulong expected)
    {
        var result = _solutionService.CalcChecksum(line);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void Part2Test()
    {
        // arrange
        _testOutputHelper.WriteLine($"Running unit test - {_helper.GetYear()} - Day {_helper.GetDay()} - Part 2");

        // act
        var result = _solutionService.RunPart2(_input);

        // assert
        Assert.Equal(31, result);
    }
}