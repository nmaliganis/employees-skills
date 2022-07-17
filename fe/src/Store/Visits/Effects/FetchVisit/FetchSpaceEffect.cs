using System;
using System.Threading.Tasks;
using Fluxor;
using smarthotel.ui.ServiceAgents.Contracts.Visits;
using smarthotel.ui.Store.Visits.Actions.FetchVisit;

namespace smarthotel.ui.Store.Visits.Effects.FetchVisit
{
  public class FetchVisitEffect : Effect<FetchVisitAction>
  {
    public IVisitDataService VisitDataService { get; set; }
    public FetchVisitEffect(IVisitDataService visitDataService)
    {
      VisitDataService = visitDataService;
    }

    public override async Task HandleAsync(FetchVisitAction action, IDispatcher dispatcher)
    {
      try
      {
        var visit = await VisitDataService.GetVisit(action.VisitToBeFetchedId);
        dispatcher.Dispatch(new FetchVisitSuccessAction(visit));
      }
      catch (Exception e)
      {
        dispatcher.Dispatch(new FetchVisitFailedAction(e.Message));
      }     
    }
  }
}