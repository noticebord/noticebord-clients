using Noticebord.Cli.Commands;
using Noticebord.Cli.Settings;
using Spectre.Console.Cli;

var app = new CommandApp();

app.Configure(config =>
{
    config.AddBranch<NoticesSettings>("notices", notices =>
    {
        notices.AddCommand<ListNoticesCommand>("list")
            .WithDescription("List all notices")
            .WithExample(new []{"notices", "list"});
        notices.AddCommand<ShowNoticeCommand>("show")
            .WithDescription("Show a single notice by its ID")
            .WithExample(new []{"notices", "show", "1"});
    });
});

return app.Run(args);




