using MediatR;
using System.Collections.Generic;
using CafeEmployeeAPI.Application.DTOs;

namespace CafeEmployeeAPI.Application.Queries.Cafe
{
public class GetCafeByIdQuery : IRequest<CafeDto>
{
    public string Id { get; set; }

    public GetCafeByIdQuery(string id)
    {
        Id = id;
    }


    
}
}


