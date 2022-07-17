using System.Collections.Generic;
using smarthotel.ui.Models.DTOs.Spaces;

namespace smarthotel.ui.Store.Spaces.Actions.FetchFameSpaces
{
  public class FetchFameSpaceListSuccessAction
  {
    public List<SpaceFameDto> SpaceFameYearList { get; private set; }
    public List<SpaceFameDto> SpaceFameMonthList { get; private set; }

    public FetchFameSpaceListSuccessAction(List<SpaceFameDto> spaceFameYearList, List<SpaceFameDto> spaceFameMonthList)
    {
      SpaceFameYearList  = spaceFameYearList;
      SpaceFameMonthList = spaceFameMonthList;
    }
  }
}