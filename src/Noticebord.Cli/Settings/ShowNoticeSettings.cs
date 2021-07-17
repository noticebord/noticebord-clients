using Spectre.Console.Cli;

namespace Noticebord.Cli.Settings
{
    public class ShowNoticeSettings : NoticesSettings
    {
        [CommandArgument(0, "<NOTICE>")]
        public long Notice { get; set; }
    }
}