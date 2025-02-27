
using MediatR;

namespace CafeEmployeeAPI.Application.Commands.Employee
{
public class DeleteEmployeeCommand : IRequest<bool>
{
    public string Id { get; set; }

    public DeleteEmployeeCommand(string id)
    {
        Id = id;
    }
}
}