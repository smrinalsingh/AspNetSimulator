using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AspNetSimulator.Data.Contracts
{
    public interface IListener
    {
        public HttpListener GetListener();
        public void Listen();
    }
}
