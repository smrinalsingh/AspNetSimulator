using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AspNetSimulator.Data;
using AspNetSimulator.Data.Contracts;
using AspNetSimulator.Data.Implementations;

namespace AspNetSimulator.Data.Extensions
{
    public static class Services
    {
        public static IServiceCollection AddCustomInjections(this IServiceCollection services)
        {
            services.AddSingleton<IListener, Listener>();
            services.AddSingleton<IRoute, Route>();
            services.AddSingleton<IRequestHandler, RequestHandler>();
            return services;
        }
    }
}
