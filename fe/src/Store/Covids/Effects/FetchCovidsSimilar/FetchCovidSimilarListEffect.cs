using System;
using System.Threading.Tasks;
using Fluxor;
using smarthotel.ui.ServiceAgents.Contracts.Spaces;
using smarthotel.ui.Store.Covids.Actions.FetchSimilarCovids;

namespace smarthotel.ui.Store.Covids.Effects.FetchCovidsSimilar
{
  public class FetchCovidSimilarListEffect : Effect<FetchCovidSimilarListAction>
  {
    public ISpaceDataService SpaceDataService { get; set; }
    public FetchCovidSimilarListEffect(ISpaceDataService spaceDataService)
    {
      SpaceDataService = spaceDataService;
    }

    public override async Task HandleAsync(FetchCovidSimilarListAction action, IDispatcher dispatcher)
    {
      try
      {
        var covid = await SpaceDataService.GetSpaceSimilarCovidList(action.Nfc);
        dispatcher.Dispatch(new FetchCovidSimilarListSuccessAction(covid));
      }
      catch (Exception e)
      {
        dispatcher.Dispatch(new FetchCovidSimilarListFailedAction(e.Message));
      }      
    }
  }
}