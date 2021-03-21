using LandisGyr.ConsoleApp;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;

namespace Microsoft.AspNetCore.Hosting
{
    /// <summary>
    /// Extensões de Persistência para o Host
    /// </summary>
    public static class LandisGyrHostExtension
    {
        /// <summary>
        /// Flag para controle das migrações
        /// </summary>
        public const string MigrationKey = "LandisGyrMigration";

        /// <summary>
        /// Garante que a database esteja atualizada
        /// </summary>
        /// <param name="host"></param>
        /// <returns></returns>
        public static IHost EnsureDatabaseMigrated(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                var context = services.GetRequiredService<LandisGyrContext>();
                var pending = context.Database.GetPendingMigrations();

                if (pending.Any())
                {
                    var configuration = services.GetRequiredService<IConfiguration>();
                    bool migrateFlag = configuration.GetValue<bool>(MigrationKey);

                    if (migrateFlag)
                    {
                        context.Database.Migrate();
                    }
                    else
                    {
                        throw new InvalidOperationException("Existem migrations pendente. Altere a flag" + MigrationKey + "para True ou atualize a base manualmente.");
                    }
                }
            }

            return host;
        }
    }
}
