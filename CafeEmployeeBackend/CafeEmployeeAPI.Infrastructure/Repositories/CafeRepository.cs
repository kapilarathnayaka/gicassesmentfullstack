using CafeEmployeeAPI.Application.Interfaces.Repositories;
using CafeEmployeeAPI.Domain.Entities;
using CafeEmployeeAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CafeEmployeeAPI.Infrastructure.Repositories
{
    public class CafeRepository : ICafeRepository
    {
        private readonly ApplicationDbContext _context;

        public CafeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Cafe?> GetByIdAsync(Guid id)
        {
            return await _context.Cafes.FindAsync(id);
        }

        public async Task<IEnumerable<Cafe>> GetAllAsync()
        {
            return await _context.Cafes.ToListAsync();
        }

        public async Task<IEnumerable<Cafe>> GetByLocationAsync(string location)
        {
            return await _context.Cafes.Where(c => c.Location == location).ToListAsync();
        }

        public async Task AddAsync(Cafe cafe)
        {
            await _context.Cafes.AddAsync(cafe);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Cafe cafe)
        {
            _context.Cafes.Update(cafe);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Cafe cafe)
        {
            _context.Cafes.Remove(cafe);
            await _context.SaveChangesAsync();
        }
    }
}
