using Fluxor;
using smarthotel.ui.Store.Covids.Actions.FetchCovids;

namespace smarthotel.ui.Store.Covids.Reducers.FetchCovids
{
  public class FetchCovidListReducerSuccessActionReducer : Reducer<CovidState, FetchCovidListSuccessAction>
  {
    public override CovidState Reduce(CovidState state, FetchCovidListSuccessAction action)
    {
      return new CovidState(
        action.SpaceCovidList,
        state.SpaceCovidSimilarList,
        "",
        true,
        state.Nfc
      );
    }
  }
}