using System;
using System.IO;
using System.Threading.Tasks;
using Noticebord.Cli.Settings;
using Noticebord.Client;
using Spectre.Console;
using Spectre.Console.Cli;

namespace Noticebord.Cli.Commands
{
    public class CreateNoticeCommand : AsyncCommand<CreateNoticeSettings>
    {
        private readonly IClient _client;
        private readonly string _path = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), 
            "noticebord", 
            "token.txt");

        public CreateNoticeCommand(IClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public override async Task<int> ExecuteAsync(CommandContext context, CreateNoticeSettings settings)
        {
            var title = settings.Title;
            var body = settings.Body;

            if (settings.Interactive)
            {
                title = AnsiConsole.Ask<string>("Enter a title for this notice:");
                body = AnsiConsole.Ask<string>("Enter the body for this notice:");
            }

            var token = await File.ReadAllTextAsync(_path);

            var notice = await AnsiConsole.Status()
                .StartAsync("Creating...", async ctx => await _client.CreateNoticeAsync(title, body, token));

            AnsiConsole.MarkupLine($"Notice [bold yellow]#{notice.Id} - {notice.Title}[/] was created.");
            return 0;
        }

        public override ValidationResult Validate(CommandContext context, CreateNoticeSettings settings)
        {
            if (!File.Exists(_path))
                return ValidationResult.Error("You must be logged in to perform this action.");

            if (settings.Interactive)
            {
                if (!string.IsNullOrWhiteSpace(settings.Title) ||
                    !string.IsNullOrWhiteSpace(settings.Body))
                    return ValidationResult.Error("Notice detail options cannot be specified in interactive mode.");

                return base.Validate(context, settings);
            }

            if (string.IsNullOrWhiteSpace(settings.Title))
                return ValidationResult.Error("Notice title must be provided in non-interactive mode.");

            if (string.IsNullOrWhiteSpace(settings.Body))
                return ValidationResult.Error("Notice body must be provided in non-interactive mode.");

            return base.Validate(context, settings);
        }
    }
}