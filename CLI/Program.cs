using CLI.Commands;
using Spectre.Console;
using Spectre.Console.Cli;

AnsiConsole.MarkupLine("[green]Hello world![/]");

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
    //config.PropagateExceptions();
});

Environment.ExitCode = await app.RunAsync(args);