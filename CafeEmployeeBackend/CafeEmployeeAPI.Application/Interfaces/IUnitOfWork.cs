using System.Threading.Tasks;
using CafeEmployeeAPI.Application.Interfaces.Repositories;

namespace CafeEmployeeAPI.Application.Interfaces.Repositories
{
    public interface IUnitOfWork
    {
        IEmployeeRepository Employees { get; }
        ICafeRepository Cafes { get; }
        Task<int> CompleteAsync();
    }
}
