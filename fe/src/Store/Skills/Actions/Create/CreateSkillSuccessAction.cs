using employee.skill.fe.Models.DTOs.Skills;

namespace employee.skill.fe.Store.Skills.Actions.Create
{
  public class CreateSkillSuccessAction
  {
    public SkillDto SkillHaveBeenCreated { get; private set; }

    public CreateSkillSuccessAction(SkillDto SkillHaveBeenCreated)
    {
      SkillHaveBeenCreated = SkillHaveBeenCreated;
    }
  }
}