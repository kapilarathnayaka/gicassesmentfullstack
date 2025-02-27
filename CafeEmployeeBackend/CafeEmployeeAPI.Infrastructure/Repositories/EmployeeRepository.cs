using CafeEmployeeAPI.Application.Interfaces.Repositories;
using CafeEmployeeAPI.Domain.Entities;
using CafeEmployeeAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CafeEmployeeAPI.Infrastructure.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext _context;

        public EmployeeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Employee?> GetByIdAsync(string id)
        {
          //  return await _context.Employees.FindAsync(id);
            //get employees with their cafe 
            return await _context.Employees
               .Include(e => e.Cafe)
               .FirstOrDefaultAsync(e => e.Id == id);    
        }

        public async Task<List<Employee>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.Employees
                .Include(e => e.Cafe) 
                .ToListAsync(cancellationToken);
        }

        public async Task AddAsync(Employee employee)
        {
            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Employee employee)
        {
            _context.Employees.Update(employee);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Employee employee)
        {
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
        }

        // Fetch all employees related to a specific cafe
        public async Task<List<Employee>> GetByCafeIdAsync(string cafeId)
        {
            return await _context.Employees
            .Include(e => e.Cafe)
            .Where(e => e.CafeId.ToString() == cafeId)
            .ToListAsync();
        }
    }
}
