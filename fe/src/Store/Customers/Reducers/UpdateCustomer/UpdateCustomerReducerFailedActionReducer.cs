using Fluxor;
using smarthotel.ui.Store.Customers.Actions.UpdateCustomer;

namespace smarthotel.ui.Store.Customers.Reducers.UpdateCustomer
{
  public class UpdateCustomerReducerFailedActionReducer : Reducer<CustomerState, UpdateCustomerFailedAction>
  {
    public override CustomerState Reduce(CustomerState state, UpdateCustomerFailedAction action)
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