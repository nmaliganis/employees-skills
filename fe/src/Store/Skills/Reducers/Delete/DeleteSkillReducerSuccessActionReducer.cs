using employee.skill.fe.Store.Skills.Actions.Delete;
using employee.skill.fe.Store.Statuses;
using Fluxor;

namespace employee.skill.fe.Store.Skills.Reducers.Delete
{
  public class DeleteSkillReducerSuccessActionReducer : Reducer<SkillState, DeleteSkillSuccessAction>
  {
    public override SkillState Reduce(SkillState state, DeleteSkillSuccessAction action)
    {
      return new SkillState(
        state.SkillList,
        action.SkillDeletionStatus,
        state.IsLoading,
        state.Skill,
        state.SkillToBeCreatedPayload,
        state.SkillToBeUpdatePayload,
        action.SkillHaveBeenDeletedId,
        state.CreationStatus,
        state.ModificationStatus,
        DeletionStatus.Success
      );
    }
  }
}