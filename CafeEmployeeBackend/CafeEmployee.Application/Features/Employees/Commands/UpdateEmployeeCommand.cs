using MediatR;
using System;

namespace CafeEmployee.Application.Commands
{
    public class CreateCafeCommand : IRequest<Guid>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
    }
}
