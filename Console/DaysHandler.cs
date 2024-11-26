using System.Runtime.CompilerServices;

namespace Helpers;

internal class DaysHandler
{

}

public static class DaysGenerator
{
    public static void GenerateDays([CallerFilePath] string? callerPath = null)
    {
        if (callerPath is null || Directory.GetParent(callerPath) is not DirectoryInfo sourcePath)
            return;
        string daysDirPath = Path.Combine(sourcePath.FullName, "Days");
        Directory.CreateDirectory(daysDirPath);
        for (int day = 1; day < 26; day++)
        {
            string path = Path.Combine(daysDirPath, $"Day{day}.cs");
            if (File.Exists(path))
                continue;
            string contents = @$"namespace Solutions;

public class Day{day}(int day) : Day(day)
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

    public static Day? GetDay(int day)
    {
        if (Type.GetType("AdventOfCode2015.Days.Day" + day) is Type dayType)
            if (Activator.CreateInstance(dayType, args: [day]) is Day dayInstance)
                return dayInstance;
        return null;
    }
}
