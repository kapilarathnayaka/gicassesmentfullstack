using MediatR;

namespace CafeEmployee.Application.Features.Cafes.Commands
{
    public class CreateCafeCommand : IRequest<CafeDto>
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
    }
}
