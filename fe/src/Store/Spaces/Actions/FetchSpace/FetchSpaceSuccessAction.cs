using smarthotel.ui.Models.DTOs.Spaces;

namespace smarthotel.ui.Store.Spaces.Actions.FetchCustomer
{
  public class FetchSpaceSuccessAction
  {
    public SpaceDto SpaceToHaveBeenFetched { get; private set; }

    public FetchSpaceSuccessAction(SpaceDto spaceToHaveBeenFetched)
    {
      SpaceToHaveBeenFetched  = spaceToHaveBeenFetched;
    }
  }
}