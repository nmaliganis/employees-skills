using employee.skill.fe.Store.Skills.Actions.FetchAll;
using Fluxor;

namespace employee.skill.fe.Store.Skills.Reducers.FetchAll
{
  public class FetchSkillListReducer : Reducer<SkillState, FetchSkillListAction>
  {
    public override SkillState Reduce(SkillState state, FetchSkillListAction action)
    {
      return new SkillState(
        state.SkillList,
        "",
        state.IsLoading,
        state.Skill,
        state.SkillToBeCreatedPayload,
        state.SkillToBeUpdatePayload,
        state.SkillId
        );
    }
  }
}