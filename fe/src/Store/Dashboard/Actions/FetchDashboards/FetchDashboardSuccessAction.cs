namespace smarthotel.ui.Store.Dashboard.Actions.FetchDashboards
{
  public class FetchDashboardSuccessAction
  {
    public int CustomersCount { get; private set;}
    public int SpacesCount { get; private set;}
    public int ServicesCount { get; private set;}
    public float TotalsCount { get; private set;}

    public FetchDashboardSuccessAction(int customersCount, int spacesCount, int servicesCount, float totalsCount)
    {
      CustomersCount = customersCount;
      SpacesCount = spacesCount;
      ServicesCount = servicesCount;
      TotalsCount = totalsCount;
    }
  }
}