using Microsoft.Extensions.Configuration;
using System.Net;

namespace Helpers;

public static class InputHandler
{
    public static readonly string BasePath = Directory.GetParent(Environment.CurrentDirectory)!.Parent!.Parent!.Parent!.FullName;
    private static readonly string InputPath = Path.Combine(BasePath, "Input");

    public static async Task<string> GetInputAsync(int year, int day)
    {
        string inputPath = Path.Combine(InputPath, year.ToString(), day.ToString(), $"day_{day}_input.txt");
        if (File.Exists(inputPath))
            return File.ReadAllText(inputPath);

        Directory.CreateDirectory(InputPath);
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
        string testInputPath = Path.Combine(InputPath, year.ToString(), day.ToString(), $"day_{day}_test_input.txt");
        if (File.Exists(testInputPath))
            return File.ReadAllText(testInputPath);
        return "Test input not found.";
    }

    public static void WriteTestInput(string input, int year, int day)
    {
        string dayPath = Path.Combine(InputPath, year.ToString(), day.ToString());
        Directory.CreateDirectory(dayPath);

        string testInputPath = Path.Combine(dayPath, $"day_{day}_test_input.txt");
        File.WriteAllText(testInputPath, input);
    }
}