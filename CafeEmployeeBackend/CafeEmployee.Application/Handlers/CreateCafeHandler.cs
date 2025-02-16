using MediatR;
using CafeEmployee.Dtos;
using CafeEmployee.Domain.Entities;
using CafeEmployee.Domain.Interfaces;

namespace CafeEmployee.Application.Features.Cafes.Handlers
{
    public class CreateCafeHandler : IRequestHandler<CreateCafeCommand, CafeDto>
    {
        private readonly ICafeRepository _cafeRepository;
        private readonly IMapper _mapper;

        public CreateCafeHandler(ICafeRepository cafeRepository, IMapper mapper)
        {
            _cafeRepository = cafeRepository;
            _mapper = mapper;
        }

        public async Task<CafeDto> Handle(CreateCafeCommand request, CancellationToken cancellationToken)
        {
            var cafe = new Cafe(request.Name, request.Location, request.Description);
            await _cafeRepository.AddAsync(cafe);
            return _mapper.Map<CafeDto>(cafe);
        }
    }
}
