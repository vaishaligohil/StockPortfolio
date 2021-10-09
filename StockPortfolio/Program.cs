using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using StockPortfolioDB;

namespace StockPortfolio
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
           var host = CreateHostBuilder(args).Build();
            await RunDatabaseMigrationsOnStartupAsync(host);
            host.Run();
        }

        private static async Task RunDatabaseMigrationsOnStartupAsync(IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                IServiceProvider services = scope.ServiceProvider;
                try
                {
                    // NOTE: Set to true using environment variable EULA_API_DbMigrations__RunAtStartup on server
                    var configuration = services.GetRequiredService<IConfiguration>();
                    var runMigrations = configuration.GetSection("DbMigrations:RunAtStartup")?.Value?.ToLower() == "true";
                    var context = services.GetRequiredService<StockDBContext>();
                    await DbInitializer.InitializeAsync(context.Database, runMigrations);
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while seeding the database.");
                }
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
