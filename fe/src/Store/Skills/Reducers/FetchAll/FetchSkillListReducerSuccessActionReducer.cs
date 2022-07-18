using employee.skill.fe.Store.Skills.Actions.FetchAll;
using Fluxor;

namespace employee.skill.fe.Store.Skills.Reducers.FetchAll
{
  public class FetchSkillListReducerSuccessActionReducer : Reducer<SkillState, FetchSkillListSuccessAction>
  {
    public override SkillState Reduce(SkillState state, FetchSkillListSuccessAction action)
    {
      return new SkillState(
        action.SkillList,
        "",
        state.IsLoading,
        state.Skill,
        state.SkillToBeCreatedPayload,
        state.SkillToBeUpdatePayload,
        state.SkillId,
        state.CreationStatus,
        state.ModificationStatus,
        state.DeletionStatus
      );
    }
  }
}