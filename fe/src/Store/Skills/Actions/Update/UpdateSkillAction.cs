using System;
using employee.skill.fe.Models.DTOs.Skills;

namespace employee.skill.fe.Store.Skills.Actions.Update
{
  public class UpdateSkillAction
  {
    public Guid SkillToBeUpdateId { get; private set; }
    public SkillForModificationDto SkillForModificationDto { get; private set; }

    public UpdateSkillAction(Guid SkillToBeUpdateId, SkillForModificationDto SkillForModificationDto)
    {
      SkillToBeUpdateId = SkillToBeUpdateId;
      SkillForModificationDto = SkillForModificationDto;
    }
  }
}