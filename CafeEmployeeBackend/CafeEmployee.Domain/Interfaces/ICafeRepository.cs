using CafeEmployee.Domain.Entities;
namespace CafeEmployee.Domain.Interfaces
{
    public interface ICafeRepository
    {
        Task AddAsync(Cafe cafe);
        Task<List<Cafe>> GetAllAsync();
    }
}
