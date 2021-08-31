using System.ComponentModel;
using Spectre.Console.Cli;

namespace Noticebord.Cli.Settings.Notices
{
    public class DeleteNoticeSettings : NoticesSettings
    {
        [CommandArgument(0, "<NOTICE>")]
        [Description("ID of notice to delete")]
        public long Notice { get; init; }
    }
}