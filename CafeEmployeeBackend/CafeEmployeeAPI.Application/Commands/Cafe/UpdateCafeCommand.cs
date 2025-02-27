using MediatR;
using CafeEmployeeAPI.Application.DTOs;

namespace CafeEmployeeAPI.Application.Commands.Cafe
{
public class UpdateCafeCommand : IRequest<CafeDto>
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Location { get; set; }
    public string? Logo { get; set; }

    public UpdateCafeCommand(string id, string name, string description, string location, string logo)
    {
        Id = id;
        Name = name;
        Description = description;
        Location = location;
        Logo = logo;
    }
}
}
