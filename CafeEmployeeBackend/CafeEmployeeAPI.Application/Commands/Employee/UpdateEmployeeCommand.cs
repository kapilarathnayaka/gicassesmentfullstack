using MediatR;
using CafeEmployeeAPI.Application.DTOs;

namespace CafeEmployeeAPI.Application.Commands.Employee
{
public class UpdateEmployeeCommand : IRequest<EmployeeDto>
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string EmailAddress { get; set; }
    public string PhoneNumber { get; set; }
    public string Gender { get; set; }
    public string CafeId { get; set; }

    public UpdateEmployeeCommand(string id, string name, string emailAddress, string phoneNumber, string gender, string cafeId)
    {
        Id = id;
        Name = name;
        EmailAddress = emailAddress;
        PhoneNumber = phoneNumber;
        Gender = gender;
        CafeId = cafeId;
    }
}
}