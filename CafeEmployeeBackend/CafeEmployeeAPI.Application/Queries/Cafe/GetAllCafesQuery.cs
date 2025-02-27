using MediatR;
using System.Collections.Generic;
using CafeEmployeeAPI.Application.DTOs;

namespace CafeEmployeeAPI.Application.Queries.Cafe
{
    public class GetAllCafesQuery : IRequest<List<CafeDto>>
    {
        public string? Location { get; set; }
    }
}
