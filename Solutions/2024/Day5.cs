using Helpers;

namespace Solutions.Year2024;

public class Day5(int year, int day) : Day(year, day)
{
    static IList<IList<int>> DoPermute(int[] nums, int start, int end, IList<IList<int>> list)
    {
        if (start == end)
        {
            list.Add(new List<int>(nums));
        }
        else
        {
            for (var i = start; i <= end; i++)
            {
                Swap(ref nums[start], ref nums[i]);
                DoPermute(nums, start + 1, end, list);
                Swap(ref nums[start], ref nums[i]);
            }
        }

        return list;
    }

    static void Swap(ref int a, ref int b)
    {
        (b, a) = (a, b);
    }

    static Comparer<int> GetComparer(Dictionary<int, List<int>> pagesAfter)
    {
        int Compare(int before, int after)
        {
            if (pagesAfter.TryGetValue(before, out var afterList))
                if (afterList.Contains(after))
                    return -1;
            return 1;
        }
        return Comparer<int>.Create((a, b) => Compare(a, b));
    }

    (List<int[]> updates, Dictionary<int, List<int>> pagesAfter) ParseInput()
    {
        Dictionary<int, List<int>> pagesAfter = new();
        List<int[]> updates = [];

        foreach (string line in SplitInput)
        {
            if (string.IsNullOrWhiteSpace(line))
                continue;

            if (line.Contains('|'))
            {
                var split = line.Split('|');
                int before = int.Parse(split[0]);
                int after = int.Parse(split[1]);

                if (pagesAfter.TryGetValue(before, out var pagesAfterList))
                    pagesAfterList.Add(after);
                else
                    pagesAfter.Add(before, [after]);
            }
            else if (line.Contains(','))
            {
                updates.Add(line.Split(',').Select(int.Parse).ToArray());
            }
        }

        return (updates, pagesAfter);
    }

    public override string PartOne()
    {
        var (updates, pagesAfter) = ParseInput();
        var comparer = GetComparer(pagesAfter);

        int total = 0;

        foreach (var update in updates)
        {
            if (update.SequenceEqual(update.OrderBy(p => p, comparer)))
            {
                total += update[update.Length / 2];
            }
        }

        return total.ToString();
    }

    public override string PartTwo()
    {
        var (updates, pagesAfter) = ParseInput();
        var comparer = GetComparer(pagesAfter);

        int total = 0;

        foreach (var update in updates)
        {
            var correctUpdate = update.OrderBy(p => p, comparer).ToArray();
            if (!update.SequenceEqual(correctUpdate))
            {
                total += correctUpdate[correctUpdate.Length / 2];
            }
        }

        return total.ToString();
    }
}