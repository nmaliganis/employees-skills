using System.Collections.Generic;
using smarthotel.ui.Models.DTOs.Visits;

namespace smarthotel.ui.Store.Visits.Actions.FetchVisits
{
  public class FetchVisitListSuccessAction
  {
    public List<VisitDto> VisitList { get; private set; }

    public FetchVisitListSuccessAction(List<VisitDto> visitList)
    {
      VisitList  = visitList;
    }
  }
}