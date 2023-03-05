namespace AdventOfCode._2022.Day7;

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

            childNode.Parent = current;

            // set folder to current node
            current = childNode;
        }

        // finally add to the last folder in the path
        current.Children.AddLast(child);
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

    public IEnumerable<Tree> Traverse()
    {
        var result = new List<Tree>();

        result.Add(this);

        foreach (var child in Children)
        {
            result.AddRange(child.Traverse());
        }

        return result;
    }

    public IEnumerable<Tree> TraverseAndResetSize()
    {
        var result = new List<Tree>();

        result.Add(this);

        foreach (var child in Children)
        {
            result.AddRange(child.TraverseAndResetSize());
            this.Size = 0;
        }

        return result;
    }

    /// <summary>
    /// Traverse the tree and update the size of each folder
    ///
    /// Note: Only call this on the root node, only run it once.
    /// To run a second time, call TraverseAndResetSize() first
    /// 
    /// </summary>
    /// <returns>
    /// List of all nodes in the tree
    /// </returns>
    public IEnumerable<Tree> TraverseAndUpdate()
    {
        var result = new List<Tree>();

        result.Add(this);

        foreach (var child in Children)
        {
            result.AddRange(child.TraverseAndUpdate());
            this.Size += child.Size;
        }

        return result;
    }
}