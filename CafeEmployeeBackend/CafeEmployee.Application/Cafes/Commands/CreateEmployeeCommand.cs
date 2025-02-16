using MediatR;

namespace CafeEmployee.Application.Commands
{
    public class CreateEmployeeCommand : IRequest<EmployeeDto>
    {
        public string Name { get; set; }
        public string Position { get; set; }
        public int Age { get; set; }
    }
}
