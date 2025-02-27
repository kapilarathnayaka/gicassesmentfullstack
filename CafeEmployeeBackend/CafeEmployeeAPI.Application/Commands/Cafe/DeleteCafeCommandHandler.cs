using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using CafeEmployeeAPI.Domain.Entities;
using CafeEmployeeAPI.Application.Interfaces.Repositories;

namespace CafeEmployeeAPI.Application.Commands.Cafe
{
public class DeleteCafeCommandHandler : IRequestHandler<DeleteCafeCommand, bool>
{
    private readonly ICafeRepository _cafeRepository;

    public DeleteCafeCommandHandler(ICafeRepository cafeRepository)
    {
        _cafeRepository = cafeRepository;
    }

    public async Task<bool> Handle(DeleteCafeCommand request, CancellationToken cancellationToken)
    {
        var cafe = await _cafeRepository.GetByIdAsync(new Guid(request.Id));
        if (cafe == null)
        {
            throw new Exception("Cafe not found.");
        }

        await _cafeRepository.DeleteAsync(cafe);
        return true;
    }}
}