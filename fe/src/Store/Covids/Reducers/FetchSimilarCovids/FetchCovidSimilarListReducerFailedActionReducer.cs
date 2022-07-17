using Fluxor;
using smarthotel.ui.Store.Covids.Actions.FetchSimilarCovids;

namespace smarthotel.ui.Store.Covids.Reducers.FetchSimilarCovids
{
  public class FetchCovidSimilarListReducerFailedActionReducer : Reducer<CovidState, FetchCovidSimilarListFailedAction>
  {
    public override CovidState Reduce(CovidState state, FetchCovidSimilarListFailedAction action)
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