using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using CafeEmployeeAPI.Domain.Entities;        // Assuming Employee is in the Domain.Entities namespace
using CafeEmployeeAPI.Application.Interfaces.Repositories;  
using CafeEmployeeAPI.Application.DTOs;        // Assuming EmployeeDto is in the Application.DTOs namespace
namespace CafeEmployeeAPI.Application.Commands.Employee
{
    public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, EmployeeDto>
    {
        private readonly IEmployeeRepository _employeeRepository;

        public CreateEmployeeCommandHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<EmployeeDto> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var newEmployee = new CafeEmployeeAPI.Domain.Entities.Employee
            {
                Id = "UI" + Guid.NewGuid().ToString("N").Substring(0, 7),
                Name = request.Name,
                EmailAddress = request.EmailAddress,
                PhoneNumber = request.PhoneNumber,
                Gender = request.Gender,
                // CafeId = new Guid(request.CafeId),
                //CafeId = string.IsNullOrEmpty(request.CafeId) ? null : new Guid(request.CafeId),
                StartDate = DateTime.UtcNow
            };

            await _employeeRepository.AddAsync(newEmployee);
            return new EmployeeDto
            {
                Id = newEmployee.Id,
                Name = newEmployee.Name,
                EmailAddress = newEmployee.EmailAddress,
                PhoneNumber = newEmployee.PhoneNumber,
                DaysWorked = (int)(DateTime.UtcNow - newEmployee.StartDate).TotalDays,
                Cafe = newEmployee.Cafe?.Name ?? string.Empty,
                Gender = newEmployee.Gender
            };
        }
    }
}
