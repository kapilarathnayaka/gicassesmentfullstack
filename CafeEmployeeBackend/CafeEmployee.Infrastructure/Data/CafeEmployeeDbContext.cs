using Microsoft.EntityFrameworkCore;
using CafeEmployee.Domain.Entities;

namespace CafeEmployee.Infrastructure.Data
{
    public class CafeEmployeeDbContext : DbContext
    {
        public DbSet<Cafe> Cafes { get; set; }
        public DbSet<Employee> Employees { get; set; }

        public CafeEmployeeDbContext(DbContextOptions<CafeEmployeeDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Cafe>().HasKey(c => c.Id);
            modelBuilder.Entity<Employee>().HasKey(e => e.Id);

            // Seeding with sample data
            modelBuilder.Entity<Cafe>().HasData(
                new Cafe { Id = Guid.NewGuid(), Name = "Cafe 1", Description = "Description 1", Location = "Location 1" },
                new Cafe { Id = Guid.NewGuid(), Name = "Cafe 2", Description = "Description 2", Location = "Location 2" }
            );

            modelBuilder.Entity<Employee>().HasData(
                new Employee { Id = "UI0000001", Name = "Employee 1", EmailAddress = "employee1@example.com", PhoneNumber = "1234567890", Gender = "Male" },
                new Employee { Id = "UI0000002", Name = "Employee 2", EmailAddress = "employee2@example.com", PhoneNumber = "0987654321", Gender = "Female" }
            );
        }
    }
}
