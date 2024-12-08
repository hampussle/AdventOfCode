using Helpers;

namespace Solutions.Year2024;

public class Day1(int year, int day) : Day(year, day)
{

    public override string PartOne()
    {
        List<int> first = [];
        List<int> second = [];

        foreach (string line in SplitInput)
        {
            string[] parts = line.Split("   ");
            first.Add(int.Parse(parts[0]));
            second.Add(int.Parse(parts[1]));
        }

        first.Sort();
        second.Sort();

        int total = 0;
        for (int i = 0; i < first.Count; i++)
        {
            int distance = Math.Abs(first[i] - second[i]);
            total += distance;
        }

        return total.ToString();
    }

    public override string PartTwo()
    {
        List<int> first = [];
        List<int> second = [];

        foreach (string line in SplitInput)
        {
            string[] parts = line.Split("   ");
            first.Add(int.Parse(parts[0]));
            second.Add(int.Parse(parts[1]));
        }

        first.Sort();
        second.Sort();

        int total = 0;
        for (int i = 0; i < first.Count; i++)
        {
            int x = first[i];
            int similarity = second.Count(j => x == j) * x;
            total += similarity;
        }

        return total.ToString();
    }

}