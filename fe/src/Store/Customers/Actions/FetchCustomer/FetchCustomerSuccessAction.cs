using smarthotel.ui.Models.DTOs.Customers;

namespace smarthotel.ui.Store.Customers.Actions.FetchCustomer
{
  public class FetchCustomerSuccessAction
  {
    public CustomerDto CustomerToHaveBeenFetched { get; private set; }

    public FetchCustomerSuccessAction(CustomerDto customerToHaveBeenFetched)
    {
      CustomerToHaveBeenFetched  = customerToHaveBeenFetched;
    }
  }
}