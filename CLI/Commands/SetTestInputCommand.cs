using Helpers;
using Spectre.Console.Cli;
using System.ComponentModel;

namespace CLI.Commands;

internal class SetTestInputCommand : Command<SetTestInputCommand.Settings>
{
    public sealed class Settings : CommandSettings
    {
        [Description($"Specify the year (2015-2025)")]
        [CommandArgument(0, "<year>")]
        public int Year { get; init; }

        [Description($"Specify the day (1-25)")]
        [CommandArgument(1, "<day>")]
        public int Day { get; init; }

        [Description("Set the test input to use when using command <run -t>")]
        [CommandArgument(0, "<testInput>")]
        public string? TestInput { get; init; }
    }

    public override int Execute(CommandContext context, Settings settings, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(settings.TestInput);
        InputHandler.WriteTestInput(settings.TestInput, settings.Year, settings.Day);
        return 0;
    }
}
