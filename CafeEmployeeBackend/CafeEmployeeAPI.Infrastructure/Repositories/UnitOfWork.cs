using CafeEmployeeAPI.Application.Interfaces;
using CafeEmployeeAPI.Application.Interfaces.Repositories;
using CafeEmployeeAPI.Infrastructure.Repositories;
using System.Threading.Tasks;

namespace CafeEmployeeAPI.Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public IEmployeeRepository Employees { get; }
        public ICafeRepository Cafes { get; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Employees = new EmployeeRepository(_context);
            Cafes = new CafeRepository(_context);
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
