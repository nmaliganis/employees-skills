using Fluxor;
using smarthotel.ui.Store.Spaces.Actions.FetchSpaces;

namespace smarthotel.ui.Store.Spaces.Reducers.FetchSpace
{
  public class FetchSpaceListReducer : Reducer<SpaceState, FetchSpaceListAction>
  {
    public override SpaceState Reduce(SpaceState state, FetchSpaceListAction action)
    {
      return new SpaceState(
        state.SpaceList,
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