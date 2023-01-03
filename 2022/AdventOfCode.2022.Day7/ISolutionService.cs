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

    public void AddChild(Tree child, string path)
    {
        // If the path is empty, we are at the right place
        if (string.IsNullOrWhiteSpace(path) || path == "/")
        {
            Children.AddLast(child);
            return;
        }

        // If the path is not empty, we need to go deeper
        // TODO: navigate to /a/b/c/d etc, then add the child
        var pathParts = path.Split('/');
        var current = this;
        foreach (var pathPart in pathParts)
        {
            if (string.IsNullOrWhiteSpace(pathPart))
            {
                continue;
            }

            var childNode = current.Children.FirstOrDefault(x => x.Name == pathPart);
            if (childNode == null)
            {
                throw new Exception($"Could not find child node {pathPart} in {current.Name}");
            }

            current = childNode;
        }

        current.Children.AddLast(child);

        // Children.AddLast(child);
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
        var root = new Tree("/", 0, true);

        var currentPath = "/";
        for (var i = 0; i < input.Length; i++)
        {
            var line = input[i];

            var isCommand = line.StartsWith('$');
            if (isCommand)
            {
                var isCd = line.Contains("cd");
                if (isCd)
                {
                    var split = line.Split(' ');
                    var path = split.Last();

                    if (path == "..")
                    {
                        var pathParts = currentPath.Split('/');
                        currentPath = string.Join('/', pathParts.Take(pathParts.Length - 1));
                    }
                    else if (path == "/")
                    {
                        currentPath = "/";
                    }
                    else
                    {
                        currentPath = $"{currentPath}/{path}";
                    }
                }

                // TODO: check if cd or ls
                // TODO: if cd, update currentPath
            }
            else
            {
                // content
                var isFolder = line.StartsWith("dir");
                if (isFolder)
                {
                    var split = line.Split(" ");
                    var name = split[1];

                    var child = new Tree(name, 0, isFolder);

                    root.AddChild(child, currentPath);
                }
                else
                {
                    var split = line.Split(" ");
                    var name = split[1];
                    var size = split[0];

                    var child = new Tree(name, int.Parse(size), isFolder);

                    root.AddChild(child, currentPath);
                }
            }

            // TODO: print tree if debugLevel > 0
        }

        return root;
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