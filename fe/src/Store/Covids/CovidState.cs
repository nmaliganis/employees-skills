using System;
using System.Collections.Generic;
using smarthotel.ui.Models.DTOs.Spaces;

namespace smarthotel.ui.Store.Covids
{
  public class CovidState
  {
    public bool IsLoading { get; private set; }
    public string ErrorMessage { get; private set; }
    public List<SpaceRepoDto> SpaceCovidList { get; private set; }
    public List<SpaceRepoDto> SpaceCovidSimilarList { get; private set; }
    public string Nfc { get; }

    public CovidState(
      List<SpaceRepoDto> spaceCovidList, 
      List<SpaceRepoDto> spaceCovidSimilarList, 
      string errorMessage, 
      bool isLoading,
      string nfc
    )
    {
      SpaceCovidList = spaceCovidList;
      SpaceCovidSimilarList = spaceCovidSimilarList;
      ErrorMessage = errorMessage;
      IsLoading = isLoading;
      Nfc = nfc;
    }
  }
}