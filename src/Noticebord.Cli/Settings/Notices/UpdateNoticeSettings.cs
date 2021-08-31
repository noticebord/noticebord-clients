using System.ComponentModel;
using Spectre.Console.Cli;

namespace Noticebord.Cli.Settings.Notices
{
    public class UpdateNoticeSettings : NoticesSettings
    {
        [CommandArgument(0, "<NOTICE>")]
        [Description("ID of notice to update")]
        public long Notice { get; init; }
    }
}