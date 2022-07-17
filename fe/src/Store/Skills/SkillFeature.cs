using System;
using System.Collections.Generic;
using employee.skill.fe.Models.DTOs.Skills;
using Fluxor;

namespace employee.skill.fe.Store.Skills
{
  public class SkillFeature : Feature<SkillState>
  {
    public override string GetName() => "Skill";

    protected override SkillState GetInitialState() => new SkillState(
      new List<SkillDto>(), 
      "",
      true,
      new SkillDto(), 
      new SkillForCreationDto(), 
      new SkillForModificationDto(), 
      Guid.Empty
    );
  }
}