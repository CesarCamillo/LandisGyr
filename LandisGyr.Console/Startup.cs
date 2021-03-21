using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace LandisGyr.ConsoleApp
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        private const string appSettingsPath = "C:\\Users\\cesar\\source\\repos\\LandisGyr\\LandisGyr.Console\\appsettings.json";

        public Startup()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile(appSettingsPath, optional: false)
                .AddEnvironmentVariables()
                .Build();

            _configuration = config;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLandisGyrPersistence(_configuration);
        }
    }
}
