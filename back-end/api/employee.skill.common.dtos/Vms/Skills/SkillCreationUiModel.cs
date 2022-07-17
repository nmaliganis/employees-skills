using System.ComponentModel.DataAnnotations;

namespace employee.skill.common.dtos.Vms.Skills
{
    public class SkillCreationUiModel
    {
        [Required(AllowEmptyStrings = false)]
        [Editable(true)]
        public string Name { get; set; }
        [Required(AllowEmptyStrings = false)]
        [Editable(true)]
        public string Description { get; set; }
    }
}