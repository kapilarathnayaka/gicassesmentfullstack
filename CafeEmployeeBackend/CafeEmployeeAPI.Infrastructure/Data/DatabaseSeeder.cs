using CafeEmployeeAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace CafeEmployeeAPI.Infrastructure.Data
{
    public class DatabaseSeeder
    {
        public static void Seed(IServiceProvider serviceProvider)
        {
            using var context = serviceProvider.GetRequiredService<ApplicationDbContext>();

            if (!context.Cafes.Any())
            {
                context.Cafes.AddRange(
                    new Cafe { Id = Guid.NewGuid(), Name = "Cafe A", Description = "Nice cafe", Location = "Downtown" },
                    new Cafe { Id = Guid.NewGuid(), Name = "Cafe B", Description = "Cozy place", Location = "Uptown" }
                );
            }

            if (!context.Employees.Any())
            {
                context.Employees.AddRange(
                    new Employee { Id = "UI1234567", Name = "John Doe", EmailAddress = "john@example.com", PhoneNumber = "91234567", Gender = "Male", StartDate = DateTime.UtcNow.AddDays(-100) },
                    new Employee { Id = "UI2345678", Name = "Jane Doe", EmailAddress = "jane@example.com", PhoneNumber = "82345678", Gender = "Female", StartDate = DateTime.UtcNow.AddDays(-50) }
                );
            }

            context.SaveChanges();
        }
    }
}
