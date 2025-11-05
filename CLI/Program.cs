using CLI.Commands;
using ConsoleApp;
using Helpers;
using Spectre.Console.Cli;

var app = new CommandApp();

app.Configure(config =>
{
    config.AddCommand<RunCommand>("run")
        .WithDescription("Run the specified day's solution.")
        .WithExample(["run", "2024", "1"])
        .WithExample(["run", "2024", "1", "--test"])
        .WithExample(["run", "2024", "1", "-t"]);
    config.AddCommand<SetApiKeyCommand>("set-api")
        .WithDescription("Set api key for fetching inputs.");
    config.PropagateExceptions();
});