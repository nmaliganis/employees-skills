using employee.skill.fe.Store.Skills.Actions.Fetch;
using Fluxor;

namespace employee.skill.fe.Store.Skills.Reducers.Fetch
{
  public class FetchSkillReducerFailedActionReducer : Reducer<SkillState, FetchSkillFailedAction>
  {
    public override SkillState Reduce(SkillState state, FetchSkillFailedAction action)
    {
      return new SkillState(
        state.SkillList,
        action.ErrorMessage,
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