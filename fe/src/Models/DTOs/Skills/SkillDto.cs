using System;
using System.ComponentModel.DataAnnotations;

namespace employee.skill.fe.Models.DTOs.Skills
{
    public class SkillDto
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
