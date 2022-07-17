using System;
using System.Threading.Tasks;
using Fluxor;
using smarthotel.ui.ServiceAgents.Contracts.Spaces;
using smarthotel.ui.Store.Spaces.Actions.FetchFameSpaces;
using smarthotel.ui.Store.Spaces.Actions.FetchSpaces;

namespace smarthotel.ui.Store.Spaces.Effects.FetchFameSpaces
{
  public class FetchFameSpaceListEffect : Effect<FetchFameSpaceListAction>
  {
    public ISpaceDataService SpaceDataService { get; set; }
    public FetchFameSpaceListEffect(ISpaceDataService spaceDataService)
    {
      SpaceDataService = spaceDataService;
    }

    public override async Task HandleAsync(FetchFameSpaceListAction action, IDispatcher dispatcher)
    {
      try
      {
        var fameSpacesYear = await SpaceDataService.GetSpaceFameList(true);
        var fameSpacesMonth = await SpaceDataService.GetSpaceFameList(false);
        dispatcher.Dispatch(new FetchFameSpaceListSuccessAction(fameSpacesYear, fameSpacesMonth));
      }
      catch (Exception e)
      {
        dispatcher.Dispatch(new FetchSpaceListFailedAction(e.Message));
      }      
    }
  }
}