using Microsoft.Extensions.Configuration;
using System.Net;

namespace Helpers;

public static class InputHandler
{
    public static readonly string BasePath = Directory.GetParent(Environment.CurrentDirectory)!.Parent!.Parent!.Parent!.FullName;
    public static readonly string CliPath = Directory.GetParent(Environment.CurrentDirectory)!.Parent!.Parent!.FullName;
    private static readonly string InputPath = Path.Combine(BasePath, "Input");
    private static string InputPathDir(int year, int day) => Path.Combine(InputPath, year.ToString(), day.ToString());

    public static async Task<string> GetInputAsync(int year, int day)
    {
        string inputPathDir = InputPathDir(year, day);
        Directory.CreateDirectory(inputPathDir);

        string inputPath = Path.Combine(inputPathDir, $"day_{day}_input.txt");
        if (File.Exists(inputPath))
            return File.ReadAllText(inputPath);

        try
        {
            Uri uri = new("https://adventofcode.com");
            IConfiguration config = new ConfigurationBuilder().AddJsonFile("appSettings.json").Build();
            Cookie cookie = new("session", config.GetSection("session").Value) { Domain = uri.Host };
            CookieContainer cookies = new();
            cookies.Add(cookie);
            using HttpClientHandler handler = new() { CookieContainer = cookies };
            using HttpClient client = new(handler) { BaseAddress = uri };
            string input = await client.GetStringAsync($"/{year}/day/{day}/input");
            File.WriteAllText(inputPath, input);
            return input;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.InnerException?.Message ?? ex.Message);
            return string.Empty;
        }
    }

    public static string GetTestInput(int year, int day)
    {
        string testInputPath = Path.Combine(InputPathDir(year, day), $"day_{day}_test_input.txt");
        return File.Exists(testInputPath) ? File.ReadAllText(testInputPath) : "Test input not found.";
    }

    public static void WriteTestInput(string input, int year, int day)
    {
        string inputPathDir = InputPathDir(year, day);
        Directory.CreateDirectory(inputPathDir);

        string testInputPath = Path.Combine(inputPathDir, $"day_{day}_test_input.txt");
        File.WriteAllText(testInputPath, input);
    }
}