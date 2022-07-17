using System;
using System.Threading.Tasks;
using employee.skill.fe.ServiceAgents.Base;
using employee.skill.fe.ServiceAgents.Contracts.Skills;
using employee.skill.fe.Store.Skills.Actions.Create;
using Fluxor;

namespace employee.skill.fe.Store.Skills.Effects.Create
{
  public class CreateSkillEffect : Effect<CreateSkillAction>
  {
    public ISkillDataService SkillDataService { get; set; }
    public CreateSkillEffect(ISkillDataService SkillDataService)
    {
      SkillDataService = SkillDataService;
    }

    public override async Task HandleAsync(CreateSkillAction action, IDispatcher dispatcher)
    {
      try
      {
        var createdSkill = await SkillDataService.CreateSkill(action.SkillToBeCreatedPayload);
        dispatcher.Dispatch(new CreateSkillSuccessAction(createdSkill));
        //Todo: Logging
      }
      catch (ServiceHttpRequestException<string> e)
      {
        dispatcher.Dispatch(new CreateSkillFailedAction(errorMessage: e.Message, e.Content));
      }     
      catch (Exception e)
      {
        dispatcher.Dispatch(new CreateSkillFailedAction(errorMessage: e.Message, e.InnerException?.Message));
      }     
    }
  }
}