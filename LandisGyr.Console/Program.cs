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
                    .EnsureDatabaseMigrated();
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
