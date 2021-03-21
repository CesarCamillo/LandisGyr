using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;

namespace LandisGyr.ConsoleApp
{
    public class Program
    {
        private const string appSettingsPath = "C:\\Users\\cesar\\source\\repos\\LandisGyr\\LandisGyr.Console\\appsettings.json";

        static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile(appSettingsPath, optional: false)
                .AddEnvironmentVariables()
                .Build();

            try
            {
                CreateHostBuilder(args)
                    .Build()
                    .EnsureDatabaseMigrated()
                    .Run();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            
            Console.WriteLine("Hello World");
            Console.ReadLine();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args);
    }
}
