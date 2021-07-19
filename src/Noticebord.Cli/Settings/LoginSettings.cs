using System.ComponentModel;
using Spectre.Console.Cli;

namespace Noticebord.Cli.Settings
{
    public class LoginSettings : AppSettings
    {
        [CommandOption("-i|--interactive")]
        [Description("Use interactive mode")]
        public bool Interactive { get; init; }

        [CommandOption("-e|--email-address <EMAIL_ADDRESS>")]
        [Description("Email address to authenticate with")]
        public string? Email { get; init; }

        [CommandOption("-p|--password <PASSWORD>")]
        [Description("Password to authenticate with")]
        public string? Password { get; init; }
    }
}