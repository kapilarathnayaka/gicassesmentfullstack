using MediatR;
using System.Collections.Generic;
using CafeEmployeeAPI.Application.DTOs;

namespace CafeEmployeeAPI.Application.Queries.Employee
{
    public class GetAllEmployeesQuery : IRequest<List<EmployeeDto>> { 
        public string? CafeId { get; set; }
    }
}
