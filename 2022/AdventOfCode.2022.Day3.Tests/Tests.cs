namespace AdventOfCode._2022.Day3.Tests;

public class Tests : TestBed<TestFixture>
{
    private readonly ISolutionService _solutionService;

    private readonly string[] _input = new[]
    {
        "vJrwpWtwJgWrhcsFMMfFFhFp",
        "jqHRNqRjqzjGDLGLrsFMfFZSrLrFZsSL",
        "PmmdzqPrVvPwwTWBwg",
        "wMqvLMZHhHMvwLHjbvcjnnSBnvTQFn",
        "ttgJtRGJQctTZtZT",
        "CrZsJsPPZsGzwwsLwLmpwMDw"
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
        var result = _solutionService!.RunPart1(_input);

        // assert
        Assert.Equal(157, result);
    }
    
    [Fact]
    public void ShouldGetCorrectPriority()
    {
        _testOutputHelper.WriteLine("Running unit test - Correct Priority");

        // arrange
        // act
        // assert
        // Assert.Equal(1, _solutionService!.GetPriority('a'));
        // Assert.Equal(2, _solutionService!.GetPriority('b'));
        // Assert.Equal(26, _solutionService!.GetPriority('z'));
        Assert.Equal(27, _solutionService!.GetPriority('A'));
        Assert.Equal(52, _solutionService!.GetPriority('Z'));
    }

    [Fact]
    public void ParseStringPart1Test1()
    {
        // arrange
        // act
        // assert
        var rucksack1 = _solutionService.ParseStringPart1(_input[0]);
        Assert.Equal('p', rucksack1.WrongItem);
        Assert.Equal(16, rucksack1.Priority);
        Assert.Equal("vJrwpWtwJgWr", rucksack1.Compartment1);
        Assert.Equal("hcsFMMfFFhFp", rucksack1.Compartment2);

        var rucksack2 = _solutionService.ParseStringPart1(_input[1]);
        Assert.Equal('L', rucksack2.WrongItem);
        Assert.Equal(38, rucksack2.Priority);
        Assert.Equal("jqHRNqRjqzjGDLGL", rucksack2.Compartment1);
        Assert.Equal("rsFMfFZSrLrFZsSL", rucksack2.Compartment2);

        var rucksack3 = _solutionService.ParseStringPart1(_input[2]);
        Assert.Equal('P', rucksack3.WrongItem);
        Assert.Equal(42, rucksack3.Priority);
        Assert.Equal("PmmdzqPrV", rucksack3.Compartment1);
        Assert.Equal("vPwwTWBwg", rucksack3.Compartment2);

        var rucksack4 = _solutionService.ParseStringPart1(_input[3]);
        Assert.Equal('v', rucksack4.WrongItem);
        Assert.Equal(22, rucksack4.Priority);

        var rucksack5 = _solutionService.ParseStringPart1(_input[4]);
        Assert.Equal('t', rucksack5.WrongItem);
        Assert.Equal(20, rucksack5.Priority);

        var rucksack6 = _solutionService.ParseStringPart1(_input[5]);
        Assert.Equal('s', rucksack6.WrongItem);
        Assert.Equal(19, rucksack6.Priority);
    }

    [Fact]
    public void Part2Test()
    {
        _testOutputHelper.WriteLine("Running unit test - Part 2");

        // arrange
        // act
        var result = _solutionService!.RunPart2(_input);

        // assert
        Assert.Equal(45000, result);
    }
}