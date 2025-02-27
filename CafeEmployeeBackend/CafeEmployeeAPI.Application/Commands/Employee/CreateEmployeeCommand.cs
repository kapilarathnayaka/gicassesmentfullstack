using MediatR;
using CafeEmployeeAPI.Application.DTOs;

namespace CafeEmployeeAPI.Application.Commands.Employee
{


    public class CreateEmployeeCommand : IRequest<EmployeeDto>
    {
        public string Name { get; set; } = string.Empty;
        public string EmailAddress { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public string CafeId { get; set; }

        public CreateEmployeeCommand(string name, string emailAddress, string phoneNumber, string gender, string cafeId)
        {
            Name = name;
            EmailAddress = emailAddress;
            PhoneNumber = phoneNumber;
            Gender = gender;
            CafeId = cafeId;
        }
    }
}



