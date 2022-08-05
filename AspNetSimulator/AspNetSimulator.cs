using Microsoft.Extensions.Hosting;
using System.Net;
using AspNetSimulator.Data.Contracts;

namespace AspNetSimulator
{
    internal class AspNetSimulator : IHostedService, IDisposable
    {
        private readonly IListener _listenerConfig;
        private readonly HttpListener Listener;
        public AspNetSimulator(IListener listenerConfig)
        {
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