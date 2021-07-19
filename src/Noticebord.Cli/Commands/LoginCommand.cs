using System.Net;
using System.IO;
using System.Net.Mail;
using System;
using System.Threading.Tasks;
using Noticebord.Cli.Settings;
using Noticebord.Client;
using Spectre.Console;
using Spectre.Console.Cli;
using System.Text.Json;

namespace Noticebord.Cli.Commands
{
    public class LoginCommand : AsyncCommand<LoginSettings>
    {
        private readonly IClient _client;
        public LoginCommand(IClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public override async Task<int> ExecuteAsync(CommandContext context, LoginSettings settings)
        {
            var username = settings.Username;
            var password = settings.Password;
            var deviceName = settings.DeviceName;

            if (settings.Interactive)
            {
                username = AnsiConsole.Prompt(new TextPrompt<string>("Enter your username:")
                    .Validate(username => IsValidEmail(username),
                        "[red]Username must be a valid email address[/]"));
                password = AnsiConsole.Prompt(new TextPrompt<string>("Enter your password:").Secret());
                deviceName = AnsiConsole.Ask<string>("Enter a name for this device:", Environment.MachineName);
            }

            var token = await AnsiConsole.Status()
                .StartAsync("Authorizing...",
                    async ctx => await _client.AuthorizeAsync(username, password, deviceName));

            var path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            path = Path.Combine(path, "noticebord");
            Directory.CreateDirectory(path);
            path = Path.Combine(path, "token.txt");
            await File.WriteAllTextAsync(path, token);

            AnsiConsole.Markup($"Logged in as [bold yellow]{username}.[/]");
            return 0;
        }

        public override ValidationResult Validate(CommandContext context, LoginSettings settings)
        {
            if (settings.Interactive)
            {
                if (!string.IsNullOrWhiteSpace(settings.Username) ||
                    !string.IsNullOrWhiteSpace(settings.Password) ||
                    !string.IsNullOrWhiteSpace(settings.DeviceName))
                    return ValidationResult.Error("Credentials cannot be specified in interactive mode.");

                return base.Validate(context, settings);
            }

            if (string.IsNullOrWhiteSpace(settings.Username))
                return ValidationResult.Error("Username must provided in non-interactive mode.");

            if (!IsValidEmail(settings.Username))
                return ValidationResult.Error("Username must be a valid email address.");

            if (string.IsNullOrWhiteSpace(settings.Password))
                return ValidationResult.Error("Password must provided in non-interactive mode.");

            if (string.IsNullOrWhiteSpace(settings.DeviceName))
                return ValidationResult.Error("Device name must provided in non-interactive mode.");

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