namespace AdventOfCode._2022.Day2.Tests;

public class Tests : TestBed<TestFixture>
{
    private readonly ISolutionService _solutionService;
    private readonly string[] _input;

    public Tests(ITestOutputHelper testOutputHelper, TestFixture fixture) : base(testOutputHelper, fixture)
    {
        _solutionService = _fixture.GetService<ISolutionService>(_testOutputHelper)!;
        _input = new[] { "A Y", "B X", "C Z" };
    }

    [Fact]
    public void SolutionPart1Test()
    {
        _testOutputHelper.WriteLine("Running unit test: Solution Part 1");

        // arrange
        // act
        var result = _solutionService!.Run(_input);

        // assert
        Assert.Equal(15, result);
    }

    [Fact]
    public void SolutionPart2Test()
    {
        _testOutputHelper.WriteLine("Running unit test: Solution Part 2");

        // arrange
        // act
        var result = _solutionService!.Run(_input);

        // assert
        Assert.Equal(12, result);
    }

    [Fact]
    public void CalculateRowPart1Test()
    {
        _testOutputHelper.WriteLine("Running unit test: Calculate Row");

        // arrange
        // act
        // assert
        Assert.Equal(8, _solutionService!.CalculateRowPart1(_input[0]));
        Assert.Equal(1, _solutionService!.CalculateRowPart1(_input[1]));
        Assert.Equal(6, _solutionService!.CalculateRowPart1(_input[2]));
    }

    [Fact]
    public void CalculateRowPart2Test()
    {
        _testOutputHelper.WriteLine("Running unit test: Calculate Row");

        // arrange
        // act
        // assert
        Assert.Equal(4, _solutionService!.CalculateRowPart2(_input[0]));
        Assert.Equal(1, _solutionService!.CalculateRowPart2(_input[1]));
        Assert.Equal(7, _solutionService!.CalculateRowPart2(_input[2]));
    }
}