namespace AdventOfCode._2022.Day3.Tests;

public class Tests : TestBed<TestFixture>
{
    private readonly ISolutionService _solutionService;
    private readonly string[] _input = new[] { "1000", "2000", "3000", "", "4000", "", "5000", "6000", "", "7000", "8000", "9000", "", "10000" };

    public Tests(ITestOutputHelper testOutputHelper, TestFixture fixture) : base(testOutputHelper, fixture)
    {
        _solutionService = _fixture.GetService<ISolutionService>(_testOutputHelper)!;
    }

    [Fact]
    public void SolutionExampleTest()
    {
        _testOutputHelper.WriteLine("Running unit test 1");

        // arrange
        // act

        var result = _solutionService!.Run(_input);

        // Find the Elf carrying the most Calories. How many total Calories is that Elf carrying?

        // assert
        Assert.Equal(24000, result);
    }

    [Fact]
    public void GroupByElfTest()
    {
        // arrange
        // act
        var result = _solutionService!.GroupByElf(_input);

        // The first Elf is carrying food with 1000, 2000, and 3000 Calories, a total of 6000 Calories.
        // The second Elf is carrying one food item with 4000 Calories.
        // The third Elf is carrying food with 5000 and 6000 Calories, a total of 11000 Calories.
        // The fourth Elf is carrying food with 7000, 8000, and 9000 Calories, a total of 24000 Calories.
        // The fifth Elf is carrying one food item with 10000 Calories.

        // assert
        Assert.Equal(6000, result[0]);
        Assert.Equal(4000, result[1]);
        Assert.Equal(11000, result[2]);
        Assert.Equal(24000, result[3]);
        Assert.Equal(10000, result[4]);
    }

    [Fact]
    public void Part2Test()
    {
        // arrange
        // act
        var result = _solutionService!.RunPart2(_input);

        // assert
        Assert.Equal(45000, result);
    }
}