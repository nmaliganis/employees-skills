using System;
using System.ComponentModel.DataAnnotations;

namespace employee.skill.fe.Models.DTOs.Skills
{
  public class SkillForModificationDto
  {
    public Guid Id { get; set; }

    [Required(ErrorMessage = "Enter a Name")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Enter a Description")]
    public string Description { get; set; }
  }
}