using employee.skill.fe.Store.Skills.Actions.Create;
using Fluxor;

namespace employee.skill.fe.Store.Skills.Reducers.Create
{
  public class CreateSkillReducerFailedActionReducer : Reducer<SkillState, CreateSkillFailedAction>
  {
    public override SkillState Reduce(SkillState state, CreateSkillFailedAction action)
    {
      return new SkillState(
        state.SkillList,
        action.ErrorMessage,
        state.IsLoading,
        state.Skill,
        state.SkillToBeCreatedPayload,
        state.SkillToBeUpdatePayload,
        state.SkillId
        );
    }
  }
}