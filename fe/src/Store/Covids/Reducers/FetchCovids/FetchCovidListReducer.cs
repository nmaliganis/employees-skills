using Fluxor;
using smarthotel.ui.Store.Covids.Actions.FetchCovids;

namespace smarthotel.ui.Store.Covids.Reducers.FetchCovids
{
  public class FetchCovidListReducer : Reducer<CovidState, FetchCovidListAction>
  {
    public override CovidState Reduce(CovidState state, FetchCovidListAction action)
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