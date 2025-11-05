using ConsoleApp;
using Spectre.Console.Cli;
using System.ComponentModel;

namespace CLI.Commands;

internal class SetApiKeyCommand : Command<SetApiKeyCommand.Settings>
{
    public sealed class Settings : CommandSettings
    {
        [Description("Set the API key used for fetching input data.")]
        [CommandArgument(0, "<apiKey>")]
        public string? ApiKey { get; init; }
    }

    public override int Execute(CommandContext context, Settings settings, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(settings.ApiKey);
        ConsoleHandler.SetApiKey(settings.ApiKey);
        return 0;
    }
}
