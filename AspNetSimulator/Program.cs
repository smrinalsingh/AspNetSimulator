using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using AspNetSimulator.Data.Extensions;

namespace AspNetSimulator
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            IHostBuilder hostBuilder = new HostBuilder()
                .ConfigureAppConfiguration((hostingContext, host) =>
                {
                    host.UseCustomBuilder();
                })
                .ConfigureServices((hostingContext, services) =>
                {
                    services.AddCustomInjections(hostingContext);
                    services.AddSingleton<IHostedService, AspNetSimulator>();
                })
                .ConfigureLogging((hostingContext, logging) =>
                {
                    logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                    logging.AddConsole();
                });

            await hostBuilder.RunConsoleAsync();
        }
    }
}