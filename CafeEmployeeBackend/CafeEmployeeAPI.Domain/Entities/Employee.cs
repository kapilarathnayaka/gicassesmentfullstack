using System;

namespace CafeEmployeeAPI.Domain.Entities
{
    public class Employee
    {
        public string Id { get; set; } // 'UIXXXXXXX'
        public string Name { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string Gender { get; set; }
        public DateTime StartDate { get; set; }
        public Guid? CafeId { get; set; }
        public Cafe? Cafe { get; set; }
    }
}
