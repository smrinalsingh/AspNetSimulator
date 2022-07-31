using Microsoft.Extensions.DependencyInjection;
using AspNetSimulator.Data.Contracts;
using AspNetSimulator.Data.Implementations;
using AspNetSimulator.Data.Config;

namespace AspNetSimulator.Data.Extensions
{
    public static class Services
    {
        public static IServiceCollection AddCustomInjections(this IServiceCollection services, Microsoft.Extensions.Hosting.HostBuilderContext hostingContext)
        {
            services.AddOptions();
            services.Configure<HttpConfig>(hostingContext.Configuration.GetSection("Listener"));
            services.Configure<RouteConfig>(hostingContext.Configuration.GetSection("Route"));
            services.AddSingleton<IListener, Listener>();
            services.AddSingleton<IRoute, Route>();
            services.AddSingleton<IRequestHandler, RequestHandler>();
            return services;
        }
    }
}
