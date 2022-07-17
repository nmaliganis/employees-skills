using System.Collections.Generic;
using smarthotel.ui.Models.DTOs.Customers;

namespace smarthotel.ui.Store.Customers.Actions.FetchCustomers
{
  public class FetchCustomerListSuccessAction
  {
    public List<CustomerDto> CustomerList { get; private set; }

    public FetchCustomerListSuccessAction(List<CustomerDto> customerList)
    {
      CustomerList  = customerList;
    }
  }
}