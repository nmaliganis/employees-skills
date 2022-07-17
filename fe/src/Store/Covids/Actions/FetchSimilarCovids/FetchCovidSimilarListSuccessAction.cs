using System.Collections.Generic;
using smarthotel.ui.Models.DTOs.Spaces;

namespace smarthotel.ui.Store.Covids.Actions.FetchSimilarCovids
{
  public class FetchCovidSimilarListSuccessAction
  {
    public List<SpaceRepoDto> SpaceSimilarCovidList { get; private set; }

    public FetchCovidSimilarListSuccessAction(List<SpaceRepoDto> spaceSimilarRepoList)
    {
      SpaceSimilarCovidList = spaceSimilarRepoList;
    }
  }
}