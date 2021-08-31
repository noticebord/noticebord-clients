using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Noticebord.Cli.Settings.Notices;
using Noticebord.Client;
using Noticebord.Client.Models;
using Spectre.Console;
using Spectre.Console.Cli;

namespace Noticebord.Cli.Commands.Notices
{
    public class UpdateNoticeCommand : AsyncCommand<UpdateNoticeSettings>
    {
        private readonly IClient _client;

        public UpdateNoticeCommand(IClient client) => _client = client;

        public override async Task<int> ExecuteAsync([NotNull] CommandContext context, [NotNull] UpdateNoticeSettings settings)
        {
            var request = new SaveNoticeRequest(
                Title: AnsiConsole.Ask<string>("Enter a new title for this notice:"),
                Body: AnsiConsole.Ask<string>("Enter the new body of this notice:"),
                Anonymous: true,
                Public: true
            );

            var anonymous = AnsiConsole.Confirm("Do you want to make this notice anonymous?");
            request = request with {
                Anonymous = anonymous,
                Public = anonymous || AnsiConsole.Confirm("Do you want to make this notice public?")
            };

            var notice = await AnsiConsole.Status()
                .StartAsync("Creating...", async ctx => await _client.UpdateNoticeAsync(settings.Notice, request));

            AnsiConsole.MarkupLine($"Notice [bold yellow]#{notice.Id} - {notice.Title}[/] was updated.");
            return 0;
        }
    }
}