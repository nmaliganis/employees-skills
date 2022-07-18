using employee.skill.fe.Models.DTOs.Skills;

namespace employee.skill.fe.Store.Skills.Actions.Create
{
  public class CreateSkillAction
  {
    public CreateSkillAction(SkillForCreationDto skillToBeCreatedPayload)
    {
      SkillToBeCreatedPayload = skillToBeCreatedPayload;
    }

    public SkillForCreationDto SkillToBeCreatedPayload { get; private set; }
  }
}