namespace Algorithms;

public static class BinarySearch
{
    /// <summary>
    /// Note: Must be sorted before calling this method.
    ///
    /// Time complexity: O(log n)
    /// </summary>
    public static int FindIndex<T>(Span<T> span, T target) where T : IComparable<T>
    {
        var left = 0;
        var right = span.Length - 1;

        while (left <= right)
        {
            var middle = left + (right - left) / 2;

            var comparison = span[middle].CompareTo(target);
            if (comparison == 0)
            {
                return middle;
            }

            if (comparison < 0)
            {
                left = middle + 1;
            }
            else
            {
                right = middle - 1;
            }
        }

        return -1;
    }
}