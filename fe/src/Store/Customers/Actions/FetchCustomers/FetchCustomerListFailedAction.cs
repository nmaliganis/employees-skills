namespace smarthotel.ui.Store.Customers.Actions.FetchCustomers
{
  public class FetchCustomerListFailedAction
  {
    public string ErrorMessage { get; private set; }
    public FetchCustomerListFailedAction(string errorMessage)
    {
      ErrorMessage = errorMessage;
    }
  }
}