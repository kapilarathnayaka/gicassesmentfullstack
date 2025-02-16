using CafeEmployee.Domain.Entities;
using CafeEmployee.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CafeEmployee.Infrastructure.Repositories
{
    public class CafeRepository : ICafeRepository
    {
        private readonly CafeEmployeeDbContext _context;

        public CafeRepository(CafeEmployeeDbContext context)
        {
            _context = context;
        }

        public async Task<Cafe> GetByIdAsync(Guid id)
        {
            return await _context.Cafes.FindAsync(id);
        }

        public async Task<List<Cafe>> GetAllAsync()
        {
            return await _context.Cafes.ToListAsync();
        }

        public async Task<Guid> CreateAsync(Cafe cafe)
        {
            _context.Cafes.Add(cafe);
            await _context.SaveChangesAsync();
            return cafe.Id;
        }

        public async Task<bool> UpdateAsync(Cafe cafe)
        {
            _context.Cafes.Update(cafe);
            var rowsAffected = await _context.SaveChangesAsync();
            return rowsAffected > 0;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var cafe = await GetByIdAsync(id);
            if (cafe == null) return false;

            _context.Cafes.Remove(cafe);
            var rowsAffected = await _context.SaveChangesAsync();
            return rowsAffected > 0;
        }
    }
}
