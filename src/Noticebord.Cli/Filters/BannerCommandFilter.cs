using System;
using System.Threading.Tasks;
using Cocona.Filters;
using Figgle;

namespace Noticebord.Cli.Filters
{
    class BannerCommandFilterAttribute : CommandFilterAttribute
    {
        public override async ValueTask<int> OnCommandExecutionAsync(CoconaCommandExecutingContext ctx, CommandExecutionDelegate next)
        {
            Console.WriteLine($"{FiggleFonts.Standard.Render("Noticebord")}Noticebord v1.0.0\n---");
            try
            {
                return await next(ctx);
            }
            finally
            {
            }
        }
    }   
}