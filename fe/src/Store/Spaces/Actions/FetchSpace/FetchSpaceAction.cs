using System;

namespace smarthotel.ui.Store.Spaces.Actions.FetchCustomer
{
  public class FetchSpaceAction
  {
    public Guid SpaceToBeFetchedId { get; private set; }

    public FetchSpaceAction(Guid spaceToBeFetchedId)
    {
      SpaceToBeFetchedId = spaceToBeFetchedId;
    }
  }
}