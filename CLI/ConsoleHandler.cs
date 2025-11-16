using Helpers;
using Spectre.Console;

namespace ConsoleApp;

public static class ConsoleHandler
{
    public const int MinYear = 2015;
    public const int MaxYear = 2025;

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

    public static string SetApiKey(string key)
    {
        string contents = @$"{{
    ""session"": ""{key}""
}}";
        string path = Path.Combine(InputHandler.CliPath, "appSettings.json");
        File.WriteAllText(path, contents);
        AnsiConsole.MarkupLine("[green]API key saved successfully![/]");
        AnsiConsole.MarkupLine($"path to API key: {path}");
        return path;
    }
}
