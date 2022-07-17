using Fluxor;
using smarthotel.ui.Store.Visits.Actions.FetchVisits;

namespace smarthotel.ui.Store.Visits.Reducers.FetchVisit
{
  public class FetchVisitListReducerSuccessActionReducer : Reducer<VisitState, FetchVisitListSuccessAction>
  {
    public override VisitState Reduce(VisitState state, FetchVisitListSuccessAction action)
    {
      return new VisitState(
        action.VisitList,
        "",
        state.IsLoading,
        state.Visit,
        state.VisitId
      );
    }
  }
}