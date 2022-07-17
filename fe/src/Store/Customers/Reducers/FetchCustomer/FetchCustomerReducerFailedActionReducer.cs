using Fluxor;
using smarthotel.ui.Store.Customers.Actions.FetchCustomer;

namespace smarthotel.ui.Store.Customers.Reducers.FetchCustomer
{
  public class FetchCustomerReducerFailedActionReducer : Reducer<CustomerState, FetchCustomerFailedAction>
  {
    public override CustomerState Reduce(CustomerState state, FetchCustomerFailedAction action)
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