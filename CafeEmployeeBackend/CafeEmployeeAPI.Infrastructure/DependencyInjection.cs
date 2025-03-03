using CafeEmployeeAPI.Application.Interfaces.Repositories;
using CafeEmployeeAPI.Infrastructure.Repositories;
using CafeEmployeeAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CafeEmployeeAPI.Infrastructure
{
    public static class DependencyInjection
    {
   
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {

            var connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING") 
                           ?? configuration.GetConnectionString("DefaultConnection");
            
            Console.WriteLine("Connection String: " + connectionString);

              services.AddDbContext<ApplicationDbContext>(options =>
                  options.UseSqlServer(connectionString)
                );

            //Register repositories
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<ICafeRepository, CafeRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
