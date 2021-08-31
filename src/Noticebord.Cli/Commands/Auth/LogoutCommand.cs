using System.IO;
using System;
using Noticebord.Cli.Settings.Auth;
using Spectre.Console;
using Spectre.Console.Cli;
using System.Diagnostics.CodeAnalysis;

namespace Noticebord.Cli.Commands.Auth
{
    public class LogoutCommand : Command<LogoutSettings>
    {
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
    }
}