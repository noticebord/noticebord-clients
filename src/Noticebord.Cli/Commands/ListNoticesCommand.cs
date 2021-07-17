using System;
using System.Linq;
using System.Threading.Tasks;
using Noticebord.Cli.Settings;
using Noticebord.Client;
using Spectre.Console;
using Spectre.Console.Cli;

namespace Noticebord.Cli.Commands
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
            var notices = await _client.GetNoticesAsync();
            var table = new Table();

            table.AddColumn(new ("[bold yellow]ID[/]"));
            table.AddColumn(new ("[bold yellow]Title[/]"));
            table.AddColumn(new ("[bold yellow]Author[/]"));
            table.AddColumn(new ("[bold yellow]Created At[/]"));
            table.AddColumn(new ("[bold yellow]Updated At[/]"));

            notices.ForEach(n =>
                table.AddRow(
                    n.Id.ToString(),
                    n.Title,
                    n.Author.Name,
                    n.CreatedAt.ToString(),
                    n.UpdatedAt.ToString()));

            AnsiConsole.Render(table);
            return 0;
        }
    }
}