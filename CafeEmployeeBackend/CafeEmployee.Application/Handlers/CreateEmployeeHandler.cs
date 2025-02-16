using MediatR;
using CafeEmployee.Infrastructure.Repositories;

namespace CafeEmployee.Application.Features.Employees.Handlers
{
    public class CreateEmployeeHandler : IRequestHandler<CreateEmployeeCommand, Guid>
    {
        private readonly IEmployeeRepository _employeeRepository;

        public CreateEmployeeHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<Guid> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employeeId = await _employeeRepository.CreateEmployeeAsync(request.Name, request.Position, request.Age);
            return employeeId;
        }
    }
}
