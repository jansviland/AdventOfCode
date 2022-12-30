namespace AdventOfCode._2022.Day7.Tests;

public class Tests : TestBed<TestFixture>
{
    private readonly ISolutionService _solutionService;

    private readonly string[] _input = new[]
    {
        "$ cd /",
        "$ ls",
        "$dir a",
        "$14848514 b.txt",
        "$8504156 c.dat",
        "$dir d",
        "$$ cd a",
        "$$ ls",
        "$dir e",
        "$29116 f",
        "$2557 g",
        "$62596 h.lst",
        "$$ cd e",
        "$$ ls",
        "$584 i",
        "$$ cd ..",
        "$$ cd ..",
        "$$ cd d",
        "$$ ls",
        "$4060174 j",
        "$8033020 d.log",
        "$5626152 d.ext",
        "$7214296 k",
    };

    public Tests(ITestOutputHelper testOutputHelper, TestFixture fixture) : base(testOutputHelper, fixture)
    {
        _solutionService = _fixture.GetService<ISolutionService>(_testOutputHelper)!;
    }

    [Fact]
    public void ParseInputTest()
    {
        // act
        var result = _solutionService.ParseInput(_input);

        // assert
        Assert.Equal(13, result.Children.Count);

        // a (dir)
        var dirA = result.Children.First(x => x.Name == "a");
        Assert.True(dirA.IsFolder);

        // i (file, size=584)
        var fileI = result.Children.First(x => x.Name == "i");
        Assert.False(fileI.IsFolder);
        Assert.Equal(584, fileI.Size);

        // k (file, size=7214296)
        var fileK = result.Children.First(x => x.Name == "k");
        Assert.False(fileK.IsFolder);
        Assert.Equal(7214296, fileK.Size);
    }

    [Fact]
    public void PrintFolderContent()
    {
        // arrange
        var tree = _solutionService.ParseInput(_input);

        // act
        var result = _solutionService.GetFolderContent(tree, "/");

        // assert
        var expected = new[]
        {
            "- / (dir)",
            "    - a (dir)",
            "    - e (dir)",
            "    - i (file, size=584)",
            "    - f (file, size=29116)",
            "    - g (file, size=2557)",
            "    - h.lst (file, size=62596)",
            "    - b.txt (file, size=14848514)",
            "    - c.dat (file, size=8504156)",
            "    - d (dir)",
            "    - j (file, size=4060174)",
            "    - d.log (file, size=8033020)",
            "    - d.ext (file, size=5626152)",
            "    - k (file, size=7214296)"
        };

        Assert.Equal(expected, result);
    }

    [Fact]
    public void Part1Test()
    {
        _testOutputHelper.WriteLine("Running unit test - Part 1");

        // arrange
        // act
        var result = _solutionService.RunPart1(_input);

        // assert
        Assert.Equal(95437, result);
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