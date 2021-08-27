using System;
using System.IO;
using Flurl.Http;
using Microsoft.Extensions.DependencyInjection;
using Noticebord.Cli.Commands;
using Noticebord.Cli.Infrastructure;
using Noticebord.Cli.Settings;
using Noticebord.Client;
using Spectre.Console.Cli;

string? token = default;

var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "noticebord", "token.txt");
if (File.Exists(path))
    token = await File.ReadAllTextAsync(path);

var noticebordClient = new NoticebordClient(token /* , "http://localhost:8000/api" */);

FlurlHttp.ConfigureClient(noticebordClient.BaseUrl, client => {
    client.HttpClient.DefaultRequestHeaders.Add("Accept", "application/json");
    client.HttpClient.DefaultRequestHeaders.Add("User-Agent", "Noticebord.Cli");
});

var registrations = new ServiceCollection();
registrations.AddSingleton<IClient>(_ => noticebordClient);

var app = new CommandApp(new TypeRegistrar(registrations));

app.Configure(config =>
{
    config.AddBranch<NoticesSettings>("notices", notices =>
    {
        notices.AddCommand<CreateNoticeCommand>("create")
            .WithDescription("Create a new notice")
            .WithExample(new[] { "notices", "create", "--interactive" })
            .WithExample(new[] {
                "notices", "create",
                "--title", "Hi Everyone!",
                "--text", "This is an awesome notice."
            });
        notices.AddCommand<ListNoticesCommand>("list")
            .WithDescription("List all notices")
            .WithExample(new[] { "notices", "list" });
        notices.AddCommand<ShowNoticeCommand>("show")
            .WithDescription("Show a single notice by its ID")
            .WithExample(new[] { "notices", "show", "1" });
    });

    config.AddCommand<LoginCommand>("login")
            .WithDescription("Log in to your account")
            .WithExample(new[] { "login", "--interactive" })
            .WithExample(new[] {
                "login",
                "--username", "user@mail.com",
                "--password", "password"
            });

    config.AddCommand<LogoutCommand>("logout")
            .WithDescription("Log out of your account")
            .WithExample(new[] { "logout" });
});

return app.Run(args);




