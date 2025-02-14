using System;
using System.ComponentModel.DataAnnotations;

namespace backend.Models
{
    public class Cafe
    {
        [Key]
        [Required]
        public Guid Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Name is required.")]
        public string Name { get; set; }

        [Required]
        [StringLength(256, ErrorMessage = "Description cannot exceed 256 characters.")]
        public string Description { get; set; }

        public string Logo { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Location is required.")]
        public string Location { get; set; }
    }
}
