using Fluxor;
using smarthotel.ui.Store.Dashboard.Actions.FetchDashboards;

namespace smarthotel.ui.Store.Dashboard.Reducers.FetchDashboard
{
  public class FetchDashboardReducer : Reducer<DashboardState, FetchDashboardAction>
  {
    public override DashboardState Reduce(DashboardState state, FetchDashboardAction action)
    {
      return new DashboardState(
        "",
        state.CustomersCount,
        state.SpacesCount,
        state.ServicesCount,
        state.TotalsCount
        );
    }
  }
}