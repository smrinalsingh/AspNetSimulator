using Microsoft.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using AspNetSimulator.Data.Extensions;
using AspNetSimulator.Data.Config;

namespace AspNetSimulator
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            IHostBuilder hostBuilder = new HostBuilder()
                .ConfigureAppConfiguration((hostingContext, configuration) =>
                {
                    configuration.AddJsonFile("appConfig.json", true, true);
                    configuration.AddEnvironmentVariables();
                })
                .ConfigureServices((hostingContext, services) =>
                {
                    services.AddOptions();
                    services.Configure<HttpConfig>(hostingContext.Configuration.GetSection("Listener"));
                    services.Configure<RouteConfig>(hostingContext.Configuration.GetSection("Route"));
                    services.AddCustomInjections();
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