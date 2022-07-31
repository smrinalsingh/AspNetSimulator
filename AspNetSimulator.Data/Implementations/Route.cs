using Microsoft.Extensions.Options;
using AspNetSimulator.Data.Contracts;
using AspNetSimulator.Data.Config;

namespace AspNetSimulator.Data.Implementations
{
    internal class Route : IRoute
    {
        public string DefaultController { get; set; }
        public string DefaultAction { get; set; }

        public Route(IOptions<RouteConfig> routeConfig)
        {
            DefaultController = routeConfig.Value.Controller;
            DefaultAction = routeConfig.Value.Action;
        }
    }
}
