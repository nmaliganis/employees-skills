using employee.skill.fe.Models.DTOs.Skills;

namespace employee.skill.fe.Store.Skills.Actions.Update
{
  public class UpdateSkillSuccessAction
  {
    public SkillDto SkillHaveBeenUpdated { get; private set; }
    public string SkillUpdateStatus { get; private set; }

    public UpdateSkillSuccessAction(SkillDto SkillHaveBeenUpdated, string SkillUpdateStatus)
    {
      SkillHaveBeenUpdated = SkillHaveBeenUpdated;
      SkillUpdateStatus = SkillUpdateStatus;
    }
  }
}