using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace LandisGyr.Persistence
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
