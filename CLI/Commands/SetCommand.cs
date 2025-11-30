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
        public FlagValue? ApiKey { get; set; }

        [Description("Set the test input to use when using command <run -t>")]
        [CommandOption("-t|--testinput [TESTINPUT]")]
        public FlagValue? TestInput { get; init; }

        [Description("Year (2015-2025)")]
        [CommandArgument(0, "[YEAR]")]
        public int? Year { get; init; }

        [Description("Day (1-25)")]
        [CommandArgument(1, "[DAY]")]
        public int? Day { get; init; }

        public override ValidationResult Validate()
        {
            if (ApiKey is null && TestInput is null)
                return ValidationResult.Error("Either --api or --testinput must be provided.");

            if (TestInput is not null)
                if (Year is null || Day is null)
                    return ValidationResult.Error("Year and day must be provided when setting test input.");

            return ValidationResult.Success();
        }
    }

    public override int Execute(CommandContext context, Settings settings, CancellationToken cancellationToken)
    {
        if (settings.ApiKey?.IsSet ?? false)
        {
            if (settings.ApiKey.Value is string apiKey)
            {
                var path = InputHandler.SetApiKey(apiKey);
                AnsiConsole.MarkupLine("[green]API key set[/]");
                AnsiConsole.WriteLine();
                AnsiConsole.MarkupLine($"[red]Key stored in:[/] {path}");
            }
        }

        if (settings.TestInput?.IsSet ?? false)
        {
            if (settings.TestInput?.Value is string testInput && settings.Year is int year && settings.Day is int day)
            {
                InputHandler.WriteTestInput(testInput, year, day);
                AnsiConsole.MarkupLine($"[green]Test input set for {year} - {day}.[/]");
            }
        }

        return 0;
    }
}

class FlagValue : IFlagValue
{
    public bool IsSet { get; set; }

    public Type Type => typeof(string);

    public object? Value { get; set; }
}