using System.Collections.Generic;
using smarthotel.ui.Models.DTOs.Spaces;

namespace smarthotel.ui.Store.Covids.Actions.FetchCovids
{
  public class FetchCovidListSuccessAction
  {
    public List<SpaceRepoDto> SpaceCovidList { get; private set; }

    public FetchCovidListSuccessAction(List<SpaceRepoDto> spaceRepoList)
    {
      SpaceCovidList = spaceRepoList;
    }
  }
}