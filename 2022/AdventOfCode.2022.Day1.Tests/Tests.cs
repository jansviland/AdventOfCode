namespace AdventOfCode._2022.Day1.Tests;

public class Tests : TestBed<TestFixture>
{
    private new readonly TestFixture _fixture;

    public Tests(ITestOutputHelper testOutputHelper, TestFixture fixture) : base(testOutputHelper, fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public void Test1()
    {
        // arrange
        var service = _fixture.GetService<ISolutionService>(_testOutputHelper);

        // act
        _testOutputHelper.WriteLine("Running unit test 1");
        var result = service!.Run(123);

        // assert
        Assert.Equal(123, result);
    }
}