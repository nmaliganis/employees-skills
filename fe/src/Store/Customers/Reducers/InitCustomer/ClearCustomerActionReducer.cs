using Fluxor;
using smarthotel.ui.Models.DTOs.Customers;
using smarthotel.ui.Store.Customers.Actions.InitCustomer;

namespace smarthotel.ui.Store.Customers.Reducers.InitCustomer
{
  public class ClearCustomerActionReducer : Reducer<CustomerState, ClearCustomerAction>
  {
    public override CustomerState Reduce(CustomerState state, ClearCustomerAction action)
    {
      return new CustomerState(
        state.CustomerList,
        "",
        state.IsLoading,
        new CustomerDto(), 
        state.CustomerToBeCreatedPayload,
        state.CustomerToBeUpdatePayload,
        state.CustomerId
      );
    }
  }
}