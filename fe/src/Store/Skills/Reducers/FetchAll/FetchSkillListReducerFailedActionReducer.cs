using employee.skill.fe.Store.Skills;
using employee.skill.fe.Store.Skills.Actions.FetchAll;
using Fluxor;

namespace Skill.skill.fe.Store.Skills.Reducers.FetchSkills
{
  public class FetchSkillListReducerFailedActionReducer : Reducer<SkillState, FetchSkillListFailedAction>
  {
    public override SkillState Reduce(SkillState state, FetchSkillListFailedAction action)
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