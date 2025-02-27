using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using CafeEmployeeAPI.Domain.Entities;        // Assuming Employee is in the Domain.Entities namespace
using CafeEmployeeAPI.Application.Interfaces.Repositories;      // Assuming IEmployeeRepository is in the Domain.Interfaces namespace

namespace CafeEmployeeAPI.Application.Commands.Employee
{
public class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand, bool>
{
    private readonly IEmployeeRepository _employeeRepository;

    public DeleteEmployeeCommandHandler(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public async Task<bool> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
    {
        var employee = await _employeeRepository.GetByIdAsync(request.Id);
        if (employee == null)
        {
            throw new Exception("Employee not found.");
        }

        await _employeeRepository.DeleteAsync(employee);
        return true;
    }
}
}