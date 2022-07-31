using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net;
using System.Text;
using AspNetSimulator.Data.Contracts;
using AspNetSimulator.Data.Config;

namespace AspNetSimulator
{
    internal class AspNetSimulator : IHostedService, IDisposable
    {
        private readonly IOptions<HttpConfig> _appConfig;
        private readonly IListener _listenerConfig;
        private readonly HttpListener Listener;
        public AspNetSimulator(IOptions<HttpConfig> appConfig, IRequestHandler requestHandler, IListener listenerConfig)
        {
            _appConfig = appConfig;
            _listenerConfig = listenerConfig;
            Listener = listenerConfig.GetListener();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _listenerConfig.Listen();
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            Console.WriteLine("Cleaning up..");
            Listener.Close();
        }
    }
}