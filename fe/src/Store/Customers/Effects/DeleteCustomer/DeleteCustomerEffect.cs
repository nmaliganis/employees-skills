using System;
using System.Threading.Tasks;
using Fluxor;
using smarthotel.ui.ServiceAgents.Contracts.Customers;
using smarthotel.ui.Store.Customers.Actions.DeleteCustomer;

namespace smarthotel.ui.Store.Customers.Effects.DeleteCustomer
{
  public class DeleteCustomerEffect : Effect<DeleteCustomerAction>
  {
    public ICustomerDataService CustomerDataService { get; set; }
    public DeleteCustomerEffect(ICustomerDataService customerDataService)
    {
      CustomerDataService = customerDataService;
    }

    public override async Task HandleAsync(DeleteCustomerAction action, IDispatcher dispatcher)
    {
      try
      {
        var deletedCustomer = await CustomerDataService.DeleteCustomer(action.CustomerToBeDeletedId);
        dispatcher.Dispatch(new DeleteCustomerSuccessAction(deletedCustomer.Id, deletedCustomer.Message));
      }
      catch (Exception e)
      {
        dispatcher.Dispatch(new DeleteCustomerFailedAction(e.Message));
      }  
    }
  }
}