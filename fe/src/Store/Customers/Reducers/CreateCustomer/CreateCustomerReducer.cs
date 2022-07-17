using Fluxor;
using smarthotel.ui.Store.Customers.Actions.CreateCustomer;

namespace smarthotel.ui.Store.Customers.Reducers.CreateCustomer
{
  public class CreateCustomerReducer : Reducer<CustomerState, CreateCustomerAction>
  {
    public override CustomerState Reduce(CustomerState state, CreateCustomerAction action)
    {
      return new CustomerState(
        state.CustomerList,
        "",
        state.IsLoading,
        state.Customer,
        action.CustomerToBeCreatedPayload,
        state.CustomerToBeUpdatePayload,
        state.CustomerId
        );
    }
  }
}