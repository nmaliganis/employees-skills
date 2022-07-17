using Fluxor;
using smarthotel.ui.Store.Customers.Actions.UpdateCustomer;

namespace smarthotel.ui.Store.Customers.Reducers.UpdateCustomer
{
  public class UpdateCustomerReducerSuccessActionReducer : Reducer<CustomerState, UpdateCustomerSuccessAction>
  {
    public override CustomerState Reduce(CustomerState state, UpdateCustomerSuccessAction action)
    {
      return new CustomerState(
        state.CustomerList,
        action.CustomerUpdateStatus,
        state.IsLoading,
        action.CustomerHaveBeenUpdated,
        state.CustomerToBeCreatedPayload,
        state.CustomerToBeUpdatePayload,
        state.CustomerId
      );
    }
  }
}