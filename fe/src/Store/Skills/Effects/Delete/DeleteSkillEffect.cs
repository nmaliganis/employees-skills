using System;
using System.Threading.Tasks;
using employee.skill.fe.ServiceAgents.Contracts.Skills;
using employee.skill.fe.Store.Skills.Actions.Delete;
using Fluxor;

namespace employee.skill.fe.Store.Skills.Effects.Delete
{
  public class DeleteSkillEffect : Effect<DeleteSkillAction>
  {
    public ISkillDataService SkillDataService { get; set; }
    public DeleteSkillEffect(ISkillDataService SkillDataService)
    {
      SkillDataService = SkillDataService;
    }

    public override async Task HandleAsync(DeleteSkillAction action, IDispatcher dispatcher)
    {
      try
      {
        var deletedSkill = await SkillDataService.DeleteSkill(action.SkillToBeDeletedId);
        dispatcher.Dispatch(new DeleteSkillSuccessAction(deletedSkill.Id, deletedSkill.Message));
      }
      catch (Exception e)
      {
        dispatcher.Dispatch(new DeleteSkillFailedAction(e.Message));
      }  
    }
  }
}