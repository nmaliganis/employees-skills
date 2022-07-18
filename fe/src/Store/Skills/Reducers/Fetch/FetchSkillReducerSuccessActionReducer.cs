using employee.skill.fe.Store.Skills.Actions.Fetch;
using Fluxor;

namespace employee.skill.fe.Store.Skills.Reducers.Fetch
{
  public class FetchSkillReducerSuccessActionReducer : Reducer<SkillState, FetchSkillSuccessAction>
  {
    public override SkillState Reduce(SkillState state, FetchSkillSuccessAction action)
    {
      return new SkillState(
        state.SkillList,
        "",
        state.IsLoading,
        action.SkillToHaveBeenFetched,
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