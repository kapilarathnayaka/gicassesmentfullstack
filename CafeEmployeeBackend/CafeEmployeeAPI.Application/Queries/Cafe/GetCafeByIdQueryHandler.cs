using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CafeEmployeeAPI.Application.Interfaces.Repositories;
using CafeEmployeeAPI.Application.DTOs;

namespace CafeEmployeeAPI.Application.Queries.Cafe
{
public class GetCafeByIdQueryHandler : IRequestHandler<GetCafeByIdQuery, CafeDto>
{
    private readonly ICafeRepository _cafeRepository;

    public GetCafeByIdQueryHandler(ICafeRepository cafeRepository)
    {
        _cafeRepository = cafeRepository;
    }

    public async Task<CafeDto> Handle(GetCafeByIdQuery request, CancellationToken cancellationToken)
    {
        var cafe = await _cafeRepository.GetByIdAsync(new System.Guid(request.Id));
        if (cafe == null)
        {
            throw new Exception("Cafe not found.");
        }

        return new CafeDto
        {
            Id = cafe.Id.ToString(),
            Name = cafe.Name,
            Description = cafe.Description,
            Location = cafe.Location,
            Employees = cafe.Employees.Count,
            Logo = cafe.Logo
        };
    }
}
}