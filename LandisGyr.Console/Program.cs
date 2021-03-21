using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
            Host.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
