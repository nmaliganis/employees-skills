using Fluxor;
using smarthotel.ui.Store.Spaces.Actions.FetchSpaces;

namespace smarthotel.ui.Store.Spaces.Reducers.FetchSpace
{
  public class FetchSpaceListReducerSuccessActionReducer : Reducer<SpaceState, FetchSpaceListSuccessAction>
  {
    public override SpaceState Reduce(SpaceState state, FetchSpaceListSuccessAction action)
    {
      return new SpaceState(
        action.SpaceList,
        state.SpaceFameYearList,
        state.SpaceFameMonthList,
        "",
        state.IsLoading,
        state.Space,
        state.SpaceId
      );
    }
  }
}