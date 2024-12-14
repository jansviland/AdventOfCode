namespace AdventOfCode._2024.Day05;

public class SolutionService3 : ISolutionService
{
    private readonly ILogger<ISolutionService> _logger;
    private readonly Helper _helper = new();

    public SolutionService3(ILogger<SolutionService> logger)
    {
        _logger = logger;
    }

    // inspired by encse
    public long RunPart1(string[] input)
    {
        _logger.LogInformation("Solving - {Year} - Day {Day} - Part 1", _helper.GetYear(), _helper.GetDay());
        _logger.LogInformation("Input contains {Input} values", input.Length);

        var (updates, comparer) = Parse(input);

        // count the pages that have the correct order and return the sum of the middle element in the list
        return updates
            .Where(pages => Sorted(pages, comparer))
            .Sum(GetMiddlePage);
    }

    public long RunPart2(string[] input)
    {
        _logger.LogInformation("Solving - {Year} - Day {Day} - Part 2", _helper.GetYear(), _helper.GetDay());
        _logger.LogInformation("Input contains {Input} values", input.Length);
        
        var (updates, comparer) = Parse(input);
        
        // take all the pages that are not ordered correctly, sort the incorrect pages, and return the sum of the middle elements in the newly sorted list
        return updates
            .Where(p => !Sorted(p, comparer)) // take not sorted
            .Select(p => p.OrderBy(a => a, comparer).ToArray()) // sort using comparer function
            .Sum(GetMiddlePage); // sum of middle elements
    }

    bool Sorted(string[] pages, Comparer<string> comparer) => Enumerable.SequenceEqual(pages, pages.OrderBy(x => x, comparer));
    
    int GetMiddlePage(string[] nums) => int.Parse(nums[nums.Length / 2]);

    (string[][] updates, Comparer<string>) Parse(string[] input)
    {
        // ordering rules like 123|456
        var ordering = input.TakeWhile(l => l.Contains("|")).ToHashSet();
        
        // the pages to update
        var updates = input.SkipWhile(l => l.Contains("|")).SkipWhile(string.IsNullOrEmpty).Select(l => l.Split(",")).ToArray();
        
        // if we want to order 123 and 456, check if we have 123|456, in the page ordering rules
        // then 123 should be before 456
        var comparer = Comparer<string>.Create((p1, p2) => ordering.Contains(p1 + "|" + p2) ? -1 : 1);

        return (updates, comparer);
    }
}
