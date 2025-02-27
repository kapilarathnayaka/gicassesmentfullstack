using MediatR;
using CafeEmployeeAPI.Application.DTOs;

namespace CafeEmployeeAPI.Application.Commands.Cafe
{
    public class CreateCafeCommand : IRequest<CafeDto> // Make sure it's IRequest<Unit>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string? Logo { get; set; }
    

    public CreateCafeCommand(string name,string description, string location, string logo)
    {
        Name = name;
        Description = description;
        Location = location;
        Logo = logo;
    }
}
}
