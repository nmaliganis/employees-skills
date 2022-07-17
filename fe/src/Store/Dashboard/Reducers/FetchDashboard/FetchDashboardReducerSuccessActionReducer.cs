using Fluxor;
using smarthotel.ui.Store.Dashboard.Actions.FetchDashboards;

namespace smarthotel.ui.Store.Dashboard.Reducers.FetchDashboard
{
  public class FetchDashboardReducerSuccessActionReducer : Reducer<DashboardState, FetchDashboardSuccessAction>
  {
    public override DashboardState Reduce(DashboardState state, FetchDashboardSuccessAction action)
    {
      return new DashboardState(
        "",
        action.CustomersCount,
        action.SpacesCount,
        action.ServicesCount,
        action.TotalsCount
      );
    }
  }
}