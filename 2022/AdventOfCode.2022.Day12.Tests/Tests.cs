using AdventOfCode._2022.Day12.Common.Models;

namespace AdventOfCode._2022.Day12.Tests;

public class Tests : TestBed<TestFixture>
{
    private readonly ISolutionService _solutionService;

    private readonly string[] _input = new[]
    {
        "Sabqponm",
        "abcryxxl",
        "accszExk",
        "acctuvwj",
        "abdefghi",
    };

    public Tests(ITestOutputHelper testOutputHelper, TestFixture fixture) : base(testOutputHelper, fixture)
    {
        _solutionService = _fixture.GetService<ISolutionService>(_testOutputHelper)!;
    }

    [Fact]
    public void FindPathTest()
    {
        // act
        var result = _solutionService.FindPath(_input);

        // assert
        // var expected = new[]
        // {
        //     "v..v<<<<",
        //     ">v.vv<<^",
        //     ".>vv>E^^",
        //     "..v>>>^^",
        //     "..>>>>>^",
        // };
        //
        // Assert.Equal(expected, result.Path);
        // Assert.Equal(31, result.Steps);
    }

    [Fact]
    public void ParseInputTestRow0()
    {
        // act
        var grid = _solutionService.ParseInput(_input);

        // assert
        Assert.Equal(8, grid.GetLength(0));
        Assert.Equal(5, grid.GetLength(1));

        // "Sabqponm",

        // top left should be a snake
        Assert.Equal(GridElementType.Snake, grid[0, 0].Type);
        Assert.Equal("S", grid[0, 0].Value);

        Assert.Equal(GridElementType.Empty, grid[0, 1].Type);
        Assert.Equal("a", grid[0, 1].Value);

        Assert.Equal(GridElementType.Empty, grid[0, 2].Type);
        Assert.Equal("b", grid[0, 2].Value);

        Assert.Equal(GridElementType.Empty, grid[0, 3].Type);
        Assert.Equal("q", grid[0, 3].Value);
    }

    [Fact]
    public void Part1Test()
    {
        _testOutputHelper.WriteLine("Running unit test - Part 1");

        // arrange
        // act
        var result = _solutionService.RunPart1(_input);

        // assert
        Assert.Equal(157, result);
    }

    [Fact]
    public void Part2Test()
    {
        _testOutputHelper.WriteLine("Running unit test - Part 2");

        // arrange
        // act
        var result = _solutionService.RunPart2(_input);

        // assert
        Assert.Equal(45000, result);
    }
}