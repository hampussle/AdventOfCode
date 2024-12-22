using Helpers;

namespace Solutions.Year2024;

public class Day11(int year, int day) : Day(year, day)
{

    bool IsEven(long x) => x.ToString().Length % 2 == 0;

    void IncrementKey(Dictionary<long, long> dict, long key, long value)
    {
        if (!dict.ContainsKey(key))
        {
            dict[key] = value;
        }
        else
        {
            dict[key] += value;
        }
    }

    public override string PartOne()
    {
        List<long> stonesList = Input.Split(' ').Select(long.Parse).ToList();
        Dictionary<int, long> stones = new();

        for (int i = 0; i < stonesList.Count; i++)
        {
            long stone = stonesList[i];
            stones[i] = stone;
        }

        for (int i = 0; i < 25; i++)
        {
            Dictionary<int, long> newStones = new(stones);
            int newStonesIdx = 0;
            for (int idx = 0; idx < stones.Count; idx++)
            {
                long stone = stones[idx];
                if (stone == 0)
                {
                    newStones[newStonesIdx] = 1;
                }
                else if (IsEven(stone))
                {
                    string stoneStr = stone.ToString();
                    newStones[newStonesIdx] = long.Parse(stoneStr.Substring(0, stoneStr.Length / 2));
                    newStones[newStonesIdx + 1] = long.Parse(stoneStr.Substring(stoneStr.Length / 2, stoneStr.Length / 2));
                    newStonesIdx++;
                }
                else
                {
                    newStones[newStonesIdx] = stone * 2024;
                }
                newStonesIdx++;
            }
            stones = newStones;
            //PrintStones(stones);
        }

        return stones.Count.ToString();
    }

    public override string PartTwo()
    {
        List<long> stonesList = Input.Split(' ').Select(long.Parse).ToList();
        Dictionary<long, long> stonesCount = new();

        for (int i = 0; i < stonesList.Count; i++)
        {
            long stone = stonesList[i];
            IncrementKey(stonesCount, stone, 1);
        }

        for (int i = 0; i < 75; i++)
        {
            Dictionary<long, long> newStones = new();

            foreach (var kvp in stonesCount)
            {
                long stone = kvp.Key;
                if (stone == 0)
                {
                    IncrementKey(newStones, 1, kvp.Value);
                }
                else if (IsEven(stone))
                {
                    string stoneStr = stone.ToString();
                    long firstHalf = long.Parse(stoneStr.Substring(0, stoneStr.Length / 2));
                    long secondHalf = long.Parse(stoneStr.Substring(stoneStr.Length / 2, stoneStr.Length / 2));
                    IncrementKey(newStones, firstHalf, kvp.Value);
                    IncrementKey(newStones, secondHalf, kvp.Value);
                }
                else
                {
                    IncrementKey(newStones, stone * 2024, kvp.Value);
                }
            }

            stonesCount = newStones;
            //PrintStones(stonesCount);
        }

        return stonesCount.Sum(kvp => kvp.Value).ToString();
    }

    void PrintStones(Dictionary<long, long> stones)
    {
        foreach (var stone in stones)
        {
            for (int i = 0; i < stone.Value; i++)
            {
                Console.Write(stone.Key + " ");
            }
        }
        Console.WriteLine();
    }

}