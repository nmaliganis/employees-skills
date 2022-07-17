using System;
using System.Threading.Tasks;
using employee.skill.fe.ServiceAgents.Contracts.Skills;
using employee.skill.fe.Store.Skills.Actions.FetchAll;
using Fluxor;

namespace employee.skill.fe.Store.Skills.Effects.FetchAll
{
  public class FetchSkillListEffect : Effect<FetchSkillListAction>
  {
    public ISkillDataService SkillDataService { get; set; }
    public FetchSkillListEffect(ISkillDataService skillDataService)
    {
      SkillDataService = skillDataService;
    }

    public override async Task HandleAsync(FetchSkillListAction action, IDispatcher dispatcher)
    {
      try
      {
        var skills = await SkillDataService.GetSkillList();
        dispatcher.Dispatch(new FetchSkillListSuccessAction(skills));
      }
      catch (Exception e)
      {
        dispatcher.Dispatch(new FetchSkillListFailedAction(e.Message));
      }      
    }
  }
}