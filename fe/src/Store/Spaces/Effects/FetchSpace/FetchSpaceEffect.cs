using System;
using System.Threading.Tasks;
using Fluxor;
using smarthotel.ui.ServiceAgents.Contracts.Spaces;
using smarthotel.ui.Store.Spaces.Actions.FetchCustomer;

namespace smarthotel.ui.Store.Spaces.Effects.FetchSpace
{
  public class FetchSpaceEffect : Effect<FetchSpaceAction>
  {
    public ISpaceDataService SpaceDataService { get; set; }
    public FetchSpaceEffect(ISpaceDataService spaceDataService)
    {
      SpaceDataService = spaceDataService;
    }

    public override async Task HandleAsync(FetchSpaceAction action, IDispatcher dispatcher)
    {
      try
      {
        var space = await SpaceDataService.GetSpace(action.SpaceToBeFetchedId);
        dispatcher.Dispatch(new FetchSpaceSuccessAction(space));
      }
      catch (Exception e)
      {
        dispatcher.Dispatch(new FetchSpaceFailedAction(e.Message));
      }     
    }
  }
}