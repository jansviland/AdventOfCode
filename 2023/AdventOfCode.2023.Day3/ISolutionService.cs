namespace AdventOfCode._2023.Day3;

public interface ISolutionService
{
    public int RunPart1(string[] input, bool printGrid = false);
    public int RunPart2(string[] input);
}

public class SolutionService : ISolutionService
{
    private readonly ILogger<SolutionService> _logger;
    // private readonly char[] symbols = new char[] { '#', '$', '%', '^', '&', '*', '@', '!', '/', '\'', '+', '-', '=', '?', '<', '>', '~', '`', '|', '\\', '(', ')', '[', ']', '{', '}'  };

    public SolutionService(ILogger<SolutionService> logger)
    {
        _logger = logger;
    }

    private bool IsSymbol(char c)
    {
        if (c != '.' && !char.IsDigit(c))
        {
            return true;
        }

        return false;
    }

    public bool IsAdjacent(char[,] grid, int x, int y)
    {
        // check one left
        if (x > 0 && IsSymbol(grid[y, x - 1]))
        {
            return true;
        }

        // check one right
        if (x < grid.GetLength(1) - 1 && IsSymbol(grid[y, x + 1]))
        {
            return true;
        }

        // check one up
        if (y > 0 && IsSymbol(grid[y - 1, x]))
        {
            return true;
        }

        // check one down
        if (y < grid.GetLength(0) - 1 && IsSymbol(grid[y + 1, x]))
        {
            return true;
        }

        // check one up and one left
        if (x > 0 && y > 0 && IsSymbol(grid[y - 1, x - 1]))
        {
            return true;
        }

        // check one up and one right
        if (x < grid.GetLength(1) - 1 && y > 0 && IsSymbol(grid[y - 1, x + 1]))
        {
            return true;
        }

        // check one down and one left
        if (x > 0 && y < grid.GetLength(0) - 1 && IsSymbol(grid[y + 1, x - 1]))
        {
            return true;
        }

        // check one down and one right
        if (x < grid.GetLength(1) - 1 && y < grid.GetLength(0) - 1 && IsSymbol(grid[y + 1, x + 1]))
        {
            return true;
        }

        return false;
    }

    public int RunPart1(string[] input, bool printGrid = false)
    {
        _logger.LogInformation("Solving - 2023 - Day 3 - Part 1");
        _logger.LogInformation("Input contains {Input} values", input.Length);

        // create a 2D array of chars
        var grid = new char[input.Length, input[0].Length];
        for (var x = 0; x < input.Length; x++)
        {
            var line = input[x];
            for (var y = 0; y < line.Length; y++)
            {
                grid[x, y] = line[y];
            }
        }

        var numbersAdjacent = new List<int>();

        // start at the top left corner
        for (var y = 0; y < grid.GetLength(0); y++)
        {
            var isAdjacent = false;
            var numberString = "";

            var line = input[y];
            for (var x = 0; x < line.Length; x++)
            {
                // get the character at the current position
                char character = grid[y, x];

                // if the character is a digit, add it to the number string
                if (char.IsDigit(character))
                {
                    // moving from left to right, check if we still have a character 0-9, and append it to the number string
                    numberString += character;

                    // check if part of the number is adjacent to a symbol
                    // only check if we haven't already found an adjacent symbol (only one part of the number needs to be adjacent)
                    if (!isAdjacent)
                    {
                        isAdjacent = IsAdjacent(grid, x, y);
                    }
                }

                if (printGrid)
                {
                    // change color of console output if the character is a symbol
                    if (IsSymbol(character))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }
                    else if (isAdjacent)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                    }

                    Console.Write(character);
                }

                // we have three separate conditions for when to add the number to the list of numbers:
                // 1. we have reached the end of the number, so add it to the list of numbers (if it is adjacent to a symbol)
                // 2. we have reached the end of row along the x axis, so add the number to the list of numbers (if it is adjacent to a symbol)
                // 3. we have reached the end of column along the y axis, so add the number to the list of numbers (if it is adjacent to a symbol)
                if (!char.IsDigit(character) || x == line.Length - 1 || y == grid.GetLength(0) - 1)
                {
                    // we have reached the end of the number, so add it to the list of numbers
                    if (numberString.Length > 0 && isAdjacent)
                    {
                        numbersAdjacent.Add(int.Parse(numberString));
                    }

                    // reset
                    numberString = "";
                    isAdjacent = false;
                }

            }

            if (printGrid)
            {
                Console.WriteLine("");
            }
        }

        return numbersAdjacent.Sum();
    }

    public int RunPart2(string[] input)
    {
        _logger.LogInformation("Solving - 2023 - Day 3 - Part 2");
        _logger.LogInformation("Input contains {Input} values", input.Length);

        throw new NotImplementedException();
    }
}