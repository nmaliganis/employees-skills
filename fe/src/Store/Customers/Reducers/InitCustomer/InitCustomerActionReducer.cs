using Fluxor;
using smarthotel.ui.Models.DTOs.Customers;
using smarthotel.ui.Store.Customers.Actions.InitCustomer;

namespace smarthotel.ui.Store.Customers.Reducers.InitCustomer
{
  public class InitCustomerActionReducer : Reducer<CustomerState, InitCustomerAction>
  {
    public override CustomerState Reduce(CustomerState state, InitCustomerAction action)
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