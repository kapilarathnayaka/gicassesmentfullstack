using MediatR;
using System.Threading;
using System.Threading.Tasks;
using CafeEmployeeAPI.Domain.Entities;
using CafeEmployeeAPI.Application.Interfaces.Repositories;
using CafeEmployeeAPI.Application.DTOs;

namespace CafeEmployeeAPI.Application.Commands.Cafe
{
    public class CreateCafeCommandHandler : IRequestHandler<CreateCafeCommand, CafeDto>
    {
        private readonly ICafeRepository _cafeRepository;

        public CreateCafeCommandHandler(ICafeRepository cafeRepository)
        {
            _cafeRepository = cafeRepository;
        }

        public async Task<CafeDto> Handle(CreateCafeCommand request, CancellationToken cancellationToken)
        {
            var cafe = new CafeEmployeeAPI.Domain.Entities.Cafe
            {
                Id = Guid.NewGuid(), //you can let the db to add this without setting a value here
                Name = request.Name,
                Description = request.Description,
                Location = request.Location,
                Logo = request.Logo
            };

            await _cafeRepository.AddAsync(cafe);
            return new CafeDto
        {
            Id = cafe.Id.ToString(),
            Name = cafe.Name,
            Location = cafe.Location,
            Description = cafe.Description,
            Logo  = cafe.Logo
        };
        }
    }
}
