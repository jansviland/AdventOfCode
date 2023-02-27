namespace AdventOfCode._2022.Day7;

public interface ISolutionService
{
    public int RunPart1(string[] input);
    public Tree ParseInput(string[] input, int debugLevel = 0);
    public int RunPart2(string[] input);
}

public class Tree
{
    public string Name;
    public int Size;
    public bool IsFolder;
    public Tree? Parent;
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

        var pathParts = path.Split('/', StringSplitOptions.RemoveEmptyEntries);
        var current = this;

        // split into folders
        foreach (var pathPart in pathParts)
        {
            var childNode = current.Children.FirstOrDefault(x => x.Name == pathPart);
            if (childNode == null)
            {
                throw new Exception($"Could not find child node {pathPart} in {current.Name}");
            }

            // increase the size of the parent
            // current.Size += child.Size;

            childNode.Parent = current;

            // set folder to current node
            current = childNode;
        }

        // TODO: update size of folder, folder size is sum of all children + all children's children

        // finally add to the last folder in the path
        current.Children.AddLast(child);

        // TODO: update size of current folder + all parents
    }

    public List<string> PrintTree(int level = 0)
    {
        var lines = new List<string>();

        var indent = new string(' ', level * 2);
        if (IsFolder)
        {
            lines.Add($"{indent}- {Name} (dir)");
        }
        else
        {
            lines.Add($"{indent}- {Name} (file, size={Size})");
        }

        foreach (var child in Children)
        {
            lines.AddRange(child.PrintTree(level + 1));
        }

        return lines;
    }

    // public int GetTotalSize()
    // {
    //     var size = Size;
    //     foreach (var child in Children.Where(x => x.IsFolder))
    //     {
    //         size += child.GetTotalSize();
    //     }
    //
    //     return size;
    // }
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
                        var pathParts = currentPath.Split('/', StringSplitOptions.RemoveEmptyEntries);
                        currentPath = string.Join('/', pathParts.Take(pathParts.Length - 1));
                    }
                    else if (path == "/")
                    {
                        currentPath = "/";
                    }
                    else
                    {
                        currentPath += $"{path}/";
                    }
                }
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

            // print tree for every lines parsed
            if (debugLevel > 1)
            {
                PrintTree(root);
            }
        }

        // print tree when done
        if (debugLevel > 0)
        {
            PrintTree(root);
        }

        return root;
    }

    private void PrintTree(Tree tree)
    {
        var lines = tree.PrintTree();
        foreach (var l in lines)
        {
            _logger.LogInformation(l);
        }
    }

    public int RunPart1(string[] input)
    {
        _logger.LogInformation("Solving day 7");
        _logger.LogInformation("Input contains {Input} values", input.Length);

        var root = ParseInput(input, 2);

        throw new NotImplementedException();
    }

    public int RunPart2(string[] input)
    {
        _logger.LogInformation("Solving day 7 - Part 2");
        _logger.LogInformation("Input contains {Input} values", input.Length);

        throw new NotImplementedException();
    }
}