using employee.skill.fe.Store.Skills.Actions.Delete;
using Fluxor;

namespace employee.skill.fe.Store.Skills.Reducers.Delete
{
  public class DeleteSkillReducer : Reducer<SkillState, DeleteSkillAction>
  {
    public override SkillState Reduce(SkillState state, DeleteSkillAction action)
    {
      return new SkillState(
        state.SkillList,
        "",
        state.IsLoading,
        state.Skill,
        state.SkillToBeCreatedPayload,
        state.SkillToBeUpdatePayload,
        action.SkillToBeDeletedId,
        state.CreationStatus,
        state.ModificationStatus,
        state.DeletionStatus
        );
    }
  }
}