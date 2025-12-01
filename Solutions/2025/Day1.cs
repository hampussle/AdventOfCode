using Helpers;

namespace Solutions.Year2025;

public class Day1(int year, int day) : Day(year, day)
{

    public override string PartOne()
    {
        int dial = 50;
        int hitZero = 0;
        foreach (var line in SplitInput)
        {
            bool isRight = line[0] == 'R';
            int steps = int.Parse(line[1..]);
            dial += isRight ? steps : -steps;
            while (dial < 0)
                dial += 100;
            while (dial > 99)
                dial -= 100;
            if (dial == 0)
                hitZero++;
        }
        return hitZero.ToString();
    }

    public override string PartTwo()
    {
        int dial = 50;
        int hitZero = 0;
        foreach (var line in SplitInput)
        {
            int steps = int.Parse(line[1..]);
            int step = line[0] == 'R' ? 1 : -1;
            for (int i = 0; i < steps; i++)
            {
                dial += step;
                if (dial == 100)
                    dial = 0;
                if (dial == -1)
                    dial = 99;
                if (dial == 0)
                    hitZero++;
            }
        }
        return hitZero.ToString();
    }

}