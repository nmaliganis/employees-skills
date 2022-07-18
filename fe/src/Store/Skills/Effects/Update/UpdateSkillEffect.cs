using System;
using System.Threading.Tasks;
using employee.skill.fe.ServiceAgents.Base;
using employee.skill.fe.ServiceAgents.Contracts.Skills;
using employee.skill.fe.Store.Skills.Actions.Update;
using Fluxor;

namespace employee.skill.fe.Store.Skills.Effects.Update
{
  public class UpdateSkillEffect : Effect<UpdateSkillAction>
  {
    public ISkillDataService SkillDataService { get; set; }
    public UpdateSkillEffect(ISkillDataService skillDataService)
    {
      SkillDataService = skillDataService;
    }

    public override async Task HandleAsync(UpdateSkillAction action, IDispatcher dispatcher)
    {
      try
      {
        var updatedSkill = await SkillDataService.UpdateSkill(action.SkillToBeUpdateId, action.SkillForModificationDto);
        dispatcher.Dispatch(new UpdateSkillSuccessAction(updatedSkill, updatedSkill.Message));
        //Todo: Logging
      }
      catch (ServiceHttpRequestException<string> e)
      {
        dispatcher.Dispatch(new UpdateSkillFailedAction(errorMessage: e.Message, e.Content));
      }     
      catch (Exception e)
      {
        dispatcher.Dispatch(new UpdateSkillFailedAction(errorMessage: e.Message, e.InnerException?.Message));
      }     
    }
  }
}