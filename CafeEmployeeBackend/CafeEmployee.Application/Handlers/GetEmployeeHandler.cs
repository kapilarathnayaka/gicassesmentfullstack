using MediatR;
using CafeEmployee.Infrastructure.Repositories;

namespace CafeEmployee.Application.Commands
{
    public class CreateCafeCommandHandler : IRequestHandler<CreateCafeCommand, Guid>
    {
        private readonly ICafeRepository _cafeRepository;

        public CreateCafeCommandHandler(ICafeRepository cafeRepository)
        {
            _cafeRepository = cafeRepository;
        }

        public async Task<Guid> Handle(CreateCafeCommand request, CancellationToken cancellationToken)
        {
            var cafeId = await _cafeRepository.CreateCafeAsync(request.Name, request.Description, request.Location);
            return cafeId;
        }
    }
}
