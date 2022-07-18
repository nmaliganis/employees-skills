using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace employee.skill.fe.Models.DTOs.Employees
{
    public class EmployeeDto
    {
        [Editable(true)] public Guid Id { get; set; }
        public string Message { get; set; }

        [Required] [Editable(true)]
        public string Firstname { get; set; }
        [Required] [Editable(true)]
        public string Lastname { get; set; }
        [Required] [Editable(true)]
        public string Email { get; set; }
        [Required(AllowEmptyStrings = false)]
        [Editable(true)]
        public List<Guid> ExistingSkillIds { get; set; }

        [Editable(true)] public string NonExistingSkill { get; set; } = string.Empty;
        [Required] [Editable(true)]
        public DateTime CreatedDate { get; set; }
        [Required] [Editable(true)]
        public DateTime HiredDate { get; set; } = DateTime.Now;
        [Required] [Editable(true)]
        public bool Active { get; set; }
    }
}
