using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Noticebord.Cli.Settings;
using Noticebord.Client;
using Noticebord.Client.Models;
using Spectre.Console;
using Spectre.Console.Cli;

namespace Noticebord.Cli.Commands
{
    public class CreateNoticeCommand : AsyncCommand<CreateNoticeSettings>
    {
        private readonly IClient _client;

        public CreateNoticeCommand(IClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public override async Task<int> ExecuteAsync([NotNull]CommandContext context, [NotNull] CreateNoticeSettings settings)
        {
            CreateNoticeRequest request = (!settings.Interactive) 
                ? new(settings.Title!, settings.Body!)
                : new(
                    AnsiConsole.Ask<string>("Enter a title for this notice:"),
                    AnsiConsole.Ask<string>("Enter the body for this notice:")
                );

            var notice = await AnsiConsole.Status()
                .StartAsync("Creating...", async ctx => await _client.CreateNoticeAsync(request));

            AnsiConsole.MarkupLine($"Notice [bold yellow]#{notice.Id} - {notice.Title}[/] was created.");
            return 0;
        }

        public override ValidationResult Validate([NotNull]CommandContext context, [NotNull]CreateNoticeSettings settings)
        {
            if (!_client.IsAuthenticated)
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