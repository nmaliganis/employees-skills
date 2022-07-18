using employee.skill.fe.Store.Skills.Actions.Create;
using Fluxor;

namespace employee.skill.fe.Store.Skills.Reducers.Create
{
  public class CreateSkillReducer : Reducer<SkillState, CreateSkillAction>
  {
    public override SkillState Reduce(SkillState state, CreateSkillAction action)
    {
      return new SkillState(
        state.SkillList,
        "",
        state.IsLoading,
        state.Skill,
        action.SkillToBeCreatedPayload,
        state.SkillToBeUpdatePayload,
        state.SkillId,
        state.CreationStatus,
        state.ModificationStatus,
        state.DeletionStatus
        );
    }
  }
}