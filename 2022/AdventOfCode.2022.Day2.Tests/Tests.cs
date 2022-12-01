namespace AdventOfCode._2022.Day2.Tests;

public class Tests : TestBed<TestFixture>
{
    private readonly ISolutionService _solutionService;
    private readonly string[] _input = new[] { "1000", "2000", "3000", "", "4000", "", "5000", "6000", "", "7000", "8000", "9000", "", "10000" };

    public Tests(ITestOutputHelper testOutputHelper, TestFixture fixture) : base(testOutputHelper, fixture)
    {
        _solutionService = _fixture.GetService<ISolutionService>(_testOutputHelper)!;
    }

    [Fact]
    public void Test1()
    {
        _testOutputHelper.WriteLine("Running unit test 1");

        // arrange
        // act
        var result = _solutionService!.Run(_input);

        // assert
        Assert.Equal(123, result);
    }
}