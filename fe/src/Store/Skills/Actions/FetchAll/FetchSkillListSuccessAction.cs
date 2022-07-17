using System.Collections.Generic;
using employee.skill.fe.Models.DTOs.Skills;

namespace employee.skill.fe.Store.Skills.Actions.FetchAll
{
  public class FetchSkillListSuccessAction
  {
    public List<SkillDto> SkillList { get; private set; }

    public FetchSkillListSuccessAction(List<SkillDto> skillList)
    {
      SkillList  = skillList;
    }
  }
}