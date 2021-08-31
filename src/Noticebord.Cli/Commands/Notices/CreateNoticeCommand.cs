using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Noticebord.Cli.Settings.Notices;
using Noticebord.Client;
using Noticebord.Client.Models;
using Spectre.Console;
using Spectre.Console.Cli;

namespace Noticebord.Cli.Commands.Notices
{
    public class CreateNoticeCommand : AsyncCommand<CreateNoticeSettings>
    {
        private readonly IClient _client;

        public CreateNoticeCommand(IClient client) => _client = client;

        public override async Task<int> ExecuteAsync([NotNull] CommandContext context, [NotNull] CreateNoticeSettings settings)
        {
            var request = new SaveNoticeRequest(
                Title: AnsiConsole.Ask<string>("Enter a title for this notice:"),
                Body: AnsiConsole.Ask<string>("Enter the body of this notice:"),
                Anonymous: true,
                Public: true
            );

            if (_client.IsLoggedIn) {
                var anonymous = AnsiConsole.Confirm("Do you want to post this notice anonymously?");
                request = request with {
                    Anonymous = anonymous,
                    Public = anonymous || AnsiConsole.Confirm("Do you want to post this notice publicly?")
                };
            }

            var notice = await AnsiConsole.Status()
                .StartAsync("Creating...", async ctx => await _client.CreateNoticeAsync(request));

            AnsiConsole.MarkupLine($"Notice [bold yellow]#{notice.Id} - {notice.Title}[/] was created.");
            return 0;
        }
    }
}