using Fluxor;
using smarthotel.ui.Store.Customers.Actions.FetchCustomers;

namespace smarthotel.ui.Store.Customers.Reducers.FetchCustomers
{
  public class FetchCustomerListReducer : Reducer<CustomerState, FetchCustomerListAction>
  {
    public override CustomerState Reduce(CustomerState state, FetchCustomerListAction action)
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