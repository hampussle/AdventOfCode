using Helpers;

namespace Solutions.Year2025;

public class Day5(int year, int day) : Day(year, day)
{

    public override string PartOne()
    {
        List<(long, long)> fresh = [];
        List<long> available = [];
        foreach (var line in SplitInput)
        {
            if (line.Contains('-'))
            {
                var numbers = line.Split('-');
                var start = long.Parse(numbers[0]);
                var end = long.Parse(numbers[1]);
                fresh.Add((start, end));
            } else
                available.Add(long.Parse(line));
        }

        long total = 0;
        foreach (var num in available)
            foreach (var (start, end) in fresh)
                if (num >= start && num <= end)
                {
                    total += 1;
                    break;
                }

        return total.ToString();
    }

    public override string PartTwo()
    {
        HashSet<(long, long)> fresh = [];
        foreach (var line in SplitInput)
        {
            if (line.Contains('-'))
            {
                var numbers = line.Split('-');
                var start = long.Parse(numbers[0]);
                var end = long.Parse(numbers[1]);
                fresh.Add((start, end));
            }
        }

        HashSet<(long, long)> cache = [];

        foreach (var (freshStart, freshEnd) in fresh.OrderBy(f => f.Item1))
        {
            bool merged = false;
            foreach (var (oldStart, oldEnd) in cache.ToHashSet())
            {
                if (!(freshEnd < oldStart || freshStart > oldEnd))
                {
                    cache.Remove((oldStart, oldEnd));
                    cache.Add((Math.Min(freshStart, oldStart), Math.Max(freshEnd, oldEnd)));
                    merged = true;
                }
            }

            if (!merged)
                cache.Add((freshStart, freshEnd));
        }

        long total = 0;
        foreach (var range in cache)
            total += range.Item2 - range.Item1 + 1;

        return total.ToString();
    }

}