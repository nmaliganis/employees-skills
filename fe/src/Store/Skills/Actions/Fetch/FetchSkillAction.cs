using System;

namespace employee.skill.fe.Store.Skills.Actions.Fetch
{
  public class FetchSkillAction
  {
    public Guid SkillToBeFetchedId { get; private set; }

    public FetchSkillAction(Guid skillToBeFetchedId)
    {
      SkillToBeFetchedId = skillToBeFetchedId;
    }
  }
}