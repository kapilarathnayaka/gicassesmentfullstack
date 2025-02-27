using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using CafeEmployeeAPI.Domain.Entities;
using CafeEmployeeAPI.Application.Interfaces.Repositories;
using CafeEmployeeAPI.Application.DTOs;

namespace CafeEmployeeAPI.Application.Commands.Cafe
{
public class UpdateCafeCommandHandler : IRequestHandler<UpdateCafeCommand, CafeDto>
{
    private readonly ICafeRepository _cafeRepository;

    public UpdateCafeCommandHandler(ICafeRepository cafeRepository)
    {
        _cafeRepository = cafeRepository;
    }

    public async Task<CafeDto> Handle(UpdateCafeCommand request, CancellationToken cancellationToken)
    {
        var cafe = await _cafeRepository.GetByIdAsync(new Guid(request.Id));
        if (cafe == null)
        {
            throw new Exception("Cafe not found.");
        }

        cafe.Name = request.Name;
        cafe.Description = request.Description;
        cafe.Location = request.Location;
        cafe.Logo = request.Logo;

        await _cafeRepository.UpdateAsync(cafe);
        return new CafeDto
        {
            Id = cafe.Id.ToString(),
            Name = cafe.Name,
            Location = cafe.Location,
            Description = cafe.Description,
            Logo = cafe.Logo
        };
    }
}
}