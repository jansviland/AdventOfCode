using Xunit.Abstractions;
using Xunit.Microsoft.DependencyInjection.Abstracts;

namespace AdventOfCode._2022.Day1.Tests;

public class Day1Tests : TestBed<TestFixture>
{
    private readonly TestFixture _fixture;

    public Day1Tests(ITestOutputHelper testOutputHelper, TestFixture fixture) : base(testOutputHelper, fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public void Test1()
    {
        // arrange
        var service = _fixture.GetService<IDay1Service>(_testOutputHelper);

        // act
        var result = service!.Run(123);

        // assert
        Assert.Equal(123, result);
    }
}