namespace smarthotel.ui.Store.Customers.Actions.CreateCustomer
{
  public class CreateCustomerFailedAction
  {
    public string ErrorMessage { get; private set; }
    public string ErrorContent { get; private set; }
    public CreateCustomerFailedAction(string errorMessage, string errorContent)
    {
      ErrorMessage = errorMessage;
      ErrorContent = errorContent;
    }
  }
}