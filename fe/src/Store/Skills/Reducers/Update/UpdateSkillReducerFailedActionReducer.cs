using employee.skill.fe.Store.Skills.Actions.Update;
using Fluxor;

namespace employee.skill.fe.Store.Skills.Reducers.Update
{
  public class UpdateSkillReducerFailedActionReducer : Reducer<SkillState, UpdateSkillFailedAction>
  {
    public override SkillState Reduce(SkillState state, UpdateSkillFailedAction action)
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