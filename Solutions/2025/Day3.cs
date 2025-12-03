using Helpers;
using System.Numerics;

namespace Solutions.Year2025;

public class Day3(int year, int day) : Day(year, day)
{

    public override string PartOne()
    {
        long total = 0;
        foreach (var line in SplitInput)
        {
            int largest = 0;
            int secondLargest = 0;
            for (int i = 0; i < line.Length; i++)
            {
                char c = line[i];
                var battery = c - '0';
                if (battery > largest && i != line.Length - 1)
                {
                    largest = battery;
                    secondLargest = 0;
                    continue;
                }
                if (battery > secondLargest)
                {
                    secondLargest = battery;
                }
            }
            var combined = largest.ToString() + secondLargest.ToString();
            //Console.WriteLine(combined);
            total += long.Parse(combined);
        }
        return total.ToString();
    }

    public override string PartTwo()
    {
        BigInteger total = 0;
        foreach (var line in SplitInput)
        {
            List<int> batteries = [];
            var batteriesToTake = 12;
            var lastBatteryTakenIdx = -1;
            while (batteriesToTake > 0)
            {
                var bestBattery =
                    line
                    .Substring(lastBatteryTakenIdx + 1, line.Length - lastBatteryTakenIdx - batteriesToTake)
                    .Select(c => c - '0')
                    .Max();
                batteries.Add(bestBattery);
                lastBatteryTakenIdx = line.IndexOf((char)(bestBattery + '0'), lastBatteryTakenIdx + 1);
                batteriesToTake--;
            }
            var largestNumber = BigInteger.Parse(string.Join("", batteries));
            //Console.WriteLine(largestNumber.ToString());
            total += largestNumber;
        }
        return total.ToString();
    }
}