using Fluxor;
using smarthotel.ui.Store.Customers.Actions.FetchCustomer;

namespace smarthotel.ui.Store.Customers.Reducers.FetchCustomer
{
  public class FetchCustomerReducer : Reducer<CustomerState, FetchCustomerAction>
  {
    public override CustomerState Reduce(CustomerState state, FetchCustomerAction action)
    {
      return new CustomerState(
        state.CustomerList,
        "",
        state.IsLoading,
        state.Customer,
        state.CustomerToBeCreatedPayload,
        state.CustomerToBeUpdatePayload,
        state.CustomerId
        );
    }
  }
}