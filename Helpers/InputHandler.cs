using Microsoft.Extensions.Configuration;
using System.Net;

namespace Helpers;

public static class InputHandler
{

    public static async Task<string> GetInputAsync(int day)
    {
        string inputFolder = Path.Combine(Directory.GetCurrentDirectory(), "Input", day.ToString());
        string inputPath = Path.Combine(inputFolder, $"day_{day}_input.txt");
        if (File.Exists(inputPath))
            return File.ReadAllText(inputPath);
        Directory.CreateDirectory(inputFolder);
        try
        {
            Uri uri = new("https://adventofcode.com");
            IConfiguration config = new ConfigurationBuilder().AddJsonFile("appSettings.json").Build();
            Cookie cookie = new("session", config.GetSection("session").Value) { Domain = uri.Host };
            CookieContainer cookies = new();
            cookies.Add(cookie);
            using HttpClientHandler handler = new() { CookieContainer = cookies };
            using HttpClient client = new(handler) { BaseAddress = uri };
            string input = await client.GetStringAsync($"/2015/day/{day}/input");
            File.WriteAllText(inputPath, input);
            return input;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.InnerException?.Message ?? ex.Message);
            return string.Empty;
        }
    }

    public static string GetTestInput(int day)
    {
        string testInputPath = Path.Combine(Directory.GetCurrentDirectory(), "Input", day.ToString(), $"day_{day}_test_input.txt");
        if (File.Exists(testInputPath))
            return File.ReadAllText(testInputPath);
        return "Test input not found.";
    }

    public static void WriteTestInput(string input, int day)
    {
        string testInputPath = Path.Combine(Directory.GetCurrentDirectory(), "Input", day.ToString(), $"day_{day}_test_input.txt");
        File.WriteAllText(testInputPath, input);
    }

}