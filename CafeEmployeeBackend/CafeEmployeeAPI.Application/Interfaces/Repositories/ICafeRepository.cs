using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CafeEmployeeAPI.Domain.Entities;

namespace CafeEmployeeAPI.Application.Interfaces.Repositories
{
    public interface ICafeRepository
    {
        Task<Cafe?> GetByIdAsync(Guid id);
        Task<IEnumerable<Cafe>> GetAllAsync();
        Task<IEnumerable<Cafe>> GetByLocationAsync(string location);
        Task AddAsync(Cafe cafe);
        Task UpdateAsync(Cafe cafe);
        Task DeleteAsync(Cafe cafe);
    }
}
