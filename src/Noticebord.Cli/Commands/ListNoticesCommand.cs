using System;
using System.Threading.Tasks;
using Noticebord.Cli.Settings;
using Spectre.Console.Cli;

namespace Noticebord.Cli.Commands
{
    public class ListNoticesCommand : AsyncCommand<ListNoticesSettings>
    {
        public override async Task<int> ExecuteAsync(CommandContext context, ListNoticesSettings settings)
        {
            await Task.Delay(TimeSpan.FromSeconds(2));
            return 0;
        }
    }
}