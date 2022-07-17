using System;
using smarthotel.ui.Models.DTOs.Customers;

namespace smarthotel.ui.Store.Customers.Actions.UpdateCustomer
{
  public class UpdateCustomerAction
  {
    public Guid CustomerToBeUpdateId { get; private set; }
    public CustomerForModificationDto CustomerForModificationDto { get; private set; }

    public UpdateCustomerAction(Guid customerToBeUpdateId, CustomerForModificationDto customerForModificationDto)
    {
      CustomerToBeUpdateId = customerToBeUpdateId;
      CustomerForModificationDto = customerForModificationDto;
    }
  }
}