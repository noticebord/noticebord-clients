using System;
using System.Threading.Tasks;
using Cocona;
using Noticebord.Cli.Filters;
using Noticebord.Client;
using static Noticebord.Cli.Commands.Notices.OutputFormats;

namespace Noticebord.Cli.Commands
{
    public class Notices
    {
        public enum OutputFormats
        {
            Text,
            Json
        }

        [BannerCommandFilter]
        [PrimaryCommand]
        [Command(Description = "Fetch and display all notices.")]
        public async Task List(
            [FromService]IClient client,
            [Option('f', Description = "Specify output format.")]OutputFormats format = Text)
        {
            var notices = await client.GetNoticesAsync();
            foreach (var notice in notices)
            {
                Console.WriteLine(notice.Title);
            }
        }

        [BannerCommandFilter]
        [Command(Description = "Fetch and display a single notice by id.")]
        public async Task Get(
            [FromService]IClient client, 
            [Argument(Description = "ID of the notice to fetch.")]long id,
            [Option('f', Description = "Specify output format.")]OutputFormats format = Text)
        {
            var notice = await client.GetNoticeAsync(id);
            Console.WriteLine($"{notice.Title}\n\n{notice.Text}");
        }
    }
}