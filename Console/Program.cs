using ConsoleApp;
using Helpers;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.CommandLine.Parsing;

var yearOption = new Option<int>("--year", "-y")
{
    Description = "Specify the year (2015-2025)",
    Required = true,
};
yearOption.AcceptOnlyFromAmong([.. Enumerable.Range(ConsoleHandler.MinYear, ConsoleHandler.MaxYear).Select(n => n.ToString())]);

var dayOption = new Option<int>("--day", "-d")
{
    Description = "Specify the day (1-25)",
    Required = true,
};
dayOption.AcceptOnlyFromAmong([.. Enumerable.Range(1, 25).Select(n => n.ToString())]);

var partOption = new Option<int?>("--part", "-p")
{
    Description = "Specify the part (1-2)",
};
partOption.AcceptOnlyFromAmong("1", "2");

var setInputOption = new Option<string>("--write", "-w")
{
    Description = "Set the test input",
};

var runCommand = new Command("run", "Runs the solution for the specified year and day")
{
    yearOption,
    dayOption,
    partOption,
};

var testCommand = new Command("test", "Runs the solution for the specified year and day on the test input")
{
    yearOption,
    dayOption,
    partOption,
};

var inputCommand = new Command("input", "Handle the test input for the specified year and day")
{
    yearOption,
    dayOption,
    setInputOption,
};

var generateCodeFilesCommand = new Command("template", "Generate code files for the specified year")
{
    yearOption,
};

testCommand.SetAction((parseResult) =>
{
    var year = parseResult.GetValue(yearOption);
    var day = parseResult.GetValue(dayOption);
    var part = parseResult.GetValue(partOption);

    if (part == 1)
        ConsoleHandler.TestPartOne(year, day);
    else if (part == 2)
        ConsoleHandler.TestPartTwo(year, day);
    else
    {
        ConsoleHandler.TestPartOne(year, day);
        ConsoleHandler.TestPartTwo(year, day);
    }
});

runCommand.SetAction((parseResult) =>
{
    var year = parseResult.GetValue(yearOption);
    var day = parseResult.GetValue(dayOption);
    var part = parseResult.GetValue(partOption);

    if (part == 1)
        ConsoleHandler.TestPartOne(year, day);
    else if (part == 2)
        ConsoleHandler.TestPartTwo(year, day);
    else
    {
        ConsoleHandler.TestPartOne(year, day);
        ConsoleHandler.TestPartTwo(year, day);
    }
});

inputCommand.SetAction((parseResult) =>
{
    var year = parseResult.GetValue(yearOption);
    var day = parseResult.GetValue(dayOption);
    var input = parseResult.GetValue(setInputOption);

    if (input is string str)
        InputHandler.WriteTestInput(str, year, day);
    else
        Console.WriteLine(InputHandler.GetTestInput(year, day));    
});

var root = new RootCommand("Advent of code CLI");

root.Subcommands.Add(runCommand);
root.Subcommands.Add(generateCodeFilesCommand);
root.Subcommands.Add(inputCommand);

var parsedResult = root.Parse(args);
await parsedResult.InvokeAsync();

foreach (ParseError parseError in parsedResult.Errors)
{
    Console.Error.WriteLine(parseError.Message);
}