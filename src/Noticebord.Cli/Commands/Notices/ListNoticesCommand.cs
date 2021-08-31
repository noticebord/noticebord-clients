using System;
using System.Linq;
using System.Threading.Tasks;
using Noticebord.Cli.Settings.Notices;
using Noticebord.Client;
using Spectre.Console;
using Spectre.Console.Cli;
using Noticebord.Cli.Utils;

namespace Noticebord.Cli.Commands.Notices
{
    public class ListNoticesCommand : AsyncCommand<ListNoticesSettings>
    {
        private readonly IClient _client;
        public ListNoticesCommand(IClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public override async Task<int> ExecuteAsync(CommandContext context, ListNoticesSettings settings)
        {
            var notices = await AnsiConsole.Status()
                    .StartAsync("Fetching...", async ctx =>
                    {
                        var data = await _client.GetNoticesAsync();
                        return data.Select(datum => NoticeUtils.AssignDefaultAuthor(datum)).ToList();
                    });
            var table = new Table();

            table.AddColumn(new("[bold yellow]ID[/]"));
            table.AddColumn(new("[bold yellow]Title[/]"));
            table.AddColumn(new("[bold yellow]Author[/]"));
            table.AddColumn(new("[bold yellow]Created At[/]"));
            table.AddColumn(new("[bold yellow]Updated At[/]"));

            notices.ForEach(notice => table.AddRow(
                notice.Id.ToString(),
                notice.Title,
                notice.Author.Name,
                notice.CreatedAt.ToString(),
                notice.UpdatedAt.ToString())
            );

            AnsiConsole.Render(table);
            return 0;
        }
    }
}