using Fluxor;
using smarthotel.ui.Store.Covids.Actions.FetchCovids;

namespace smarthotel.ui.Store.Covids.Reducers.FetchCovids
{
  public class FetchCovidListReducerFailedActionReducer : Reducer<CovidState, FetchCovidListFailedAction>
  {
    public override CovidState Reduce(CovidState state, FetchCovidListFailedAction action)
    {
      return new CovidState(
        state.SpaceCovidList,
        state.SpaceCovidSimilarList,
        action.ErrorMessage,
        true,
        state.Nfc
        );
    }
  }
}