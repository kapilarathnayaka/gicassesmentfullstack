using MediatR;

namespace CafeEmployee.Application.Queries
{
    public class GetCafesQuery : IRequest<List<CafeDto>>
    {
        public string Location { get; set; }
    }
}
