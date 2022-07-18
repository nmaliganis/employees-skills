using System;
using System.Collections.Generic;
using employee.skill.fe.Models.DTOs.Skills;
using employee.skill.fe.Store.Statuses;

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
    public CreationStatus CreationStatus { get; }
    public ModificationStatus ModificationStatus { get; }
    public DeletionStatus DeletionStatus { get; }

    public SkillState(
      List<SkillDto> skillList, 
      string errorMessage, 
      bool isLoading,
      SkillDto skill, 
      SkillForCreationDto skillToBeCreatedPayload, 
      SkillForModificationDto skillToBeUpdatePayload, 
      Guid skillId,
      CreationStatus creationStatus,
      ModificationStatus modificationStatus,
      DeletionStatus deletionStatus
    )
    {
      SkillList  = skillList;
      ErrorMessage = errorMessage;
      IsLoading = isLoading;
      Skill = skill;
      SkillToBeCreatedPayload = skillToBeCreatedPayload;
      SkillToBeUpdatePayload = skillToBeUpdatePayload;
      SkillId = skillId;
      CreationStatus = creationStatus;
      ModificationStatus = modificationStatus;
      DeletionStatus = deletionStatus;
    }
  }
}