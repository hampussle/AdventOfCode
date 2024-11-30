using Helpers;
using System.Diagnostics;

string? input = "";
while (input != "exit")
{
    int year = AskForYear();
    int day = AskForDay();

    Day? dayInstance = DayProvider.GetDay(year, day);
    if (dayInstance is null)
        continue;

    NewFrame(year, day);
    DisplayInstructions();

    while (input != "exit")
    {
        input = Console.ReadLine()?.ToLower();
        NewFrame(year, day);

        switch (input)
        {
            case "help":
                DisplayInstructions();
                break;
            case "test":
                RunPartOne(true);
                RunPartTwo(true);
                break;
            case "run":
                RunPartOne(false);
                RunPartTwo(false);
                break;
            case "test 1":
                RunPartOne(true);
                break;
            case "test 2":
                RunPartTwo(true);
                break;
            case "run 1":
                RunPartOne(false);
                break;
            case "run 2":
                RunPartTwo(false);
                break;
            case "input":
                PrintInput(false);
                break;
            case "testinput":
                PrintInput(true);
                break;
            case "testwrite":
                WriteInput(year, day);
                break;
            default:
                break;
        }
    }

    void RunPartOne(bool useTestInput)
    {
        dayInstance.UseTestInput = useTestInput;
        Stopwatch sw = Stopwatch.StartNew();
        string answer = dayInstance.PartOne();
        sw.Stop();
        Console.WriteLine("\nPart one answer found in " + sw.ElapsedMilliseconds + " ms");
        Console.WriteLine($"Part one answer: {answer}\n");
    }

    void RunPartTwo(bool useTestInput)
    {
        dayInstance.UseTestInput = useTestInput;
        Stopwatch sw = Stopwatch.StartNew();
        string answer = dayInstance.PartTwo();
        sw.Stop();
        Console.WriteLine("\nPart two answer found in " + sw.ElapsedMilliseconds + " ms");
        Console.WriteLine($"Part two answer: {answer}\n");
    }

    void PrintInput(bool useTestInput)
    {
        dayInstance.UseTestInput = useTestInput;
        Console.WriteLine(dayInstance.Input);
    }
}

void WriteInput(int year, int day)
{
    Console.WriteLine("Press 'Ctrl + Z' then ENTER to save.");
    Console.WriteLine("Test input:");
    string testInput = Console.In.ReadToEnd();
    if (string.IsNullOrWhiteSpace(testInput)) return;
    InputHandler.WriteTestInput(testInput, year, day);
}

void EmptyFrame()
{
    Console.Clear();
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine(" ADVENT OF CODE");
    Console.ForegroundColor = ConsoleColor.White;
}

void NewFrame(int year, int day)
{
    Console.Clear();
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine($" ADVENT OF CODE - {year}");
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine($" DAY {day}");
    Console.ForegroundColor = ConsoleColor.White;
}

int AskForYear()
{
    int year;
    while (!int.TryParse(input, out year) || year < 2015 || year > 2024)
    {
        EmptyFrame();
        Console.WriteLine(" What year?");
        input = Console.ReadLine()?.ToLower();
    }
    return year;
}

int AskForDay()
{
    int day;
    while (!int.TryParse(input, out day) || day < 1 || day > 25)
    {
        EmptyFrame();
        Console.WriteLine(" What day?");
        input = Console.ReadLine()?.ToLower();
    }
    return day;
}

void DisplayInstructions()
{
    Console.WriteLine();
    Console.WriteLine(" * TEST to run solution for parts 1 & 2 using the test input.");
    Console.WriteLine(" * TEST {1 or 2} to run solution for selected part using the test input.");
    Console.WriteLine(" * RUN to run solution for parts 1 & 2 using the actual input.");
    Console.WriteLine(" * RUN {1 or 2} to get solution for selected part using the actual input.");
    Console.WriteLine();
    Console.WriteLine(" * INPUT to see actual input from adventofcode.com.");
    Console.WriteLine(" * TESTINPUT to view saved test input.");
    Console.WriteLine(" * TESTWRITE to save test input.");
}