namespace smarthotel.ui.Store.Dashboard.Actions.FetchDashboards
{
  public class FetchDashboardFailedAction
  {
    public string ErrorMessage { get; private set; }
    public FetchDashboardFailedAction(string errorMessage)
    {
      ErrorMessage = errorMessage;
    }
  }
}