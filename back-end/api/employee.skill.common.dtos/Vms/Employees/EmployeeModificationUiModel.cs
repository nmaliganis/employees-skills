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
    }
}