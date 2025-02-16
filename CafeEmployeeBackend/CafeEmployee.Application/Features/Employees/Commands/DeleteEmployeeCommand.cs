using MediatR;

namespace CafeEmployee.Application.Features.Cafes.Commands
{
    public class UpdateCafeCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
    }
}
