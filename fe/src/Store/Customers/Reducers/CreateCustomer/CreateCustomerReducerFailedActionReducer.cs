using Fluxor;
using smarthotel.ui.Store.Customers.Actions.CreateCustomer;

namespace smarthotel.ui.Store.Customers.Reducers.CreateCustomer
{
  public class CreateCustomerReducerFailedActionReducer : Reducer<CustomerState, CreateCustomerFailedAction>
  {
    public override CustomerState Reduce(CustomerState state, CreateCustomerFailedAction action)
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