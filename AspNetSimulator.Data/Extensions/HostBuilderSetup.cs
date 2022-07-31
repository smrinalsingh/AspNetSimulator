using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetSimulator.Data.Extensions
{
    public static class HostBuilderSetup
    {
        public static IConfigurationBuilder UseCustomBuilder(this IConfigurationBuilder builder)
        {
            builder.AddJsonFile("appConfig.json", true, true);
            builder.AddEnvironmentVariables();
            return builder;
        }
    }
}
