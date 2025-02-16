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
        }
    }
}
