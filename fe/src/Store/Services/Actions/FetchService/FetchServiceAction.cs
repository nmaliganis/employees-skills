using System;

namespace smarthotel.ui.Store.Services.Actions.FetchService
{
  public class FetchServiceAction
  {
    public Guid ServiceToBeFetchedId { get; private set; }

    public FetchServiceAction(Guid serviceToBeFetchedId)
    {
      ServiceToBeFetchedId = serviceToBeFetchedId;
    }
  }
}