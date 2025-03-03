using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace CafeEmployeeAPI.Infrastructure.Data
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables() // Add environment variables
                .Build();

            // Get connection string from environment variable first, then fallback to appsettings.json
            string connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING") 
                                    ?? configuration.GetConnectionString("DefaultConnection");

            // Print connection string (for debugging)
            Console.WriteLine($"Using Connection String: {connectionString}");

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new ApplicationDbContext(optionsBuilder.Options);


            // var configuration = new ConfigurationBuilder()
            //     .SetBasePath(Directory.GetCurrentDirectory())
            //     .AddJsonFile("appsettings.json")
            //     .Build();

            // //print connection string from configuration 
            // System.Console.WriteLine(configuration.GetConnectionString("DefaultConnection"));

            // var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            // optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));

            // return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}
