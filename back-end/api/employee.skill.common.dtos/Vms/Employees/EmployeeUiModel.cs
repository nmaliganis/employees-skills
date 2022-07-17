using System;
using System.ComponentModel.DataAnnotations;
using employee.skill.common.dtos.Vms.Base;

namespace employee.skill.common.dtos.Vms.Employees
{
    public class EmployeeUiModel : IUiModel
    {
        [Editable(true)] public Guid Id { get; set; }
        public string Message { get; set; }

        [Required] [Editable(true)]
        public string Firstname { get; set; }
        [Required] [Editable(true)]
        public string Lastname { get; set; }
        [Required] [Editable(true)]
        public string Email { get; set; }
        [Required] [Editable(true)]
        public DateTime CreatedDate { get; set; }
        [Required] [Editable(true)]
        public bool Active { get; set; }
    }
}