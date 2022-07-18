using employee.skill.fe.Store.Skills.Actions.Update;
using employee.skill.fe.Store.Statuses;
using Fluxor;

namespace employee.skill.fe.Store.Skills.Reducers.Update
{
  public class UpdateSkillReducerSuccessActionReducer : Reducer<SkillState, UpdateSkillSuccessAction>
  {
    public override SkillState Reduce(SkillState state, UpdateSkillSuccessAction action)
    {
      return new SkillState(
        state.SkillList,
        action.SkillUpdateStatus,
        state.IsLoading,
        action.SkillHaveBeenUpdated,
        state.SkillToBeCreatedPayload,
        state.SkillToBeUpdatePayload,
        state.SkillId,
        state.CreationStatus,
        ModificationStatus.Success,
        state.DeletionStatus
      );
    }
  }
}