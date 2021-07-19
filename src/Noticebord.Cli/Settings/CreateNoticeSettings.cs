using System.ComponentModel;
using Spectre.Console.Cli;

namespace Noticebord.Cli.Settings
{
    public class CreateNoticeSettings : NoticesSettings
    {
        [CommandOption("-i|--interactive")]
        [Description("Use interactive mode")]
        public bool Interactive { get; init; }

        [CommandOption("-t|--title <TITLE>")]
        [Description("Title of the notice")]
        public string? Title { get; init; }

        [CommandOption("-b|--body <BODY>")]
        [Description("Body of the notice")]
        public string? Body { get; init; }
    }
}