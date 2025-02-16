using Microsoft.AspNetCore.Mvc;
using MediatR;
using CafeEmployee.Application.Commands;
using CafeEmployee.Application.Queries;
using CafeEmployee.Dtos;

namespace CafeEmployeeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EmployeeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // POST: api/Employee
        [HttpPost]
        public async Task<IActionResult> CreateEmployee(CreateEmployeeCommand command)
        {
            var employee = await _mediator.Send(command);
            return Ok(employee);
        }

        // GET: api/Employee
        [HttpGet]
        public async Task<IActionResult> GetEmployees([FromQuery] GetEmployeeQuery query)
        {
            var employees = await _mediator.Send(query);
            return Ok(employees);
        }
    }
}
