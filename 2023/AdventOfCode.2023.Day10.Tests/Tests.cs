namespace AdventOfCode._2023.Day10.Tests;

public class Tests : TestBed<TestFixture>
{
    private readonly ISolutionService _solutionService;

    private readonly string[] _input;
    private readonly string[] _input_part2_A;
    private readonly string[] _input_part2_B;

    public Tests(ITestOutputHelper testOutputHelper, TestFixture fixture) : base(testOutputHelper, fixture)
    {
        _solutionService = _fixture.GetService<ISolutionService>(_testOutputHelper)!;
        _input = File.ReadAllLines("Assets/test-input.txt");
        _input_part2_A = File.ReadAllLines("Assets/test-input-part-2-A.txt");
        _input_part2_B = File.ReadAllLines("Assets/test-input-part-2-B.txt");
    }

    [Fact]
    public void Part1Test()
    {
        // arrange
        _testOutputHelper.WriteLine("Running unit test - 2023 - Day 10 - Part 1");

        // act
        var result = _solutionService.RunPart1(_input);

        // assert
        Assert.Equal(8, result);
    }

    [Theory]
    [InlineData(2, 6, true)]
    [InlineData(3, 6, true)]
    [InlineData(6, 6, true)]
    [InlineData(7, 6, true)]
    [InlineData(3, 3, false)]
    [InlineData(4, 3, false)]
    [InlineData(5, 3, false)]
    [InlineData(6, 3, false)]
    public void Part2IsInsideTest(int x, int y, bool expected)
    {
        // arrange
        var grid = _solutionService.CreateGrid(_input_part2_A);
        var start = grid[x, y];
 
        // act
        var result = _solutionService.IsInside(start, grid);

        // assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void Part2TestA()
    {
        // arrange
        _testOutputHelper.WriteLine("Running unit test - 2023 - Day 10 - Part 2");

        // act
        var result = _solutionService.RunPart2(_input_part2_A);

        // assert
        Assert.Equal(4, result);
    }

    [Fact]
    public void Part2TestB()
    {
        // arrange
        _testOutputHelper.WriteLine("Running unit test - 2023 - Day 10 - Part 2");

        // act
        var result = _solutionService.RunPart2(_input_part2_B);

        // assert
        Assert.Equal(8, result);
    }
}