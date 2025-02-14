using System;
using System.ComponentModel.DataAnnotations;

namespace backend.Models
{
    public class Employee
    {
        [Key]
        [Required]
        [RegularExpression(@"^UI[A-Za-z0-9]{7}$", ErrorMessage = "Invalid ID format.")]
        public string Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Name is required.")]
        public string Name { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Invalid email address format.")]
        public string EmailAddress { get; set; }

        [Required]
        [RegularExpression(@"^[89]\d{7}$", ErrorMessage = "Invalid phone number format.")]
        public string PhoneNumber { get; set; }

        [Required]
        [EnumDataType(typeof(Gender))]
        public Gender Gender { get; set; }

        public string CafeId { get; set; }
        public Cafe Cafe { get; set; }

        public DateTime StartDate { get; set; }
    }

    public enum Gender
    {
        Male,
        Female
    }
}
