using System;
using System.Threading.Tasks;
using employee.skill.fe.ServiceAgents.Contracts.Skills;
using employee.skill.fe.Store.Skills.Actions.Fetch;
using Fluxor;

namespace Skill.skill.fe.Store.Skills.Effects.FetchSkill
{
  public class FetchSkillEffect : Effect<FetchSkillAction>
  {
    public ISkillDataService SkillDataService { get; set; }
    public FetchSkillEffect(ISkillDataService SkillDataService)
    {
      SkillDataService = SkillDataService;
    }

    public override async Task HandleAsync(FetchSkillAction action, IDispatcher dispatcher)
    {
      try
      {
        var skill = await SkillDataService.GetSkill(action.SkillToBeFetchedId);
        dispatcher.Dispatch(new FetchSkillSuccessAction(skill));
      }
      catch (Exception e)
      {
        dispatcher.Dispatch(new FetchSkillFailedAction(e.Message));
      }     
    }
  }
}