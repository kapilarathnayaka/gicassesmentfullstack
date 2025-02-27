using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CafeEmployeeAPI.Application.Interfaces.Repositories;
using CafeEmployeeAPI.Application.DTOs;

namespace CafeEmployeeAPI.Application.Queries.Cafe
{
public class GetEmployeesByCafeQuery : IRequest<List<EmployeeDto>>
{
    public string CafeId { get; set; }

    public GetEmployeesByCafeQuery(string cafeId)
    {
        CafeId = cafeId;
    }

}
}
