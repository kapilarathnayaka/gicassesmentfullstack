using CafeEmployeeAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CafeEmployeeAPI.Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Cafe> Cafes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Employee Unique ID format enforcement
            modelBuilder.Entity<Employee>()
                .Property(e => e.Id)
                .IsRequired();

            modelBuilder.Entity<Employee>()
                .HasIndex(e => e.EmailAddress)
                .IsUnique();

            modelBuilder.Entity<Employee>()
                .HasIndex(e => e.PhoneNumber)
                .IsUnique();

            modelBuilder.Entity<Employee>()
                .Property(e => e.PhoneNumber)
                .HasMaxLength(20);

            // Employee-Cafe Relationship (One-to-Many)
            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Cafe)
                .WithMany(c => c.Employees)
                .HasForeignKey(e => e.CafeId)
                .OnDelete(DeleteBehavior.Cascade); // Delete employees if cafe is deleted

            base.OnModelCreating(modelBuilder);
        }
    }
}
