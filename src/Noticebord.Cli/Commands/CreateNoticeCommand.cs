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

        public CreateNoticeCommand(IClient client) => _client = client;

        public override async Task<int> ExecuteAsync([NotNull] CommandContext context, [NotNull] CreateNoticeSettings settings)
        {
            var title = AnsiConsole.Ask<string>("Enter a title for this notice:");
            var body = AnsiConsole.Ask<string>("Enter the body for this notice:");
            var anonymous = false;

            if (_client.IsLoggedIn) 
                anonymous = AnsiConsole.Confirm("Do you want to post this notice anonymously?");

            CreateNoticeRequest request = new(title, body, anonymous);

            var notice = await AnsiConsole.Status()
                .StartAsync("Creating...", async ctx => await _client.CreateNoticeAsync(request));

            AnsiConsole.MarkupLine($"Notice [bold yellow]#{notice.Id} - {notice.Title}[/] was created.");
            return 0;
        }
    }
}