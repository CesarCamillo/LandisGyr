using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MediatR;
using AutoMapper;
using Microsoft.Extensions.Hosting;
using LandisGyr.ConsoleApp.Controller;

namespace LandisGyr.ConsoleApp
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        private const string appSettingsPath = "C:\\Users\\cesar\\repos\\C#\\LandisGyr\\LandisGyr.Console\\appsettings.json";

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

            services.AddSingleton<IHostedService, ConsoleController>();

            services.AddMediatR(GetType().Assembly);

            services.AddAutoMapper(GetType().Assembly);
        }
    }
}
