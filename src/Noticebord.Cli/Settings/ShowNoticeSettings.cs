using System.ComponentModel;
using Spectre.Console.Cli;

namespace Noticebord.Cli.Settings
{
    public class ShowNoticeSettings : NoticesSettings
    {
        [CommandArgument(0, "<NOTICE>")]
        [Description("ID of notice to show")]
        public long Notice { get; set; }
    }
}