using Helpers;
using System.Runtime.CompilerServices;

namespace Solutions.Year2025;

public class Day6(int year, int day) : Day(year, day)
{

    public override string PartOne()
    {
        var normalizedInput = SplitInput.Select(line => line.NormalizeWhitespace().Replace(" ", ",")).ToArray();
        var operators = normalizedInput.Last().Split(',', StringSplitOptions.RemoveEmptyEntries);
        normalizedInput = [.. normalizedInput.Take(normalizedInput.Length - 1)];
        var numberRows = normalizedInput.Select(line => line.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray()).ToArray();

        long total = 0L;
        var maxCol = numberRows.Max(row => row.Length);
        for (int col = 0; col < maxCol; col++)
        {
            var op = operators[col];
            long mul = 1L;
            long add = 0L;
            foreach (var row in numberRows)
            {
                if (op == "*")
                    mul *= row[col];
                else
                    add += row[col];
            }
            if (op == "*")
                total += mul;
            else
                total += add;
        }

        return total.ToString();
    }

    public override string PartTwo()
    {
        List<List<int>> numbers = [];
        int outerColumn = 0;
        numbers.Add([]);
        for (int col = SplitInput.Max(line => line.Length) - 1; col >= 0; col--)
        {
            if (SplitInput.All(line => line.Length <= col || line[col] == ' '))
            {

            }
            string number = "";
            for (int row = 0; row < SplitInput.Length; row++)
            {
                string? line = SplitInput[row];
                if (line.Length <= col)
                    continue;
                var c = line[col];
                if (c == ' ')
                    continue;
                numbers[row].Add(int.Parse(c.ToString()));
            }
            if (!string.IsNullOrWhiteSpace(number))
                numbers[outerColumn].Add(int.Parse(number));
        }
    }

}