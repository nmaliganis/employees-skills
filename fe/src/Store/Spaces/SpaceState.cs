using System;
using System.Collections.Generic;
using smarthotel.ui.Models.DTOs.Spaces;

namespace smarthotel.ui.Store.Spaces
{
  public class SpaceState
  {
    public bool IsLoading { get; private set; }
    public string ErrorMessage { get; private set; }
    public List<SpaceDto> SpaceList { get; private set; }
    public List<SpaceFameDto> SpaceFameYearList { get; }
    public List<SpaceFameDto> SpaceFameMonthList { get; }
    public SpaceDto Space { get; private set; }
    public Guid SpaceId { get; }

    public SpaceState(
      List<SpaceDto> spaceList, 
      List<SpaceFameDto> spaceFameYearList, 
      List<SpaceFameDto> spaceFameMonthList, 
      string errorMessage, 
      bool isLoading,
      SpaceDto space, 
      Guid spaceId
    )
    {
      SpaceList  = spaceList;
      SpaceFameYearList = spaceFameYearList;
      SpaceFameMonthList = spaceFameMonthList;
      ErrorMessage = errorMessage;
      IsLoading = isLoading;
      Space = space;
      SpaceId = spaceId;
    }
  }
}