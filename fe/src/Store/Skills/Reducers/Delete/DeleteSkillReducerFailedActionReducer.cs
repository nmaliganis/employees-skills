using employee.skill.fe.Store.Skills.Actions.Delete;
using employee.skill.fe.Store.Statuses;
using Fluxor;

namespace employee.skill.fe.Store.Skills.Reducers.Delete
{
  public class DeleteSkillReducerFailedActionReducer : Reducer<SkillState, DeleteSkillFailedAction>
  {
    public override SkillState Reduce(SkillState state, DeleteSkillFailedAction action)
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
        DeletionStatus.Failed
        );
    }
  }
}