namespace Helpers;

public static class DayProvider
{
    public static Day? GetDay(int year, int day)
    {
        if (Type.GetType($"Solutions.Year{year}.Day{day}, Solutions") is Type dayType)
            if (Activator.CreateInstance(dayType, [year, day]) is Day dayInstance)
                return dayInstance;
        return null;
    }
}

public static class DaysGenerator
{
    public static void GenerateDays(int year)
    {
        string solutionsPath = Path.Combine(InputHandler.BasePath, "Solutions");
        string yearDirPath = Path.Combine(solutionsPath, year.ToString());
        Directory.CreateDirectory(yearDirPath);

        for (int day = 1; day < 26; day++)
        {
            string path = Path.Combine(yearDirPath, $"Day{day}.cs");
            if (File.Exists(path))
                continue;

            string contents = @$"using Helpers;

namespace Solutions.Year{year};

public class Day{day}(int year, int day) : Day(year, day)
{{

    public override string PartOne()
    {{
        return base.PartOne();
    }}

    public override string PartTwo()
    {{
        return base.PartTwo();
    }}

}}";

            File.WriteAllText(path, contents);
        }
    }
}
