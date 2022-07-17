using System;

namespace employee.skill.fe.Store.Skills.Actions.Delete
{
  public class DeleteSkillSuccessAction
  {
    public Guid SkillHaveBeenDeletedId { get; private set; }
    public string SkillDeletionStatus { get; private set; }

    public DeleteSkillSuccessAction(Guid SkillHaveBeenDeletedId, string SkillDeletionStatus)
    {
      SkillHaveBeenDeletedId = SkillHaveBeenDeletedId;
      SkillDeletionStatus = SkillDeletionStatus;
    }
  }
}