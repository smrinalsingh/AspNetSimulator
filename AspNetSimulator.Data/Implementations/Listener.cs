using System.Net;
using Microsoft.Extensions.Options;
using AspNetSimulator.Data.Contracts;
using AspNetSimulator.Data.Config;

namespace AspNetSimulator.Data.Implementations
{
    internal class Listener : IListener
    {
        private HttpListener _listener = new HttpListener();
        private IOptions<HttpConfig> _httpConfig;
        private IRequestHandler _handler;

        public Listener(IOptions<HttpConfig> httpConfig, IRequestHandler handler)
        {
            _httpConfig = httpConfig;
            _handler = handler;
        }

        public HttpListener GetListener()
        {
            _listener.Prefixes.Add("http://+:" + _httpConfig.Value.Port + "/");
            _listener.Start();
            return _listener;
        }

        public async void Listen()
        {
            try
            {
                while (_listener.IsListening)
                {
                    var context = await _listener.GetContextAsync();
                    Task.Factory.StartNew(() => ProcessRequest(context));
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void ProcessRequest(HttpListenerContext context)
        {
            _handler.Handle(context);
        }
    }
}
