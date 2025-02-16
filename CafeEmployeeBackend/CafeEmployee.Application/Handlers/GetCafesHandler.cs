using MediatR;
using CafeEmployee.Dtos;
using CafeEmployee.Domain.Interfaces;

namespace CafeEmployee.Application.Handlers
{
    public class GetCafesHandler : IRequestHandler<GetCafesQuery, List<CafeDto>>
    {
        private readonly ICafeRepository _cafeRepository;
        private readonly IMapper _mapper;

        public GetCafesHandler(ICafeRepository cafeRepository, IMapper mapper)
        {
            _cafeRepository = cafeRepository;
            _mapper = mapper;
        }

        public async Task<List<CafeDto>> Handle(GetCafesQuery request, CancellationToken cancellationToken)
        {
            var cafes = await _cafeRepository.GetAllAsync();
            return _mapper.Map<List<CafeDto>>(cafes);
        }
    }
}
