using Helpers;

namespace Solutions.Year2024;

public class Day2(int year, int day) : Day(year, day)
{
    static bool IsSafe(List<int> levels)
    {
        int last = levels[0];
        bool isIncrease = false;
        bool isDecrease = false;
        bool safe = true;

        for (int i = 1; i < levels.Count; i++)
        {
            int curr = levels[i];
            int diff = Math.Abs(curr - last);
            if (diff < 1 || diff > 3)
                safe = false;

            if (curr > last)
                isIncrease = true;
            else if (curr < last)
                isDecrease = true;

            last = curr;
        }

        if (isIncrease && isDecrease)
            safe = false;

        return safe;
    }

    public override string PartOne()
    {
        int total = 0;
        foreach (string line in SplitInput)
        {
            List<int> levels = line.Split(' ').Select(int.Parse).ToList();
            int last = levels[0];
            bool isIncrease = false;
            bool isDecrease = false;
            bool safe = true;

            for (int i = 1; i < levels.Count; i++)
            {
                int curr = levels[i];
                int diff = Math.Abs(curr - last);
                if (diff < 1 || diff > 3)
                    safe = false;

                if (curr > last)
                    isIncrease = true;
                else if (curr < last)
                    isDecrease = true;

                last = curr;
            }

            if (isIncrease && isDecrease)
                safe = false;

            if (safe)
                total++;
        }
        return total.ToString();
    }

    public override string PartTwo()
    {
        int total = 0;
        foreach (string line in SplitInput)
        {
            List<int> levels = line.Split(' ').Select(int.Parse).ToList();
            if (IsSafe(levels))
            {
                total++;
            }
            else
            {
                for (int i = 0; i < levels.Count; i++)
                {
                    int nr = levels[i];
                    levels.RemoveAt(i);
                    if (IsSafe(levels))
                    {
                        total++;
                        break;
                    }
                    levels.Insert(i, nr);
                }
            }
        }
        return total.ToString();
    }

}