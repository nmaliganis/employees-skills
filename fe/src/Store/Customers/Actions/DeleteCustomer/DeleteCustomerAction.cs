using System;

namespace smarthotel.ui.Store.Customers.Actions.DeleteCustomer
{
  public class DeleteCustomerAction
  {
    public Guid CustomerToBeDeletedId { get; private set; }

    public DeleteCustomerAction(Guid customerToBeDeletedId)
    {
      CustomerToBeDeletedId = customerToBeDeletedId;
    }
  }
}