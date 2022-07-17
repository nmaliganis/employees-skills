using smarthotel.ui.Models.DTOs.Customers;

namespace smarthotel.ui.Store.Customers.Actions.CreateCustomer
{
  public class CreateCustomerSuccessAction
  {
    public CustomerDto CustomerHaveBeenCreated { get; private set; }

    public CreateCustomerSuccessAction(CustomerDto customerHaveBeenCreated)
    {
      CustomerHaveBeenCreated = customerHaveBeenCreated;
    }
  }
}