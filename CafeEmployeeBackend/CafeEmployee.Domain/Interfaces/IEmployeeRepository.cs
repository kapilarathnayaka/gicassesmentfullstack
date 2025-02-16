using CafeEmployee.Domain.Entities;

namespace CafeEmployee.Domain.Interfaces
{
    public interface IEmployeeRepository
    {
        Task AddAsync(Employee employee);
        Task<List<Employee>> GetAllAsync();
        Task<Employee?> GetByIdAsync(Guid id);
        Task UpdateAsync(Employee employee);
        Task DeleteAsync(Guid id);
    }
}
