using employee.skill.fe.Store.Skills.Actions.Update;
using Fluxor;

namespace employee.skill.fe.Store.Skills.Reducers.Update
{
  public class UpdateSkillReducer : Reducer<SkillState, UpdateSkillAction>
  {
    public override SkillState Reduce(SkillState state, UpdateSkillAction action)
    {
      return new SkillState(
        state.SkillList,
        "",
        state.IsLoading,
        state.Skill,
        state.SkillToBeCreatedPayload,
        action.SkillForModificationDto,
        action.SkillToBeUpdateId
        );
    }
  }
}