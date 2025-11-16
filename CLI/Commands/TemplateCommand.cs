using Helpers;
using Spectre.Console;
using Spectre.Console.Cli;
using System.ComponentModel;

namespace CLI.Commands;

internal class TemplateCommand : Command<TemplateCommand.Settings>
{
    public sealed class Settings : CommandSettings
    {
        [Description($"Specify the year (2015-2025)")]
        [CommandArgument(0, "<year>")]
        public int Year { get; init; }

        public override ValidationResult Validate()
        {
            return Constants.ValidYear(Year) ?
                ValidationResult.Success() :
                ValidationResult.Error($"Year must be between {Constants.MinYear} and {Constants.MaxYear}.");
        }
    }

    public override int Execute(CommandContext context, Settings settings, CancellationToken cancellationToken)
    {
        DaysGenerator.GenerateDays(settings.Year);
        return 0;
    }
}
