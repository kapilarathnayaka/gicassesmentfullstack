using MediatR;

namespace CafeEmployee.Application.Queries
{
    public class GetEmployeeQuery : IRequest<List<EmployeeDto>>
    {
        public string Position { get; set; }
    }
}
