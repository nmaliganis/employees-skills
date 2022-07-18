using employee.skill.fe.Store.Skills.Actions.Fetch;
using Fluxor;

namespace employee.skill.fe.Store.Skills.Reducers.Fetch
{
  public class FetchSkillReducer : Reducer<SkillState, FetchSkillAction>
  {
    public override SkillState Reduce(SkillState state, FetchSkillAction action)
    {
      return new SkillState(
        state.SkillList,
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