using MediatR;

namespace CafeEmployee.Application.Features.Employees.Queries
{
    public class GetEmployeeQuery : IRequest<List<EmployeeDto>>
    {
        public string Position { get; set; }
    }
}
