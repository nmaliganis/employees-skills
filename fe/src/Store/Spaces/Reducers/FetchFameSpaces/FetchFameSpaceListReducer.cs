using Fluxor;
using smarthotel.ui.Store.Spaces.Actions.FetchFameSpaces;

namespace smarthotel.ui.Store.Spaces.Reducers.FetchFameSpaces
{
  public class FetchFameSpaceListReducer : Reducer<SpaceState, FetchFameSpaceListAction>
  {
    public override SpaceState Reduce(SpaceState state, FetchFameSpaceListAction action)
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