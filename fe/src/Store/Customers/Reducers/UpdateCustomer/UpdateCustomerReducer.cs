using Fluxor;
using smarthotel.ui.Store.Customers.Actions.UpdateCustomer;

namespace smarthotel.ui.Store.Customers.Reducers.UpdateCustomer
{
  public class UpdateCustomerReducer : Reducer<CustomerState, UpdateCustomerAction>
  {
    public override CustomerState Reduce(CustomerState state, UpdateCustomerAction action)
    {
      return new CustomerState(
        state.CustomerList,
        "",
        state.IsLoading,
        state.Customer,
        state.CustomerToBeCreatedPayload,
        action.CustomerForModificationDto,
        action.CustomerToBeUpdateId
        );
    }
  }
}