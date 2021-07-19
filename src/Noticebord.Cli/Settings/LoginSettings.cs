using System.ComponentModel;
using Spectre.Console.Cli;

namespace Noticebord.Cli.Settings
{
    public class LoginSettings : AppSettings
    {
        [CommandOption("-i|--interactive")]
        [Description("Use interactive mode")]
        public bool Interactive { get; set; }

        [CommandOption("-u|--username <USERNAME>")]
        [Description("Username to authenticate with")]
        public string Username { get; set; }

        [CommandOption("-p|--password <PASSWORD>")]
        [Description("Password to authenticate with")]
        public string Password { get; set; }

        [CommandOption("-d|--device-name <DEVICE_NAME>")]
        [Description("Name of this device, e.g. My HP Laptop")]
        public string DeviceName { get; set; }
    }
}