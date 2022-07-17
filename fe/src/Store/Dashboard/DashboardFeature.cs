using Fluxor;

namespace smarthotel.ui.Store.Dashboard
{
  public class DashboardFeature : Feature<DashboardState>
  {
    public override string GetName() => "Dashboard";

    protected override DashboardState GetInitialState() => new DashboardState(
      "", 0,0,0,0
    );
  }
}