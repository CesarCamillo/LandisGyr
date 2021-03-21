using LandisGyr.ConsoleApp;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class LandisGyrServiceExtension
    {   
        public static IServiceCollection AddLandisGyrPersistence (this IServiceCollection services, IConfiguration configuration)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            string connString = configuration.GetConnectionString(LandisGyrContext.ConnectionStringKey);
            if (string.IsNullOrWhiteSpace(connString))
            {
                throw new InvalidOperationException($"Connection String {LandisGyrContext.ConnectionStringKey} não encontrada");
            }

            services.AddDbContext<LandisGyrContext>(opt => opt.UseInMemoryDatabase(connString));

            return services;
        }
    }
}
