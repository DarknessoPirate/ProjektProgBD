using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProjektProgBD.Models;
using System;
using System.IO;
using Microsoft.EntityFrameworkCore;

namespace ProjektProgBD.Tools
{
    public static class ConfigurationSetup
    {
        public static IServiceProvider ConfigureServices()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            var configuration = builder.Build();

            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection, configuration);

            return serviceCollection.BuildServiceProvider();
        }

        private static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            Console.WriteLine("Connection String: " + connectionString); // Check if this prints the correct connection string

            services.AddSingleton(configuration);
            services.AddDbContext<ShopDbContext>(options =>
                options.UseSqlServer(connectionString));
            services.AddTransient<MainWindow>();
            // Add other services and dependencies here
        }
    }
}