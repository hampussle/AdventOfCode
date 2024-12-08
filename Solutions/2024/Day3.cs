using Helpers;
using System.Text;
using System.Text.RegularExpressions;

namespace Solutions.Year2024;

public class Day3(int year, int day) : Day(year, day)
{
    private readonly Regex MultRegex = new("mul\\(([0-9]+),([0-9]+)\\)");

    public override string PartOne()
    {
        int total = 0;

        var multiplications = MultRegex.Matches(Input);
        foreach (Match match in multiplications)
        {
            total += int.Parse(match.Groups[1].Value) * int.Parse(match.Groups[2].Value);
        }

        return total.ToString();
    }

    public override string PartTwo()
    {
        StringBuilder inputBuilder = new();
        bool isEnabled = true;

        for (int i = 0; i < Input.Length; i++)
        {
            char c = Input[i];
            string currStr = Input.Skip(i).ConcatChars();

            if (currStr.StartsWith("don't()"))
                isEnabled = false;
            else if (currStr.StartsWith("do()"))
                isEnabled = true;

            if (isEnabled)
                inputBuilder.Append(c);
        }

        string input = inputBuilder.ToString();

        int total = 0;

        var multiplications = MultRegex.Matches(input);
        foreach (Match match in multiplications)
        {
            total += int.Parse(match.Groups[1].Value) * int.Parse(match.Groups[2].Value);
        }

        return total.ToString();
    }

}