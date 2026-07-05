using System;
using System.ComponentModel.DataAnnotations;

namespace web_token_Di.Models.DTOs
{
    public class EmployeeModel 
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }

        [Required]
        public string? Email { get; set; }

        [Required]
        public string? Mobile { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [Required]
        public Char Gender { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public Guid TenantId { get; set; }
    }
}
