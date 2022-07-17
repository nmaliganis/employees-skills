using System;

namespace employee.skill.fe.Store.Skills.Actions.Delete
{
  public class DeleteSkillAction
  {
    public Guid SkillToBeDeletedId { get; private set; }

    public DeleteSkillAction(Guid skillToBeDeletedId)
    {
      SkillToBeDeletedId = skillToBeDeletedId;
    }
  }
}