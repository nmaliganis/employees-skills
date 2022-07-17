using Fluxor;
using smarthotel.ui.Store.Visits.Actions.FetchVisits;

namespace smarthotel.ui.Store.Visits.Reducers.FetchVisit
{
  public class FetchVisitListReducer : Reducer<VisitState, FetchVisitListAction>
  {
    public override VisitState Reduce(VisitState state, FetchVisitListAction action)
    {
      return new VisitState(
        state.VisitList,
        "",
        state.IsLoading,
        state.Visit,
        state.VisitId
        );
    }
  }
}