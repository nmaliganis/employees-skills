using Fluxor;
using smarthotel.ui.Store.Spaces.Actions.FetchFameSpaces;

namespace smarthotel.ui.Store.Spaces.Reducers.FetchFameSpaces
{
  public class FetchFameSpaceListReducerSuccessActionReducer : Reducer<SpaceState, FetchFameSpaceListSuccessAction>
  {
    public override SpaceState Reduce(SpaceState state, FetchFameSpaceListSuccessAction action)
    {
      return new SpaceState(
        state.SpaceList,
        action.SpaceFameYearList,
        action.SpaceFameMonthList,
        "",
        state.IsLoading,
        state.Space,
        state.SpaceId
      );
    }
  }
}