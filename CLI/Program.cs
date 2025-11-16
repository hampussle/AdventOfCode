using CLI.Commands;
using Spectre.Console.Cli;

var app = new CommandApp();

app.Configure(config =>
{
    config.AddCommand<RunCommand>("run")
        .WithDescription("Run the specified day's solution.")
        .WithExample(["run", "2024", "1"])
        .WithExample(["run", "2024", "1", "--test"])
        .WithExample(["run", "2024", "1", "-t"]);
    config.AddCommand<SetCommand>("set")
        .WithDescription("Configure working directory, test inputs and aoc api-key.");
    config.AddCommand<TemplateCommand>("template")
        .WithDescription("Generate template project for the specified year.");
    //config.PropagateExceptions();
});

Environment.ExitCode = await app.RunAsync(args);

// TODO:
// grid for "run" command output
// elapsed time
// template should add project file