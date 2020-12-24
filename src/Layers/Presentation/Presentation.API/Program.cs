using System;
using System.Threading.Tasks;
using Infrastructure.Identity.API;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace Presentation.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    Log.Information("ASP.NET Core Identity Database Migration");
                    await services.GetRequiredService<WlodzimierzIdentityContext>().Database.MigrateAsync();
                }
                catch (Exception ex)
                {
                    Log.Fatal(ex, ex.Message);
                    Log.CloseAndFlush();
                    throw;
                }
            }

            await host.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((context, config) =>
                {
                    var env = context.HostingEnvironment;

                    config.AddJsonFile("appsettings.json", false, true)
                        .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true, true)
                        .AddJsonFile("appsettings.Caching.json", false, true)
                        .AddJsonFile("appsettings.Identity.json", false, true)
                        .AddJsonFile("appsettings.Persistence.json", false, true);

                    config.AddEnvironmentVariables();
                })
                .ConfigureWebHostDefaults(builder => builder.UseStartup<Startup>())
                .UseSerilog((context, config) => config.ReadFrom.Configuration(context.Configuration));
        }
    }
}