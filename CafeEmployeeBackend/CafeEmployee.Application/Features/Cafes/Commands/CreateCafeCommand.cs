using MediatR;

namespace CafeEmployee.Application.Commands
{
    public class CreateCafeCommand : IRequest<CafeDto>
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
    }
}
