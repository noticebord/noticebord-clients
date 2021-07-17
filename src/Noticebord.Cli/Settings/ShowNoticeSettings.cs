using Spectre.Console.Cli;
using static Noticebord.Cli.Utils.Output;

namespace Noticebord.Cli.Settings
{
    public class ShowNoticeSettings : NoticesSettings
    {
        [CommandArgument(0, "<NOTICE>")]
        public long Notice { get; set; }
    }
}