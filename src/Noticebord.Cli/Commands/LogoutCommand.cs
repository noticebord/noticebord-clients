using System.IO;
using System;
using Noticebord.Cli.Settings;
using Noticebord.Client;
using Spectre.Console;
using Spectre.Console.Cli;
using System.Diagnostics.CodeAnalysis;

namespace Noticebord.Cli.Commands
{
    public class LogoutCommand : Command<LogoutSettings>
    {
        private readonly IClient _client;
        public LogoutCommand(IClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        private readonly string _path = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "noticebord",
                "token.txt"
            );

        public override int Execute([NotNull] CommandContext context, [NotNull] LogoutSettings settings)
        {
            File.Delete(_path);
            AnsiConsole.MarkupLine($"Logged out.");
            return 0;
        }

        public override ValidationResult Validate([NotNull] CommandContext context, [NotNull] LogoutSettings settings)
        {
            if (!_client.IsAuthenticated)
                return ValidationResult.Error("You must be logged in to perform this action.");

            return base.Validate(context, settings);
        }
    }
}