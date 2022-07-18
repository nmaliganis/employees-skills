using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace employee.skill.common.dtos.Vms.Employees
{
    public class EmployeeModificationUiModel
    {
        [Required(AllowEmptyStrings = false)]
        [Editable(true)]
        public string Firstname { get; set; }
    
        [Required(AllowEmptyStrings = false)]
        [Editable(true)]
        public string Lastname { get; set; }

        [Required(AllowEmptyStrings = false)]
        [Editable(true)]
        public string Email { get; set; }
        
        [Required]
        [Editable(true)]
        public DateTime HiredDate { get; set; }
    
        [Required(AllowEmptyStrings = false)]
        [Editable(true)]
        public List<Guid> ExistingSkillIds { get; set; }
    
        [Editable(true)]
        public string NonExistingSkill { get; set; }
    }
}