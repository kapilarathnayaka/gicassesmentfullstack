namespace CafeEmployee.Domain.Entities
{
    public class Employee
    {
        public string Id { get; set; } // Format: UIXXXXXXX
        public string Name { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string Gender { get; set; } // Male or Female
    }
}
