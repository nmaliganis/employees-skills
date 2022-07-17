using System;

namespace smarthotel.ui.Store.Visits.Actions.FetchVisit
{
  public class FetchVisitAction
  {
    public Guid VisitToBeFetchedId { get; private set; }

    public FetchVisitAction(Guid visitToBeFetchedId)
    {
      VisitToBeFetchedId = visitToBeFetchedId;
    }
  }
}