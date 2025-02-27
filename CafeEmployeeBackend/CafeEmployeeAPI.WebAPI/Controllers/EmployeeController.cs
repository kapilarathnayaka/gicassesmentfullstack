using Microsoft.AspNetCore.Mvc;
using CafeEmployeeAPI.Application.Queries.Cafe;
using CafeEmployeeAPI.Application.DTOs;
using CafeEmployeeAPI.Application.Commands.Employee;
using CafeEmployeeAPI.Application.Queries.Employee;
using MediatR;
using System;
using System.Net.Http;
using CafeEmployeeAPI.Application.Exceptions;


[ApiController]
[Route("api/employees")]
public class EmployeeController : ControllerBase
{
    private readonly IMediator _mediator;

    public EmployeeController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<EmployeeDto>> GetEmployeeById(string id)
    {
        var query = new GetEmployeeByIdQuery(id);
        var employee = await _mediator.Send(query);
        return Ok(employee);
    }

    [HttpGet]
    public async Task<ActionResult<List<EmployeeDto>>> GetEmployees()
    {
            var query = new GetAllEmployeesQuery();
            var employees = await _mediator.Send(query);
            return Ok(employees);
    }

    [HttpPost]
    public async Task<ActionResult> CreateEmployee(CreateEmployeeCommand command)
    {
        var empCommand = new CreateEmployeeCommand
        (
            command.Name,
            command.EmailAddress,
            command.PhoneNumber,
            command.Gender,
            command.CafeId
        );
        await _mediator.Send(empCommand);
        return CreatedAtAction(nameof(GetEmployees), new { cafe = command.CafeId }, new { success = true, message = "Employee created successfully!" });
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<string>> UpdateEmployee(string id,UpdateEmployeeCommand command)
    {
        command.Id = id;  // Ensure the id is part of the command
        try
        {
            var result = await _mediator.Send(command);
            return Ok(new { success = true, message = "Employee updated successfully!", id = result.Id });
        }
        catch (NotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (BusinessRuleException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception)
        {
            return StatusCode(500, "An unexpected error occurred.");
        }        
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<bool>> DeleteEmployee(string id)
    {
        var command = new DeleteEmployeeCommand(id);
        var response = await _mediator.Send(command);
     return Ok(new { success = true, message = "Employee deleted successfully!"});
    }

    [HttpGet("by-cafe/{cafeId}")]
    public async Task<ActionResult<List<EmployeeDto>>> GetEmployeesByCafe(string cafeId)
    {
        var query = new GetEmployeesByCafeQuery(cafeId);
        var employees = await _mediator.Send(query);
        Console.WriteLine(employees);
        return Ok(employees);
    }
}
