using Fluxor;
using smarthotel.ui.Store.Customers.Actions.FetchCustomers;

namespace smarthotel.ui.Store.Customers.Reducers.FetchCustomers
{
  public class FetchCustomerListReducerSuccessActionReducer : Reducer<CustomerState, FetchCustomerListSuccessAction>
  {
    public override CustomerState Reduce(CustomerState state, FetchCustomerListSuccessAction action)
    {
      return new CustomerState(
        action.CustomerList,
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