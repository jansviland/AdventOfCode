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
    public void FinalSolutionPart1Test()
    {
        _testOutputHelper.WriteLine("Running unit test: Final Solution");

        // arrange
        // act
        var result = _solutionService!.Run(_input);

        // assert
        Assert.Equal(15, result);
    }

    [Fact]
    public void CalculateRowTest()
    {
        _testOutputHelper.WriteLine("Running unit test: Calculate Row");

        // arrange
        // act
        // assert
        Assert.Equal(8, _solutionService!.CalculateRow(_input[0]));
        Assert.Equal(1, _solutionService!.CalculateRow(_input[1]));
        Assert.Equal(6, _solutionService!.CalculateRow(_input[2]));
    }
}