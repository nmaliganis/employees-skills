using Fluxor;
using smarthotel.ui.Store.Customers.Actions.FetchCustomers;

namespace smarthotel.ui.Store.Customers.Reducers.FetchCustomers
{
  public class FetchCustomerListReducerFailedActionReducer : Reducer<CustomerState, FetchCustomerListFailedAction>
  {
    public override CustomerState Reduce(CustomerState state, FetchCustomerListFailedAction action)
    {
      return new CustomerState(
        state.CustomerList,
        action.ErrorMessage,
        state.IsLoading,
        state.Customer,
        state.CustomerToBeCreatedPayload,
        state.CustomerToBeUpdatePayload,
        state.CustomerId
        );
    }
  }
}