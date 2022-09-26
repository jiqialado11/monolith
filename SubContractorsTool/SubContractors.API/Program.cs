using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using SubContractors.Common.Logging;
using System;
using System.IO;

namespace SubContractors.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                                                          .AddJsonFile("appsettings.json")
                                                          .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", true)
                                                          .Build();

            Log.Logger = Extension.PrepareLogger(configuration);

            try
            {
                Log.Information("SubContractors.API is starting");
                CreateHostBuilder(args).UseLogging().Build().Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "SubContractors.API failed to start");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                       .ConfigureWebHostDefaults(webBuilder =>
                       {
                           webBuilder.UseStartup<Startup>();
                       });
        }
    }
}