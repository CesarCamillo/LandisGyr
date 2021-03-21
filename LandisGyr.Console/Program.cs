using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;

namespace LandisGyr.ConsoleApp
{
    public class Program
    {
        static void Main(string[] args)
        {    
            try
            {
                CreateHostBuilder(args)
                    .Build()
                    .EnsureDatabaseMigrated()
                    .RunAsync();
            }
            catch (Exception ex)    
            {
                throw ex;
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
