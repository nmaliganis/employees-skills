using System;
using System.Threading.Tasks;
using Fluxor;
using smarthotel.ui.ServiceAgents.Contracts.Spaces;
using smarthotel.ui.Store.Spaces.Actions.FetchSpaces;

namespace smarthotel.ui.Store.Spaces.Effects.FetchSpaces
{
  public class FetchSpaceListEffect : Effect<FetchSpaceListAction>
  {
    public ISpaceDataService SpaceDataService { get; set; }
    public FetchSpaceListEffect(ISpaceDataService spaceDataService)
    {
      SpaceDataService = spaceDataService;
    }

    public override async Task HandleAsync(FetchSpaceListAction action, IDispatcher dispatcher)
    {
      try
      {
        var spaces = await SpaceDataService.GetSpaceList();
        dispatcher.Dispatch(new FetchSpaceListSuccessAction(spaces));
      }
      catch (Exception e)
      {
        dispatcher.Dispatch(new FetchSpaceListFailedAction(e.Message));
      }      
    }
  }
}