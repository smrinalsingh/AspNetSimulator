using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AspNetSimulator.Data.Contracts;

namespace AspNetSimulator.Controllers
{
    internal class HomeController : IControllerBase
    {
        public string Index()
        {
            return "HomeController/Index responds!";
        }
        public string GetName()
        {
            return "HomeController/GetName responds!";
        }
    }
}
