using Dna;
using Dna.AspNet;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Fasetto.Word.Web.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Run();
        }

        public static IWebHost CreateHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder()
            // Add Dna Framework
            .UseDnaFramework(construct =>
            {
                // Configure framework

                // Add file logger
                construct.AddFileLogger();
            })
            .UseStartup<Startup>()
            .Build();
               
    }
}
