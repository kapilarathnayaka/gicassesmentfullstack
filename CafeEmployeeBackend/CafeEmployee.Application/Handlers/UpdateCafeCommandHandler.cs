using MediatR;
using CafeEmployee.Domain.Entities;
using CafeEmployee.Domain.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace CafeEmployee.Application.Features.Cafes.Handlers
{
    public class UpdateCafeCommandHandler : IRequestHandler<UpdateCafeCommand, bool>
    {
        private readonly ICafeRepository _cafeRepository;

        public UpdateCafeCommandHandler(ICafeRepository cafeRepository)
        {
            _cafeRepository = cafeRepository;
        }

        public async Task<bool> Handle(UpdateCafeCommand request, CancellationToken cancellationToken)
        {
            var cafe = await _cafeRepository.GetByIdAsync(request.Id);
            if (cafe == null) return false;

            cafe.Name = request.Name;
            cafe.Location = request.Location;

            await _cafeRepository.UpdateAsync(cafe);
            return true;
        }
    }
}
