using System;
using System.Collections.Generic;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Data
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new AppDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<AppDbContext>>()))
            {
                if (context.Employees.Any() || context.Cafes.Any())
                {
                    return;   // DB has been seeded
                }

                var cafes = new List<Cafe>
                {
                    new Cafe
                    {
                        Id = Guid.NewGuid(),
                        Name = "Cafe A",
                        Description = "A cozy place to relax.",
                        Location = "Location 1"
                    },
                    new Cafe
                    {
                        Id = Guid.NewGuid(),
                        Name = "Cafe B",
                        Description = "A great place for coffee.",
                        Location = "Location 2"
                    }
                };

                var employees = new List<Employee>
                {
                    new Employee
                    {
                        Id = "UI1234567",
                        Name = "John Doe",
                        EmailAddress = "john.doe@example.com",
                        PhoneNumber = "91234567",
                        Gender = Gender.Male,
                        StartDate = DateTime.Now.AddDays(-10),
                        CafeId = cafes[0].Id.ToString()
                    },
                    new Employee
                    {
                        Id = "UI2345678",
                        Name = "Jane Smith",
                        EmailAddress = "jane.smith@example.com",
                        PhoneNumber = "82345678",
                        Gender = Gender.Female,
                        StartDate = DateTime.Now.AddDays(-20),
                        CafeId = cafes[1].Id.ToString()
                    }
                };

                context.Cafes.AddRange(cafes);
                context.Employees.AddRange(employees);
                context.SaveChanges();
            }
        }
    }
}
