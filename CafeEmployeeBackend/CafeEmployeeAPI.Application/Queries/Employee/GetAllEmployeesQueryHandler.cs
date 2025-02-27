using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CafeEmployeeAPI.Application.Interfaces.Repositories;
using CafeEmployeeAPI.Application.DTOs;
using Microsoft.EntityFrameworkCore;




namespace CafeEmployeeAPI.Application.Queries.Employee
{
    public class GetAllEmployeesQueryHandler : IRequestHandler<GetAllEmployeesQuery, List<EmployeeDto>>
    {
        private readonly IEmployeeRepository _employeeRepository;

        public GetAllEmployeesQueryHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<List<EmployeeDto>> Handle(GetAllEmployeesQuery request, CancellationToken cancellationToken)
        {
            var employees = await _employeeRepository.GetAllAsync(cancellationToken);
            
            //return by highest number of working days of employee 
            return employees.OrderBy(c => c.StartDate).Select(e => new EmployeeDto
            {
                Id = e.Id,
                Name = e.Name,
                EmailAddress = e.EmailAddress,
                PhoneNumber = e.PhoneNumber,
                DaysWorked = (int)(DateTime.UtcNow - e.StartDate).TotalDays,
                Cafe = e.Cafe?.Name ?? string.Empty
            }).ToList();
        }
    }
}
