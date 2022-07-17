using smarthotel.ui.Models.DTOs.Visits;

namespace smarthotel.ui.Store.Visits.Actions.FetchVisit
{
  public class FetchVisitSuccessAction
  {
    public VisitDto VisitToHaveBeenFetched { get; private set; }

    public FetchVisitSuccessAction(VisitDto visitToHaveBeenFetched)
    {
      VisitToHaveBeenFetched  = visitToHaveBeenFetched;
    }
  }
}