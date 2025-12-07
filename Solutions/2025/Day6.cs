using Helpers;

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
        long total = 0L;
        List<int> numbers = [];

        for (int i = SplitInput[0].Length - 1; i > -1; i--)
        {
            if (SplitInput.All(line => line[i] == ' '))
                continue;

            char op = SplitInput.Last()[i];
            int number = int.Parse(SplitInput.Select(line => line[i]).Where(char.IsNumber).ConcatChars());
            numbers.Add(number);
            if (op == '*')
            {
                total += numbers.Aggregate(1L, (acc, val) => acc * val);
                numbers.Clear();
            }
            else if (op == '+')
            {
                total += numbers.Sum();
                numbers.Clear();
            }
        }

        return total.ToString();
    }

}