using employee.skill.fe.Models.DTOs.Skills;

namespace employee.skill.fe.Store.Skills.Actions.Fetch
{
  public class FetchSkillSuccessAction
  {
    public SkillDto SkillToHaveBeenFetched { get; private set; }

    public FetchSkillSuccessAction(SkillDto SkillToHaveBeenFetched)
    {
      SkillToHaveBeenFetched  = SkillToHaveBeenFetched;
    }
  }
}