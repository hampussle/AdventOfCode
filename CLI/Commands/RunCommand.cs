using Helpers;
using Spectre.Console;
using Spectre.Console.Cli;
using System.ComponentModel;

namespace CLI.Commands;

internal class RunCommand : Command<RunCommand.Settings>
{
    public sealed class Settings : CommandSettings
    {
        [Description($"Specify the year (2015-2025)")]
        [CommandArgument(0, "<year>")]
        public int Year { get; init; }

        [Description($"Specify the day (1-25)")]
        [CommandArgument(1, "<day>")]
        public int Day { get; init; }

        [CommandOption("-t|--test")]
        [DefaultValue(false)]
        public bool UseTestInput { get; init; }

        public override ValidationResult Validate()
        {
            if (Constants.ValidYear(Year))
                return ValidationResult.Error($"Year must be between {Constants.MinYear} and {Constants.MaxYear}.");
            if (Constants.ValidDay(Day))
                return ValidationResult.Error($"Day must be between {Constants.MinDay} and {Constants.MaxDay}.");

            return ValidationResult.Success();
        }
    }

    public override int Execute(CommandContext context, Settings settings, CancellationToken cancellationToken)
    {
        var dayInstance = DayProvider.GetDay(settings.Year, settings.Day);
        ArgumentNullException.ThrowIfNull(dayInstance);
        dayInstance.UseTestInput = settings.UseTestInput;

        var sw = System.Diagnostics.Stopwatch.StartNew();
        string answer = dayInstance.PartOne();
        sw.Stop();
        
        AnsiConsole.MarkupLine($"\nPart one answer: {answer}\n");
        AnsiConsole.MarkupLine($"\nPart one answer found in [green]{sw.ElapsedMilliseconds} ms[/]");
        
        sw.Restart();
        answer = dayInstance.PartTwo();
        sw.Stop();
        
        AnsiConsole.MarkupLine($"\nPart two answer: {answer}\n");
        AnsiConsole.MarkupLine($"\nPart two answer found in [green]{sw.ElapsedMilliseconds} ms[/]");

        return 0;
    }
}
