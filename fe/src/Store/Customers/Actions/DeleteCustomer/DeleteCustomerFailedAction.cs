namespace smarthotel.ui.Store.Customers.Actions.DeleteCustomer
{
  public class DeleteCustomerFailedAction
  {
    public string ErrorMessage { get; private set; }
    public DeleteCustomerFailedAction(string errorMessage)
    {
      ErrorMessage = errorMessage;
    }
  }
}