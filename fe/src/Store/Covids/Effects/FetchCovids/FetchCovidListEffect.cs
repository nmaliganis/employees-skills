using System;
using System.Threading.Tasks;
using Fluxor;
using smarthotel.ui.ServiceAgents.Contracts.Spaces;
using smarthotel.ui.Store.Covids.Actions.FetchCovids;

namespace smarthotel.ui.Store.Covids.Effects.FetchCovids
{
  public class FetchCovidListEffect : Effect<FetchCovidListAction>
  {
    public ISpaceDataService SpaceDataService { get; set; }
    public FetchCovidListEffect(ISpaceDataService spaceDataService)
    {
      SpaceDataService = spaceDataService;
    }

    public override async Task HandleAsync(FetchCovidListAction action, IDispatcher dispatcher)
    {
      try
      {
        var covid = await SpaceDataService.GetSpaceCovidList(action.Nfc);
        dispatcher.Dispatch(new FetchCovidListSuccessAction(covid));
      }
      catch (Exception e)
      {
        dispatcher.Dispatch(new FetchCovidListFailedAction(e.Message));
      }      
    }
  }
}