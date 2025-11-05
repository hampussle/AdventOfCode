using Helpers;
using System.Diagnostics;

namespace ConsoleApp;

public static class ConsoleHandler
{
    public const int MinYear = 2015;
    public const int MaxYear = 2025;

    public static void TestPartOne(int year, int day)
        => PartOne(year, day, true);
    public static void RunPartOne(int year, int day)
        => PartOne(year, day, false);
    private static void PartOne(int year, int day, bool useTestInput)
    {
        var dayInstance = DayProvider.GetDay(year, day);
        ArgumentNullException.ThrowIfNull(dayInstance);
        dayInstance.UseTestInput = useTestInput;
        Stopwatch sw = Stopwatch.StartNew();
        string answer = dayInstance.PartOne();
        sw.Stop();
        Console.WriteLine($"\nPart one answer: {answer}\n");
        Console.WriteLine("\nPart one answer found in " + sw.ElapsedMilliseconds + " ms");
    }

    public static void TestPartTwo(int year, int day)
        => PartTwo(year, day, true);
    public static void RunPartTwo(int year, int day)
        => PartTwo(year, day, false);
    public static void PartTwo(int year, int day, bool useTestInput)
    {
        var dayInstance = DayProvider.GetDay(year, day);
        ArgumentNullException.ThrowIfNull(dayInstance);
        dayInstance.UseTestInput = useTestInput;
        Stopwatch sw = Stopwatch.StartNew();
        string answer = dayInstance.PartTwo();
        sw.Stop();
        Console.WriteLine($"\nPart two answer: {answer}\n");
        Console.WriteLine("\nPart two answer found in " + sw.ElapsedMilliseconds + " ms");
    }

    public static void PrintInput(int year, int day, bool useTestInput)
    {
        var dayInstance = DayProvider.GetDay(year, day);
        ArgumentNullException.ThrowIfNull(dayInstance);
        dayInstance.UseTestInput = useTestInput;
        Console.WriteLine(dayInstance.Input);
    }

    public static void WriteInput(int year, int day)
    {
        Console.WriteLine("Press 'Ctrl + Z' then ENTER to save.");
        Console.WriteLine("Test input:");
        string testInput = Console.In.ReadToEnd();
        if (string.IsNullOrWhiteSpace(testInput)) return;
        InputHandler.WriteTestInput(testInput, year, day);
    }

    public static void SetApiKey(string key)
    {
        string contents = @$"{{
    ""session"": ""{key}""
}}";
        string path = Path.Combine(Directory.GetCurrentDirectory(), "appSettings.json");
        File.WriteAllText(path, contents);
    }

    private static void EmptyFrame()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(" ADVENT OF CODE");
        Console.ForegroundColor = ConsoleColor.White;
    }

    private static void NewFrame(int year, int day)
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($" ADVENT OF CODE - {year}");
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($" DAY {day}");
        Console.ForegroundColor = ConsoleColor.White;
    }
}
