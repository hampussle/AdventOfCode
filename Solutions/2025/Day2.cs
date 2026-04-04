using Helpers;

namespace Solutions.Year2025;

public class Day2(int year, int day) : Day(year, day)
{

    public override string PartOne()
    {
        var ranges = Input.Split(',');
        long result = 0;

        foreach (var range in ranges)
        {
            var ids = range.Split('-');
            var start = long.Parse(ids[0]);
            var end = long.Parse(ids[1]);
            for (long i = start; i <= end; i++)
            {
                var nrStr = i.ToString();
                var firstPart = nrStr[..(nrStr.Length / 2)];
                var secondPart = nrStr[(nrStr.Length / 2)..];
                if (firstPart.Equals(secondPart))
                    result += i;
            }
        }

        return result.ToString();
    }

    public override string PartTwo()
    {
        var ranges = Input.Split(',');
        long result = 0;

        foreach (var range in ranges)
        {
            var ids = range.Split('-');
            var start = long.Parse(ids[0]);
            var end = long.Parse(ids[1]);

            for (long i = start; i <= end; i++)
            {
                var nrStr = i.ToString();
                string curr = "";
                foreach (var c in nrStr)
                {
                    curr += c.ToString();
                    if (curr.Length < nrStr.Length && string.IsNullOrWhiteSpace(nrStr.Replace(curr, string.Empty)))
                    {
                        result += i;
                        break;
                    }
                }
            }
        }

        return result.ToString();
    }

}