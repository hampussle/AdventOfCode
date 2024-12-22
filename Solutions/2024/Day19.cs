using Helpers;

namespace Solutions.Year2024;

public class Day19(int year, int day) : Day(year, day)
{

    Dictionary<string, bool> IsPossibleMap = [];
    Dictionary<string, long> IsPossibleCount = [];

    bool IsPossible(string desiredPattern, string currentPattern, string next, string[] availablePatterns)
    {
        string nextPattern = currentPattern + next;

        if (!desiredPattern.StartsWith(nextPattern))
            return false;

        if (desiredPattern == nextPattern)
            return true;

        if (IsPossibleMap.TryGetValue(nextPattern, out bool possible))
        {
            return possible;
        }

        bool isPossible = availablePatterns.Any(next => IsPossible(desiredPattern, nextPattern, next, availablePatterns));
        IsPossibleMap[nextPattern] = isPossible;
        return isPossible;
    }

    long Ways(string desiredPattern, string[] availablePatterns)
    {
        if (IsPossibleCount.TryGetValue(desiredPattern, out long count))
            return count;

        long answer = 0;

        if (desiredPattern.Length == 0)
            return 1;

        foreach (string a in availablePatterns)
        {
            if (desiredPattern.StartsWith(a))
            {
                answer += Ways(desiredPattern[a.Length..], availablePatterns);
                //answer += Ways(desiredPattern.Skip(a.Length).ConcatChars(), availablePatterns);
            }
        }

        IsPossibleCount[desiredPattern] = answer;

        return answer;
    }

    public override string PartOne()
    {
        string[] availablePatterns = SplitInput[0].RemoveWhitespace().Split(',');

        List<string> desiredPatterns = [];
        int total = 0;

        foreach (string line in SplitInput.Skip(1))
        {
            desiredPatterns.Add(line);
            if (IsPossible(line, string.Empty, string.Empty, availablePatterns))
            {
                total++;
            }
            IsPossibleMap.Clear();
        }

        return total.ToString();
    }

    public override string PartTwo()
    {
        string[] availablePatterns = SplitInput[0].Split(", ");

        List<string> desiredPatterns = [];
        long total = 0;

        foreach (string line in SplitInput)
        {
            if (line.Contains(',') || string.IsNullOrWhiteSpace(line))
                continue;
            desiredPatterns.Add(line);
            long possible = Ways(line, availablePatterns);
            total += possible;

            Console.WriteLine($"{line} has {possible} possible ways. total: {total}");
        }

        return total.ToString();
    }

}