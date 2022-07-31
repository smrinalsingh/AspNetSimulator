using System.Net;

namespace AspNetSimulator.Data.Contracts
{
    public interface IRequestHandler
    {
        void Handle(HttpListenerContext context);
    }
}