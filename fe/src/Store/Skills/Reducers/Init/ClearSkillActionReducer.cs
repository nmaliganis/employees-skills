using employee.skill.fe.Models.DTOs.Skills;
using employee.skill.fe.Store.Skills.Actions.Init;
using Fluxor;

namespace employee.skill.fe.Store.Skills.Reducers.Init
{
  public class ClearSkillActionReducer : Reducer<SkillState, ClearSkillAction>
  {
    public override SkillState Reduce(SkillState state, ClearSkillAction action)
    {
      return new SkillState(
        state.SkillList,
        "",
        state.IsLoading,
        new SkillDto(), 
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