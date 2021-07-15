using System;
using Figgle;
using Cocona;
using Microsoft.Extensions.DependencyInjection;
using Noticebord.Cli.Filters;
using Noticebord.Cli.Commands;
using Noticebord.Client;

namespace Noticebord.Cli
{
    [HasSubCommands(typeof(Notices), Description = "Commands dealing with notices.")]
    public class Program
    {
        public static void Main(string[] args) => CoconaApp.Create()
            .ConfigureServices(services =>
            {
                services.AddTransient<IClient, NoticebordClient>();
            })
            .Run<Program>(args);

        [BannerCommandFilter]
        [Command(Description = "Display application information.")]
        public void Info(){
            var info = $"Noticebord 1.0.0 (cli) (built: {DateTime.Now}) ( .NET {Environment.Version} x64 )\n" +
                "Copyright (c) The Noticebord Group\n" +
                "Noticebord 1.0.0, Copyright (c) Noticebord Technologies";
            Console.WriteLine(info);
        } 
    }
}




