using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CafeEmployeeAPI.Application.Interfaces.Repositories;
using CafeEmployeeAPI.Application.DTOs;

namespace CafeEmployeeAPI.Application.Queries.Cafe
{
public class GetEmployeesByCafeQueryHandler : IRequestHandler<GetEmployeesByCafeQuery, List<EmployeeDto>>
{
    private readonly IEmployeeRepository _employeeRepository;

    public GetEmployeesByCafeQueryHandler(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public async Task<List<EmployeeDto>> Handle(GetEmployeesByCafeQuery request,CancellationToken cancellationToken)
    {
        // Fetch employees related to the specified cafe
        var employees = await _employeeRepository.GetByCafeIdAsync(request.CafeId);

        return employees.OrderBy(c => c.StartDate).Select(e => new EmployeeDto
            {
                Id = e.Id,
                Name = e.Name,
                EmailAddress = e.EmailAddress,
                PhoneNumber = e.PhoneNumber,
                DaysWorked = (int)(DateTime.UtcNow - e.StartDate).TotalDays,
                Cafe = e.Cafe?.Name ?? string.Empty
            }).ToList();

        // Map the list of employees to EmployeeDto
        // return employees.Select(e => new EmployeeDto
        // {
        //     Id = e.Id.ToString(),
        //     Name = e.Name,
        //     EmailAddress = e.EmailAddress,
        //     PhoneNumber = e.PhoneNumber,
        //     DaysWorked = (int)(DateTime.UtcNow - e.StartDate).TotalDays,
        //     Cafe = e.Cafe?.Name ?? string.Empty
        // }).ToList();
    }
}
}