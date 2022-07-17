using System.Collections.Generic;
using smarthotel.ui.Models.DTOs.Spaces;

namespace smarthotel.ui.Store.Spaces.Actions.FetchSpaces
{
  public class FetchSpaceListSuccessAction
  {
    public List<SpaceDto> SpaceList { get; private set; }

    public FetchSpaceListSuccessAction(List<SpaceDto> spaceList)
    {
      SpaceList  = spaceList;
    }
  }
}