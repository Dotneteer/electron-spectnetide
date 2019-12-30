using ElectronNET.API;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Spect.Net.Shell
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    // --- Allow Electron.NET
                    // --- Always use it before UseStartup
                    webBuilder
                        .UseElectron(args)
                        .UseStartup<Startup>();
                });
    }
}
