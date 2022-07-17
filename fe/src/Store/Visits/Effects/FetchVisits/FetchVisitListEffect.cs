using System;
using System.Threading.Tasks;
using Fluxor;
using smarthotel.ui.ServiceAgents.Contracts.Visits;
using smarthotel.ui.Store.Visits.Actions.FetchVisits;

namespace smarthotel.ui.Store.Visits.Effects.FetchVisits
{
  public class FetchVisitListEffect : Effect<FetchVisitListAction>
  {
    public IVisitDataService VisitDataService { get; set; }
    public FetchVisitListEffect(IVisitDataService visitDataService)
    {
      VisitDataService = visitDataService;
    }

    public override async Task HandleAsync(FetchVisitListAction action, IDispatcher dispatcher)
    {
      try
      {
        var visits = await VisitDataService.GetVisitList();
        dispatcher.Dispatch(new FetchVisitListSuccessAction(visits));
      }
      catch (Exception e)
      {
        dispatcher.Dispatch(new FetchVisitListFailedAction(e.Message));
      }      
    }
  }
}