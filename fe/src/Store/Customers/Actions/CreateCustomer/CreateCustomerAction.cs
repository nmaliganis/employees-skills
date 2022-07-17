
using smarthotel.ui.Models.DTOs.Customers;

namespace smarthotel.ui.Store.Customers.Actions.CreateCustomer
{
  public class CreateCustomerAction
  {
    public CreateCustomerAction(CustomerForCreationDto customerToBeCreatedPayload)
    {
      CustomerToBeCreatedPayload = customerToBeCreatedPayload;
    }

    public CustomerForCreationDto CustomerToBeCreatedPayload { get; private set; }
  }
}