using Fluxor;
using smarthotel.ui.Store.Customers.Actions.FetchCustomer;

namespace smarthotel.ui.Store.Customers.Reducers.FetchCustomer
{
  public class FetchCustomerReducerSuccessActionReducer : Reducer<CustomerState, FetchCustomerSuccessAction>
  {
    public override CustomerState Reduce(CustomerState state, FetchCustomerSuccessAction action)
    {
      return new CustomerState(
        state.CustomerList,
        "",
        state.IsLoading,
        action.CustomerToHaveBeenFetched,
        state.CustomerToBeCreatedPayload,
        state.CustomerToBeUpdatePayload,
        state.CustomerId
      );
    }
  }
}