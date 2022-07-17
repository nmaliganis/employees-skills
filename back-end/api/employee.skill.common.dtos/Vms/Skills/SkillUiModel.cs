using System;
using System.ComponentModel.DataAnnotations;
using employee.skill.common.dtos.Vms.Base;

namespace employee.skill.common.dtos.Vms.Skills
{
    public class SkillUiModel : IUiModel
    {
        [Required(AllowEmptyStrings = false)]
        [Editable(true)]
        public string Name { get; set; }
        [Required(AllowEmptyStrings = false)]
        [Editable(true)]
        public string Description { get; set; }
        [Editable(true)]
        public Guid Id { get; set; }

        public string Message { get; set; }

        [Required]
        [Editable(true)]
        public DateTime CreatedDate { get; set; }
        [Required]
        [Editable(true)]
        public bool Active { get; set; }
    }
}