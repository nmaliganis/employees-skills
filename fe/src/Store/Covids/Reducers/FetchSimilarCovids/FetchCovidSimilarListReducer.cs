using Fluxor;
using smarthotel.ui.Store.Covids.Actions.FetchSimilarCovids;

namespace smarthotel.ui.Store.Covids.Reducers.FetchSimilarCovids
{
  public class FetchCovidSimilarListReducer : Reducer<CovidState, FetchCovidSimilarListAction>
  {
    public override CovidState Reduce(CovidState state, FetchCovidSimilarListAction action)
    {
      return new CovidState(
        state.SpaceCovidList,
        state.SpaceCovidSimilarList,
        "",
        true,
        state.Nfc
      );
    }
  }
}