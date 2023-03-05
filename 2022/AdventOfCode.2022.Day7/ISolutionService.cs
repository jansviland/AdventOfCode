using System.Diagnostics.Eventing.Reader;

namespace AdventOfCode._2022.Day7;

public interface ISolutionService
{
    public int RunPart1(string[] input, int debugLevel = 0);
    public Tree ParseInput(string[] input, int debugLevel = 0);
    public int RunPart2(string[] input, int debugLevel = 0);
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
                        if (currentPath.EndsWith("/"))
                        {
                            currentPath += $"{path}/";
                        }
                        else
                        {
                            currentPath += $"/{path}/";
                        }
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

        root.TraverseAndUpdate();

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

    public int RunPart1(string[] input, int debugLevel = 0)
    {
        _logger.LogInformation("Solving day 7");
        _logger.LogInformation("Input contains {Input} values", input.Length);

        var root = ParseInput(input, debugLevel);

        // Find all of the directories with a total size of at most 100000. What is the sum of the total sizes of those directories?
        return root
            .Traverse()
            .Where(x => x.IsFolder && x.Size <= 100000)
            .Sum(x => x.Size);
    }

    public int RunPart2(string[] input, int debugLevel = 0)
    {
        _logger.LogInformation("Solving day 7 - Part 2");
        _logger.LogInformation("Input contains {Input} values", input.Length);

        var root = ParseInput(input, debugLevel);
        var unusedDiskSpace = 70000000 - root.Size;

        var folders = root
            .Traverse()
            .Where(x => x.IsFolder)
            .OrderBy(x => x.Size)
            .ToList();

        for (var i = 0; i < folders.Count; i++)
        {
            var unusedDiskSpaceAfter = unusedDiskSpace + folders[i].Size;
            if (unusedDiskSpaceAfter >= 30000000)
            {
                return folders[i].Size;
            }
        }

        return 0;
    }
}