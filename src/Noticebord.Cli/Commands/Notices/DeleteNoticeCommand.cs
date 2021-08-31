using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Noticebord.Cli.Settings.Notices;
using Noticebord.Client;
using Spectre.Console;
using Spectre.Console.Cli;

namespace Noticebord.Cli.Commands.Notices
{
    public class DeleteNoticeCommand : AsyncCommand<DeleteNoticeSettings>
    {
        private readonly IClient _client;

        public DeleteNoticeCommand(IClient client) => _client = client;

        public override async Task<int> ExecuteAsync([NotNull] CommandContext context, [NotNull] DeleteNoticeSettings settings)
        {
            if (!AnsiConsole.Confirm("Are you sure you want to delete this notice?"))
                return 0;

            var notice = await AnsiConsole.Status()
                .StartAsync("Creating...", async ctx => await _client.DeleteNoticeAsync(settings.Notice));

            AnsiConsole.MarkupLine($"Notice [bold yellow]#{notice.Id} - {notice.Title}[/] was deleted.");
            return 0;
        }
    }
}