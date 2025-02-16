using MediatR;
using System;

namespace CafeEmployee.Application.Features.Employees.Commands
{
    public class UpdateEmployeeCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public int Age { get; set; }
    }
}
