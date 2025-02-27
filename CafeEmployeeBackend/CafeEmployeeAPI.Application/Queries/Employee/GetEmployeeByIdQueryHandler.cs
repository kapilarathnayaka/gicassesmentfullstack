using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CafeEmployeeAPI.Application.Interfaces.Repositories;
using CafeEmployeeAPI.Application.DTOs;

namespace CafeEmployeeAPI.Application.Queries.Employee
{
public class GetEmployeeByIdQueryHandler : IRequestHandler<GetEmployeeByIdQuery, EmployeeDto>
{
    private readonly IEmployeeRepository _employeeRepository;

    public GetEmployeeByIdQueryHandler(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public async Task<EmployeeDto> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
    {
        var employee = await _employeeRepository.GetByIdAsync(request.Id);
        if (employee == null)
        {
            throw new Exception("Employee not found.");
        }
        

        return new EmployeeDto
        {
            Id = employee.Id,
            Name = employee.Name,
            EmailAddress = employee.EmailAddress,
            PhoneNumber = employee.PhoneNumber,
            DaysWorked = (int)(DateTime.UtcNow - employee.StartDate).TotalDays,
            // Cafe = employee.Cafe?.Name ?? string.Empty,
            CafeId = employee.Cafe?.Id.ToString(),
            Gender = employee.Gender
        };
    }
}
}