using Microsoft.AspNetCore.Mvc;
using CafeEmployeeAPI.Application.Commands.Cafe;
using CafeEmployeeAPI.Application.DTOs;
using CafeEmployeeAPI.Application.Queries.Cafe;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;

[ApiController]
[Route("api/cafes")]
public class CafeController : ControllerBase
{
    private readonly IMediator _mediator;

    public CafeController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CafeDto>> GetCafeById(string id)
    {
        var query = new GetCafeByIdQuery(id);
        var cafe = await _mediator.Send(query);
        return Ok(cafe);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Guid>> UpdateCafe(string id,UpdateCafeCommand command)
    {
        command.Id = id;
        var updatedCafe = await _mediator.Send(command);
        return Ok(new { success = true, message = "Cafe updated successfully!", id = updatedCafe.Id });
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<bool>> DeleteCafe(string id)
    {
        var command = new DeleteCafeCommand(id);
        var response = await _mediator.Send(command);
        return Ok(new { success = true, message = "Cafe deleted successfully!" });
    }

    [HttpGet]
    public async Task<ActionResult<List<CafeDto>>> GetCafes(string location = null)
    {
        var query = new GetAllCafesQuery { Location = location };
        var cafes = await _mediator.Send(query);
        return Ok(cafes);
    }

    [HttpPost]
    public async Task<ActionResult> CreateCafe(CreateCafeCommand command)
    {        
        var cafeCommand = new CreateCafeCommand
        (
            command.Name,
            command.Description,
            command.Location,
            command.Logo
        );
        await _mediator.Send(cafeCommand);
        return CreatedAtAction(nameof(GetCafes), new { location = command.Location }, new { success = true, message = "Cafe created successfully!" });
    }    
}
