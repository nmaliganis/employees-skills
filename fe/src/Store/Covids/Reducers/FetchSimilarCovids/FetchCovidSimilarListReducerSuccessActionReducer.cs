using Fluxor;
using smarthotel.ui.Store.Covids.Actions.FetchSimilarCovids;

namespace smarthotel.ui.Store.Covids.Reducers.FetchSimilarCovids
{
  public class FetchCovidSimilarListReducerSuccessActionReducer : Reducer<CovidState, FetchCovidSimilarListSuccessAction>
  {
    public override CovidState Reduce(CovidState state, FetchCovidSimilarListSuccessAction action)
    {
      return new CovidState(
        state.SpaceCovidList,
        action.SpaceSimilarCovidList,
        "",
        true,
        state.Nfc
      );
    }
  }
}