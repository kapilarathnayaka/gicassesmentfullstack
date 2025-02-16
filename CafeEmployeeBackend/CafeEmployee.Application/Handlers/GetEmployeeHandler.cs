using MediatR;
using CafeEmployee.Infrastructure.Repositories;

namespace CafeEmployee.Application.Features.Employees.Handlers
{
    public class GetEmployeeHandler : IRequestHandler<GetEmployeeQuery, EmployeeDto>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public GetEmployeeHandler(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        public async Task<EmployeeDto> Handle(GetEmployeeQuery request, CancellationToken cancellationToken)
        {
            var employee = await _employeeRepository.GetByIdAsync(request.Id);
            return _mapper.Map<EmployeeDto>(employee);
        }
    }
}
