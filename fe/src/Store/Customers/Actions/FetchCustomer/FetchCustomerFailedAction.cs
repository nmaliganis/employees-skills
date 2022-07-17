namespace smarthotel.ui.Store.Customers.Actions.FetchCustomer
{
  public class FetchCustomerFailedAction
  {
    public string ErrorMessage { get; private set; }
    public FetchCustomerFailedAction(string errorMessage)
    {
      ErrorMessage = errorMessage;
    }
  }
}