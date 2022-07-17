using System;
using System.Collections.Generic;
using employee.skill.fe.Models.DTOs.Skills;

namespace employee.skill.fe.Store.Skills
{
  public class SkillState
  {
    public bool IsLoading { get; private set; }
    public string ErrorMessage { get; private set; }
    public List<SkillDto> SkillList { get; private set; }
    public SkillDto Skill { get; private set; }
    public SkillForCreationDto SkillToBeCreatedPayload { get; private set; }
    public SkillForModificationDto SkillToBeUpdatePayload { get; }
    public Guid SkillId { get; }

    public SkillState(
      List<SkillDto> skillList, 
      string errorMessage, 
      bool isLoading,
      SkillDto skill, 
      SkillForCreationDto skillToBeCreatedPayload, 
      SkillForModificationDto skillToBeUpdatePayload, 
      Guid skillId
    )
    {
      SkillList  = skillList;
      ErrorMessage = errorMessage;
      IsLoading = isLoading;
      Skill = skill;
      SkillToBeCreatedPayload = skillToBeCreatedPayload;
      SkillToBeUpdatePayload = skillToBeUpdatePayload;
      SkillId = skillId;
    }
  }
}