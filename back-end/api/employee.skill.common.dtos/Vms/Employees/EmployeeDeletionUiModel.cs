using System;
using System.ComponentModel.DataAnnotations;

namespace employee.skill.common.dtos.Vms.Employees
{
    public class EmployeeDeletionUiModel
    {
        [Required]
        [Editable(true)]
        public Guid Id { get; set; }
        [Required]
        [Editable(true)]
        public bool Active { get; set; }
        [Required]
        [Editable(true)]
        public bool DeletionStatus { get; set; }
        [Required]
        [Editable(true)]
        public string Message { get; set; }
    }
}