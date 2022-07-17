using Fluxor;
using smarthotel.ui.Store.Spaces.Actions.FetchSpaces;

namespace smarthotel.ui.Store.Spaces.Reducers.FetchSpace
{
  public class FetchSpaceListReducerFailedActionReducer : Reducer<SpaceState, FetchSpaceListFailedAction>
  {
    public override SpaceState Reduce(SpaceState state, FetchSpaceListFailedAction action)
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