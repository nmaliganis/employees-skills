using Fluxor;
using smarthotel.ui.Store.Dashboard.Actions.FetchDashboards;

namespace smarthotel.ui.Store.Dashboard.Reducers.FetchDashboard
{
  public class FetchDashboardReducerFailedActionReducer : Reducer<DashboardState, FetchDashboardFailedAction>
  {
    public override DashboardState Reduce(DashboardState state, FetchDashboardFailedAction action)
    {
      return new DashboardState(
        action.ErrorMessage,
        state.CustomersCount,
        state.SpacesCount,
        state.ServicesCount,
        state.TotalsCount
        );
    }
  }
}