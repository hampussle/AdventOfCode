using Helpers;
using Spectre.Console;
using Spectre.Console.Cli;
using System.ComponentModel;

namespace CLI.Commands;

internal class SetCommand : Command<SetCommand.Settings>
{
    public sealed class Settings : CommandSettings
    {
        [Description("Set the API key used for fetching input.")]
        [CommandOption("-a|--api [APIKEY]")]
        public string? ApiKey { get; set; }

        [Description("Set the test input to use when using command <run -t>")]
        [CommandOption("-t|--testinput [TESTINPUT]")]
        public string? TestInput { get; init; }

        [Description("Year (2015-2025)")]
        [CommandArgument(0, "[YEAR]")]
        public int? Year { get; init; }

        [Description("Day (1-25)")]
        [CommandArgument(1, "[DAY}")]
        public int? Day { get; init; }

        [Description("Path to working directory")]
        [CommandOption("-d|--workingdirectory [PATH]")]
        public string? WorkingDirectory { get; init; }

        public override ValidationResult Validate()
        {
            if (ApiKey is null && TestInput is null && WorkingDirectory is null)
                return ValidationResult.Error("Either --api, --testinput, --homedir must be provided.");

            if (TestInput is not null)
                if (Year is null || Day is null)
                    return ValidationResult.Error("Year and day must be provided when setting test input.");

            return ValidationResult.Success();
        }
    }

    public override int Execute(CommandContext context, Settings settings, CancellationToken cancellationToken)
    {
        if (settings.ApiKey is string apiKey)
        {
            var path = InputHandler.SetApiKey(apiKey);
            AnsiConsole.MarkupLine("[green]API key set[/]");
            AnsiConsole.WriteLine();
            AnsiConsole.MarkupLine($"[red]Key stored in:[/] {path}");
        }

        if (settings.TestInput is not null && settings.Year is int year && settings.Day is int day)
        {
            InputHandler.WriteTestInput(settings.TestInput, year, day);
            AnsiConsole.MarkupLine($"[green]Test input set for {year} - {day}.[/]");
        }

        if (settings.WorkingDirectory is not null)
        {
            // TODO
        }

        return 0;
    }
}
