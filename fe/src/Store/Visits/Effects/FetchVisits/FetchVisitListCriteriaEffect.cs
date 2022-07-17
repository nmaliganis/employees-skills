using System;
using System.Threading.Tasks;
using Fluxor;
using smarthotel.ui.ServiceAgents.Contracts.Visits;
using smarthotel.ui.Store.Visits.Actions.FetchVisits;

namespace smarthotel.ui.Store.Visits.Effects.FetchVisits
{
  public class FetchVisitListCriteriaEffect : Effect<FetchVisitByCriteriaListAction>
  {
    public IVisitDataService VisitDataService { get; set; }
    public FetchVisitListCriteriaEffect(IVisitDataService visitDataService)
    {
      VisitDataService = visitDataService;
    }

    public override async Task HandleAsync(FetchVisitByCriteriaListAction action, IDispatcher dispatcher)
    {
      try
      {
        var visits = await VisitDataService.GetVisitListByCriteria(action.Criteria);
        dispatcher.Dispatch(new FetchVisitListSuccessAction(visits));
      }
      catch (Exception e)
      {
        dispatcher.Dispatch(new FetchVisitListFailedAction(e.Message));
      }      
    }
  }
}