namespace smarthotel.ui.Store.Customers.Actions.UpdateCustomer
{
  public class UpdateCustomerFailedAction
  {
    public string ErrorMessage { get; private set; }
    public string ErrorContent { get; private set; }
    public UpdateCustomerFailedAction(string errorMessage, string errorContent)
    {
      ErrorMessage = errorMessage;
    }
  }
}