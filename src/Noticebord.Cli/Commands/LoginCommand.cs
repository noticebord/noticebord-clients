using System.IO;
using System.Net.Mail;
using System;
using System.Threading.Tasks;
using Noticebord.Cli.Settings;
using Noticebord.Client;
using Spectre.Console;
using Spectre.Console.Cli;
using Noticebord.Client.Models;
using System.Diagnostics.CodeAnalysis;

namespace Noticebord.Cli.Commands
{
    public class LoginCommand : AsyncCommand<LoginSettings>
    {
        private readonly IClient _client;

        public LoginCommand(IClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public override async Task<int> ExecuteAsync([NotNull]CommandContext context, [NotNull]LoginSettings settings)
        {
            AuthenticateRequest request = !settings.Interactive
                ? new(settings.Email!, settings.Password!, Environment.MachineName)
                : new(
                    AnsiConsole.Prompt(new TextPrompt<string>("Enter your email address:")
                        .Validate(email => IsValidEmail(email), "[red]Email address is invalid[/]")),
                    AnsiConsole.Prompt(new TextPrompt<string>("Enter your password:").Secret()),
                    Environment.MachineName
                );

            var token = await AnsiConsole.Status()
                .StartAsync("Authorizing...",
                    async ctx => await _client.AuthenticateAsync(request));

            var path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            path = Path.Combine(path, "noticebord");
            Directory.CreateDirectory(path);
            path = Path.Combine(path, "token.txt");
            await File.WriteAllTextAsync(path, token);

            AnsiConsole.MarkupLine($"Logged in as [bold yellow]{request.Email}.[/]");
            return 0;
        }

        public override ValidationResult Validate([NotNull]CommandContext context, [NotNull]LoginSettings settings)
        {
            if (settings.Interactive)
            {
                if (!string.IsNullOrWhiteSpace(settings.Email) ||
                    !string.IsNullOrWhiteSpace(settings.Password))
                    return ValidationResult.Error("Credentials cannot be specified in interactive mode.");

                return base.Validate(context, settings);
            }

            if (string.IsNullOrWhiteSpace(settings.Email))
                return ValidationResult.Error("Email address must be provided in non-interactive mode.");

            if (!IsValidEmail(settings.Email))
                return ValidationResult.Error("Email address is invalid.");

            if (string.IsNullOrWhiteSpace(settings.Password))
                return ValidationResult.Error("Password must be provided in non-interactive mode.");

            return base.Validate(context, settings);
        }

        private static bool IsValidEmail(string text)
        {
            try
            {
                var email = new MailAddress(text);
                return email.Address == text;
            }
            catch
            {
                return false;
            }
        }
    }
}