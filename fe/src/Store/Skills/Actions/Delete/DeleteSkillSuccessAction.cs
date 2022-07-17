using System;

namespace employee.skill.fe.Store.Skills.Actions.Delete
{
  public class DeleteSkillSuccessAction
  {
    public Guid SkillHaveBeenDeletedId { get; private set; }
    public string SkillDeletionStatus { get; private set; }

    public DeleteSkillSuccessAction(Guid skillHaveBeenDeletedId, string skillDeletionStatus)
    {
      SkillHaveBeenDeletedId = skillHaveBeenDeletedId;
      SkillDeletionStatus = skillDeletionStatus;
    }
  }
}