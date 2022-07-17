using System;
using System.ComponentModel.DataAnnotations;

namespace employee.skill.fe.Models.DTOs.Employees
{
    public class EmployeeDto
    {
        public Guid Id { get; set; }
        public string Message { get; set; }

        [Required(ErrorMessage = "Enter a Firstname")]
        [StringLength(10, ErrorMessage = "That Firstname is too long")]
        public string Firstname { get; set; }

        [Required(ErrorMessage = "Enter a Lastname")]
        [StringLength(10, ErrorMessage = "That Lastname is too long")]
        public string Lastname { get; set; }

        [Required(ErrorMessage = "Enter a Email")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
