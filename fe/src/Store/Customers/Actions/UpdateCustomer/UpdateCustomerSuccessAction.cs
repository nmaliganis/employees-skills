using System;
using smarthotel.ui.Models.DTOs.Customers;

namespace smarthotel.ui.Store.Customers.Actions.UpdateCustomer
{
  public class UpdateCustomerSuccessAction
  {
    public CustomerDto CustomerHaveBeenUpdated { get; private set; }
    public string CustomerUpdateStatus { get; private set; }

    public UpdateCustomerSuccessAction(CustomerDto customerHaveBeenUpdated, string customerUpdateStatus)
    {
      CustomerHaveBeenUpdated = customerHaveBeenUpdated;
      CustomerUpdateStatus = customerUpdateStatus;
    }
  }
}