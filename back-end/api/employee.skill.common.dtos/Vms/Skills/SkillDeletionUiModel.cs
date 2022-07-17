using System;
using System.ComponentModel.DataAnnotations;

namespace employee.skill.common.dtos.Vms.Skills
{
    public class SkillDeletionUiModel
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