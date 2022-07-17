using Fluxor;
using smarthotel.ui.Store.Customers.Actions.CreateCustomer;

namespace smarthotel.ui.Store.Customers.Reducers.CreateCustomer
{
  public class CreateCustomerReducerSuccessActionReducer : Reducer<CustomerState, CreateCustomerSuccessAction>
  {
    public override CustomerState Reduce(CustomerState state, CreateCustomerSuccessAction action)
    {
      return new CustomerState(
        state.CustomerList,
        action.CustomerHaveBeenCreated.Message,
        state.IsLoading,
        action.CustomerHaveBeenCreated,
        state.CustomerToBeCreatedPayload,
        state.CustomerToBeUpdatePayload,
        state.CustomerId
      );
    }
  }
}