namespace smarthotel.ui.Store.Dashboard
{
  public class DashboardState
  {
    public string ErrorMessage { get; private set; }
    public int CustomersCount { get; private set;}
    public int SpacesCount { get; private set;}
    public int ServicesCount { get; private set;}
    public float TotalsCount { get; private set;}

    public DashboardState(string errorMessage, int customersCount, int spacesCount, int servicesCount, float totalsCount)
    {
      ErrorMessage = errorMessage;
      CustomersCount = customersCount;
      SpacesCount = spacesCount;
      ServicesCount = servicesCount;
      TotalsCount = totalsCount;
    }
  }
}