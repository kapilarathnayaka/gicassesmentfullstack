using MediatR;

namespace CafeEmployeeAPI.Application.Commands.Cafe
{
public class DeleteCafeCommand : IRequest<bool>
{
    public string Id { get; set; }

    public DeleteCafeCommand(string id)
    {
        Id = id;
    }
}

}
