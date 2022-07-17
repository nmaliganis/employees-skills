using System;

namespace smarthotel.ui.Store.Customers.Actions.DeleteCustomer
{
  public class DeleteCustomerSuccessAction
  {
    public Guid CustomerHaveBeenDeletedId { get; private set; }
    public string CustomerDeletionStatus { get; private set; }

    public DeleteCustomerSuccessAction(Guid customerHaveBeenDeletedId, string customerDeletionStatus)
    {
      CustomerHaveBeenDeletedId = customerHaveBeenDeletedId;
      CustomerDeletionStatus = customerDeletionStatus;
    }
  }
}