using CafeEmployee.Domain.Entities;
using CafeEmployee.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CafeEmployee.Infrastructure.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly CafeEmployeeDbContext _context;

        public EmployeeRepository(CafeEmployeeDbContext context)
        {
            _context = context;
        }

        public async Task<Employee> GetByIdAsync(string id)
        {
            return await _context.Employees.FindAsync(id);
        }

        public async Task<List<Employee>> GetAllAsync()
        {
            return await _context.Employees.ToListAsync();
        }

        public async Task<string> CreateAsync(Employee employee)
        {
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
            return employee.Id;
        }

        public async Task<bool> UpdateAsync(Employee employee)
        {
            _context.Employees.Update(employee);
            var rowsAffected = await _context.SaveChangesAsync();
            return rowsAffected > 0;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var employee = await GetByIdAsync(id);
            if (employee == null) return false;

            _context.Employees.Remove(employee);
            var rowsAffected = await _context.SaveChangesAsync();
            return rowsAffected > 0;
        }
    }
}
