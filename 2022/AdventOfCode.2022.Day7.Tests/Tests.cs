using System.Drawing;

namespace AdventOfCode._2022.Day7.Tests;

public class Tests : TestBed<TestFixture>
{
    private readonly ISolutionService _solutionService;

    private readonly string[] _input = new[]
    {
        "$ cd /",
        "$ ls",
        "dir a",
        "14848514 b.txt",
        "8504156 c.dat",
        "dir d",
        "$ cd a",
        "$ ls",
        "dir e",
        "29116 f",
        "2557 g",
        "62596 h.lst",
        "$ cd e",
        "$ ls",
        "584 i",
        "$ cd ..",
        "$ cd ..",
        "$ cd d",
        "$ ls",
        "4060174 j",
        "8033020 d.log",
        "5626152 d.ext",
        "7214296 k",
    };

    public Tests(ITestOutputHelper testOutputHelper, TestFixture fixture) : base(testOutputHelper, fixture)
    {
        _solutionService = _fixture.GetService<ISolutionService>(_testOutputHelper)!;
    }

    [Fact]
    public void ParseInput_RootHasCorrectChildren()
    {
        // act
        var root = _solutionService.ParseInput(_input);

        // assert
        Assert.Equal(4, root.Children.Count);

        var dirA = root.Children.First(x => x.Name == "a");
        Assert.True(dirA.IsFolder);

        var fileB = root.Children.First(x => x.Name == "b.txt");
        Assert.False(fileB.IsFolder);
        Assert.Equal(14848514, fileB.Size);

        var fileC = root.Children.First(x => x.Name == "c.dat");
        Assert.False(fileC.IsFolder);
        Assert.Equal(8504156, fileC.Size);

        var dirD = root.Children.First(x => x.Name == "d");
        Assert.True(dirD.IsFolder);
    }

    [Fact]
    public void ParseInput_AFolderHasCorrectChildren()
    {
        // act
        var root = _solutionService.ParseInput(_input);
        var dirA = root.Children.First(x => x.Name == "a");

        // assert
        Assert.True(dirA.IsFolder);
        Assert.Equal(4, dirA.Children.Count);

        // dir e
        var dirE = dirA.Children.First(x => x.Name == "e");
        Assert.True(dirE.IsFolder);

        // 29116 f
        var fileF = dirA.Children.First(x => x.Name == "f");
        Assert.False(fileF.IsFolder);
        Assert.Equal(29116, fileF.Size);
    }

    [Fact]
    public void ParseInput_FoldersHasCorrectSize()
    {
        // arrange
        var tree = _solutionService.ParseInput(_input, 1);

        // act
        var result = _solutionService.UpdateFolderSizes(tree);

        // assert

        // total size
        Assert.Equal(48381165, result);
        Assert.Equal(48381165, tree.Size);

        // size of A
        var dirA = tree.Children.First(x => x.Name == "a");
        Assert.Equal(584 + 29116 + 2557 + 62596, dirA.Size);

        // size of E
        var dirE = dirA.Children.First(x => x.Name == "e");
        Assert.Equal(584, dirE.Size);
    }

    [Fact]
    public void PrintFolderContent()
    {
        // arrange
        var tree = _solutionService.ParseInput(_input);

        // act
        var result = tree.PrintTree();

        // assert
        var expected = new[]
        {
            "- / (dir)",
            "  - a (dir)",
            "    - e (dir)",
            "      - i (file, size=584)",
            "    - f (file, size=29116)",
            "    - g (file, size=2557)",
            "    - h.lst (file, size=62596)",
            "  - b.txt (file, size=14848514)",
            "  - c.dat (file, size=8504156)",
            "  - d (dir)",
            "    - j (file, size=4060174)",
            "    - d.log (file, size=8033020)",
            "    - d.ext (file, size=5626152)",
            "    - k (file, size=7214296)"
        };

        Assert.Equal(expected, result);
    }

    [Fact]
    public void UpdateFolderSizes1()
    {
        var input = new[]
        {
            "$ cd /",
            "$ ls",
            "dir a",
            "14848514 b.txt",
            "8504156 c.dat",
        };

        // arrange
        var tree = _solutionService.ParseInput(input, 1);

        // act
        _solutionService.UpdateFolderSizes(tree);

        // assert
        Assert.Equal(14848514 + 8504156, tree.Size);
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