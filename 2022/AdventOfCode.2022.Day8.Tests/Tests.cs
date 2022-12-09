namespace AdventOfCode._2022.Day8.Tests;

public class Tests : TestBed<TestFixture>
{
    private readonly ISolutionService _solutionService;

    private readonly string[] _input = new[]
    {
        "30373",
        "25512",
        "65332",
        "33549",
        "35390",
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
        Assert.Equal(21, result);
    }

    [Fact]
    public void ParseInputTest()
    {
        // arrange
        // act
        var result = _solutionService.ParseInput(_input);

        // assert
        Assert.Equal(25, result.Length);

        Assert.Equal(3, result[0, 0]);
        Assert.Equal(3, result[0, 4]);
        Assert.Equal(3, result[4, 0]);
        Assert.Equal(0, result[4, 4]);

        Assert.Equal(2, result[0, 1]);
        Assert.Equal(6, result[0, 2]);
        Assert.Equal(3, result[0, 3]);
        Assert.Equal(3, result[0, 4]);

        Assert.Equal(5, result[2, 1]);
        Assert.Equal(3, result[2, 2]);
        Assert.Equal(5, result[2, 3]);
        Assert.Equal(3, result[2, 4]);
    }

    [Fact]
    public void IsVisibleTest()
    {
        // arrange
        var grid = _solutionService.ParseInput(_input);

        // act

        // assert
        // edges are always visible
        Assert.True(_solutionService.IsVisible(grid, 0, 0, Direction.Left));
        Assert.True(_solutionService.IsVisible(grid, 4, 4, Direction.Left));

        // The top-left 5 is visible from the left and top.
        Assert.True(_solutionService.IsVisible(grid, 1, 1, Direction.Left));
        Assert.True(_solutionService.IsVisible(grid, 1, 1, Direction.Up));

        // The top-middle 5 is visible from the top and right.
        Assert.True(_solutionService.IsVisible(grid, 2, 1, Direction.Right));
        Assert.True(_solutionService.IsVisible(grid, 2, 1, Direction.Up));

        // The top-right 1 is not visible from any direction.
        Assert.False(_solutionService.IsVisible(grid, 3, 1, Direction.Up));
        Assert.False(_solutionService.IsVisible(grid, 3, 1, Direction.Down));
        Assert.False(_solutionService.IsVisible(grid, 3, 1, Direction.Left));
        Assert.False(_solutionService.IsVisible(grid, 3, 1, Direction.Right));

        // The left-middle 5 is visible, but only from the right.
        Assert.False(_solutionService.IsVisible(grid, 1, 2, Direction.Up));
        Assert.False(_solutionService.IsVisible(grid, 1, 2, Direction.Down));
        Assert.False(_solutionService.IsVisible(grid, 1, 2, Direction.Left));
        Assert.True(_solutionService.IsVisible(grid, 1, 2, Direction.Right));

        // The center 3 is not visible from any direction;
        Assert.False(_solutionService.IsVisible(grid, 2, 2, Direction.Up));
        Assert.False(_solutionService.IsVisible(grid, 2, 2, Direction.Down));
        Assert.False(_solutionService.IsVisible(grid, 2, 2, Direction.Left));
        Assert.False(_solutionService.IsVisible(grid, 2, 2, Direction.Right));

        // The right-middle 3 is visible from the right.
        Assert.False(_solutionService.IsVisible(grid, 3, 2, Direction.Up));
        Assert.False(_solutionService.IsVisible(grid, 3, 2, Direction.Down));
        Assert.False(_solutionService.IsVisible(grid, 3, 2, Direction.Left));
        Assert.True(_solutionService.IsVisible(grid, 3, 2, Direction.Right));

        // In the bottom row, the middle 5 is visible, but the 3 and 4 are not.
        Assert.False(_solutionService.IsVisible(grid, 1, 3, Direction.Up));
        Assert.False(_solutionService.IsVisible(grid, 1, 3, Direction.Down));
        Assert.False(_solutionService.IsVisible(grid, 1, 3, Direction.Left));
        Assert.False(_solutionService.IsVisible(grid, 1, 3, Direction.Right));

        Assert.False(_solutionService.IsVisible(grid, 2, 3, Direction.Up));
        Assert.True(_solutionService.IsVisible(grid, 2, 3, Direction.Down));
        Assert.True(_solutionService.IsVisible(grid, 2, 3, Direction.Left));
        Assert.False(_solutionService.IsVisible(grid, 2, 3, Direction.Right));

        Assert.False(_solutionService.IsVisible(grid, 3, 3, Direction.Up));
        Assert.False(_solutionService.IsVisible(grid, 3, 3, Direction.Down));
        Assert.False(_solutionService.IsVisible(grid, 3, 3, Direction.Left));
        Assert.False(_solutionService.IsVisible(grid, 3, 3, Direction.Right));
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