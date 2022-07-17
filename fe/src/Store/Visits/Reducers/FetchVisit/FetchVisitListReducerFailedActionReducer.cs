using Fluxor;
using smarthotel.ui.Store.Visits.Actions.FetchVisits;

namespace smarthotel.ui.Store.Visits.Reducers.FetchVisit
{
  public class FetchVisitListReducerFailedActionReducer : Reducer<VisitState, FetchVisitListFailedAction>
  {
    public override VisitState Reduce(VisitState state, FetchVisitListFailedAction action)
    {
      return new VisitState(
        state.VisitList,
        action.ErrorMessage,
        state.IsLoading,
        state.Visit,
        state.VisitId
        );
    }
  }
}