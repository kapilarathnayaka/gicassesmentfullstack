using MediatR;
using System.Collections.Generic;
using CafeEmployeeAPI.Application.DTOs;

namespace CafeEmployeeAPI.Application.Queries.Employee
{
public class GetEmployeeByIdQuery : IRequest<EmployeeDto>
{
    public string Id { get; set; }

    public GetEmployeeByIdQuery(string id)
    {
        Id = id;
    }
}
}
