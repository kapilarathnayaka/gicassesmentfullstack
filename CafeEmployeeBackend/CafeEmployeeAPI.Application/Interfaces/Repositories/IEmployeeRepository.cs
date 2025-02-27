using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CafeEmployeeAPI.Domain.Entities;

namespace CafeEmployeeAPI.Application.Interfaces.Repositories
{
    public interface IEmployeeRepository
    {
        Task<Employee?> GetByIdAsync(string id);
        Task<List<Employee>> GetAllAsync(CancellationToken cancellationToken);
        Task AddAsync(Employee employee);
        Task UpdateAsync(Employee employee);
        Task DeleteAsync(Employee employee);
        Task<List<Employee>> GetByCafeIdAsync(string cafeId);
    }
}
