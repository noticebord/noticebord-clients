using System;
using System.Threading.Tasks;
using Cocona;
using Noticebord.Cli.Filters;
using Noticebord.Cli.Utils;
using Noticebord.Client;
using static Noticebord.Cli.Utils.Output;
using static Noticebord.Cli.Utils.Output.OutputFormats;

namespace Noticebord.Cli.Commands
{
    public class Notices
    {
        [BannerCommandFilter]
        [PrimaryCommand]
        [Command(Description = "Fetch and display all notices.")]
        public async Task List(
            [FromService]IClient client,
            [Option('f', Description = "Specify output format.")]OutputFormats format = Text,
            [Option('s', Description = "Write output to file.")]bool save = false)
        {
            var notices = await client.GetNoticesAsync();

            if (save) await Output.ExportManyAsync(notices, format);
            else Output.DisplayMany(notices, format);
        }

        [BannerCommandFilter]
        [Command(Description = "Fetch and display a single notice by id.")]
        public async Task Get(
            [FromService]IClient client, 
            [Argument(Description = "ID of the notice to fetch.")]long id,
            [Option('f', Description = "Specify output format.")]OutputFormats format = Text,
            [Option('s', Description = "Write output to file.")]bool save = false)
        {
            var notice = await client.GetNoticeAsync(id);

            if (save) await Output.ExportAsync(notice, format);
            else Output.Display(notice, format);
        }
    }
}