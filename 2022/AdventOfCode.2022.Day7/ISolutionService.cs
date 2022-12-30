namespace AdventOfCode._2022.Day7;

public interface ISolutionService
{
    public int RunPart1(string[] input);
    public Tree ParseInput(string[] input, int debugLevel = 0);
    public string[] GetFolderContent(Tree tree, string path);
    public int RunPart2(string[] input);
}

public class Tree
{
    public string Name;
    public int Size;
    public bool IsFolder;
    public LinkedList<Tree> Children;

    public Tree(string name, int size, bool isFolder)
    {
        this.Name = name;
        this.Size = size;
        this.IsFolder = isFolder;
        Children = new LinkedList<Tree>();
    }

    // TODO: AddChild
    // TODO: Traverse
}

public class SolutionService : ISolutionService
{
    private readonly ILogger<SolutionService> _logger;

    public SolutionService(ILogger<SolutionService> logger)
    {
        _logger = logger;
    }

    public Tree ParseInput(string[] input, int debugLevel)
    {
        // TODO: go through each line, print folder content every time new folder or files are found, if debugLevel > 0
        throw new NotImplementedException();
    }

    public int RunPart1(string[] input)
    {
        _logger.LogInformation("Solving day 7");
        _logger.LogInformation("Input contains {Input} values", input.Length);

        throw new NotImplementedException();
    }

    public string[] GetFolderContent(Tree tree, string path)
    {
        throw new NotImplementedException();
    }

    public int RunPart2(string[] input)
    {
        _logger.LogInformation("Solving day 7 - Part 2");
        _logger.LogInformation("Input contains {Input} values", input.Length);

        throw new NotImplementedException();
    }
}