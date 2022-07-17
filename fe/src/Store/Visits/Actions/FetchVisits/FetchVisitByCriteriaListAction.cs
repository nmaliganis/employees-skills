using smarthotel.ui.Models.DTOs.Visits;

namespace smarthotel.ui.Store.Visits.Actions.FetchVisits
{
  public class FetchVisitByCriteriaListAction
  {
    public VisitCriteriaSearchDto Criteria { get; }

    public FetchVisitByCriteriaListAction(VisitCriteriaSearchDto criteria)
    {
      Criteria = criteria;
    }
  }
}