using System;
using System.Collections.Generic;
using smarthotel.ui.Models.DTOs.Visits;

namespace smarthotel.ui.Store.Visits
{
  public class VisitState
  {
    public bool IsLoading { get; private set; }
    public string ErrorMessage { get; private set; }
    public List<VisitDto> VisitList { get; private set; }
    public VisitDto Visit { get; private set; }
    public Guid VisitId { get; }

    public VisitState(
      List<VisitDto> visitList, 
      string errorMessage, 
      bool isLoading,
      VisitDto visit, 
      Guid visitId
    )
    {
      VisitList  = visitList;
      ErrorMessage = errorMessage;
      IsLoading = isLoading;
      Visit = visit;
      VisitId = visitId;
    }
  }
}