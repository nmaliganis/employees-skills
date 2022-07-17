using Fluxor;
using smarthotel.ui.Store.Spaces.Actions.FetchFameSpaces;

namespace smarthotel.ui.Store.Spaces.Reducers.FetchFameSpaces
{
  public class FetchFameSpaceListReducerFailedActionReducer : Reducer<SpaceState, FetchFameSpaceListFailedAction>
  {
    public override SpaceState Reduce(SpaceState state, FetchFameSpaceListFailedAction action)
    {
      return new SpaceState(
        state.SpaceList,
        state.SpaceFameYearList,
        state.SpaceFameMonthList,
        action.ErrorMessage,
        state.IsLoading,
        state.Space,
        state.SpaceId
        );
    }
  }
}