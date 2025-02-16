using Microsoft.AspNetCore.Mvc;
using MediatR;
using CafeEmployee.Application.Commands;
using CafeEmployee.Application.Queries;
using CafeEmployee.Dtos;

namespace CafeEmployeeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CafeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CafeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // POST: api/Cafe
        [HttpPost]
        public async Task<IActionResult> CreateCafe(CreateCafeCommand command)
        {
            var cafe = await _mediator.Send(command);
            return Ok(cafe);
        }

        // GET: api/Cafe
        [HttpGet]
        public async Task<IActionResult> GetCafes([FromQuery] GetCafesQuery query)
        {
            var cafes = await _mediator.Send(query);
            return Ok(cafes);
        }
    }
}
