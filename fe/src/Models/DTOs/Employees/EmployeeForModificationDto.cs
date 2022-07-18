using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace employee.skill.fe.Models.DTOs.Employees
{
  public class EmployeeForModificationDto
  {
    public Guid Id { get; set; }

    [Required(ErrorMessage = "Enter a Firstname")]
    [StringLength(10, ErrorMessage = "That Firstname is too long")]
    public string Firstname { get; set; }

    [Required(ErrorMessage = "Enter a Lastname")]
    [StringLength(10, ErrorMessage = "That Lastname is too long")]
    public string Lastname { get; set; }

    [Required(ErrorMessage = "Enter a Email")]
    [DataType(DataType.EmailAddress)]
    [EmailAddress]
    public string Email { get; set; }
    
    [Required(ErrorMessage = "Skills are Required")]
    [Editable(true)]
    public List<Guid> ExistingSkillIds { get; set; } = new List<Guid>();
    
    [Editable(true)]
    public string NonExistingSkill { get; set; }

    [Required(ErrorMessage = "Hired Date is Required")]
    public DateTime HiredDate { get; set; } = DateTime.Now;
  }
}