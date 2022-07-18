using employee.skill.fe.Store.Skills.Actions.Create;
using employee.skill.fe.Store.Statuses;
using Fluxor;

namespace employee.skill.fe.Store.Skills.Reducers.Create
{
  public class CreateSkillReducerSuccessActionReducer : Reducer<SkillState, CreateSkillSuccessAction>
  {
    public override SkillState Reduce(SkillState state, CreateSkillSuccessAction action)
    {
      return new SkillState(
        state.SkillList,
        action.SkillHaveBeenCreated.Message,
        state.IsLoading,
        action.SkillHaveBeenCreated,
        state.SkillToBeCreatedPayload,
        state.SkillToBeUpdatePayload,
        state.SkillId,
        CreationStatus.Success,
        state.ModificationStatus,
        state.DeletionStatus
      );
    }
  }
}