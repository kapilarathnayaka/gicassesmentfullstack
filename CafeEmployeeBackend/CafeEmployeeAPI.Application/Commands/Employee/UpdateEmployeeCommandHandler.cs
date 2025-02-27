using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using CafeEmployeeAPI.Domain.Entities;      
using CafeEmployeeAPI.Application.Interfaces.Repositories;     
using CafeEmployeeAPI.Application.DTOs;   
using CafeEmployeeAPI.Application.Exceptions;

namespace CafeEmployeeAPI.Application.Commands.Employee
{
public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand, EmployeeDto>
{
    private readonly IEmployeeRepository _employeeRepository;

    public UpdateEmployeeCommandHandler(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public async Task<EmployeeDto> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
    {
        var employee = await _employeeRepository.GetByIdAsync(request.Id);
        if (employee == null)
        {
            throw new NotFoundException("Employee not found.");
        }

        //check if the existing cafe is different from the new cafe
        if (!string.IsNullOrEmpty(employee.CafeId.ToString()) && employee.CafeId.ToString() != request.CafeId)
        {
            //return message while working on a cafe an employee cannot be transferred to another cafe
            throw new BusinessRuleException("Employee is currently working in a cafe and cannot be transferred to another cafe.");
        }
        employee.Name = request.Name;
        employee.EmailAddress = request.EmailAddress;
        employee.PhoneNumber = request.PhoneNumber;
        employee.Gender = request.Gender;
        employee.CafeId = new Guid(request.CafeId);

        await _employeeRepository.UpdateAsync(employee);
        return new EmployeeDto
        {
            Id = employee.Id,
            Name = employee.Name,
            EmailAddress = employee.EmailAddress,
            PhoneNumber = employee.PhoneNumber,
            DaysWorked = (int)(DateTime.UtcNow - employee.StartDate).TotalDays,
            Cafe = employee.Cafe?.Name ?? string.Empty,
            Gender = employee.Gender
        };
    }
}
}