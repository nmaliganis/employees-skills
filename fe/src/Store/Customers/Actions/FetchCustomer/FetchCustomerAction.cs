using System;

namespace smarthotel.ui.Store.Customers.Actions.FetchCustomer
{
  public class FetchCustomerAction
  {
    public Guid CustomerToBeFetchedId { get; private set; }

    public FetchCustomerAction(Guid customerToBeFetchedId)
    {
      CustomerToBeFetchedId = customerToBeFetchedId;
    }
  }
}