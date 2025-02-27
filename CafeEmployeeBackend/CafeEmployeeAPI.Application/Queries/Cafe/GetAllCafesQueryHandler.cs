using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CafeEmployeeAPI.Application.Interfaces.Repositories;
using CafeEmployeeAPI.Application.DTOs;

namespace CafeEmployeeAPI.Application.Queries.Cafe
{
    public class GetAllCafesQueryHandler : IRequestHandler<GetAllCafesQuery, List<CafeDto>>
    {
        private readonly ICafeRepository _cafeRepository;

        public GetAllCafesQueryHandler(ICafeRepository cafeRepository)
        {
            _cafeRepository = cafeRepository;
        }

        public async Task<List<CafeDto>> Handle(GetAllCafesQuery request, CancellationToken cancellationToken)
        {
            var cafes = await _cafeRepository.GetAllAsync();

            if (!string.IsNullOrEmpty(request.Location))
            {
                cafes = cafes.Where(c => c.Location == request.Location).ToList();
            }
            else
            {
                cafes = cafes.ToList();
            }

            return cafes.OrderByDescending(c => c.Employees.Count)
                        .Select(c => new CafeDto
                        {
                            Id = c.Id.ToString(),
                            Name = c.Name,
                            Description = c.Description,
                            Employees = c.Employees.Count,
                            Location = c.Location,
                            Logo = c.Logo
                        }).ToList();
        }
    }
}
