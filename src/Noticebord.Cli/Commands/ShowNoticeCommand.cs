using System;
using System.Threading.Tasks;
using Noticebord.Cli.Settings;
using Noticebord.Cli.Utils;
using Noticebord.Client;
using Spectre.Console;
using Spectre.Console.Cli;

namespace Noticebord.Cli.Commands
{
    public class ShowNoticeCommand : AsyncCommand<ShowNoticeSettings>
    {
        private readonly IClient _client;
        public ShowNoticeCommand(IClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public override async Task<int> ExecuteAsync(CommandContext context, ShowNoticeSettings settings)
        {
            var notice = await AnsiConsole.Status()
                .StartAsync("Fetching...", async ctx =>
                {
                    var data = await _client.GetNoticeAsync(settings.Notice);
                    return Notices.AssignDefaultAuthor(data);
                });

            AnsiConsole.MarkupLine($"[bold yellow]{notice.Title}[/]");
            AnsiConsole.MarkupLine($"By [bold yellow]{notice.Author.Name}[/]");
            AnsiConsole.MarkupLine($"Created at {notice.CreatedAt}");
            AnsiConsole.MarkupLine($"Updated at {notice.UpdatedAt}");
            AnsiConsole.MarkupLine(string.Empty);
            AnsiConsole.MarkupLine(notice.Body);
            return 0;
        }
    }
}