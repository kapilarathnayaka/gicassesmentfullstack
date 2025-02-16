using CafeEmployee.Dtos;
using MediatR;

namespace CafeEmployee.Application.Queries
{
    public class GetCafesQuery : IRequest<List<CafeDto>>
    {
        public string Location { get; set; }
    }

    public class GetCafesQueryHandler : IRequestHandler<GetCafesQuery, List<CafeDto>>
    {
        private readonly ICafeRepository _cafeRepository;

        public GetCafesQueryHandler(ICafeRepository cafeRepository)
        {
            _cafeRepository = cafeRepository;
        }

        public async Task<List<CafeDto>> Handle(GetCafesQuery request, CancellationToken cancellationToken)
        {
            var cafes = await _cafeRepository.GetCafesAsync(request.Location);

            return cafes.Select(c => new CafeDto
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description,
                Location = c.Location
            }).ToList();
        }
    }
}
