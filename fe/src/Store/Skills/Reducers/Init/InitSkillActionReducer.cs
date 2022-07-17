using employee.skill.fe.Models.DTOs.Skills;
using employee.skill.fe.Store.Skills.Actions.Init;
using Fluxor;

namespace employee.skill.fe.Store.Skills.Reducers.Init
{
  public class InitSkillActionReducer : Reducer<SkillState, InitSkillAction>
  {
    public override SkillState Reduce(SkillState state, InitSkillAction action)
    {
      return new SkillState(
        state.SkillList,
        "",
        state.IsLoading,
        new SkillDto(), 
        state.SkillToBeCreatedPayload,
        state.SkillToBeUpdatePayload,
        state.SkillId
      );
    }
  }
}