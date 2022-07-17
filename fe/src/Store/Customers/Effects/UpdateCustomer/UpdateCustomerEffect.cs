using System;
using System.Threading.Tasks;
using Fluxor;
using smarthotel.ui.ServiceAgents.Base;
using smarthotel.ui.ServiceAgents.Contracts.Customers;
using smarthotel.ui.Store.Customers.Actions.UpdateCustomer;

namespace smarthotel.ui.Store.Customers.Effects.UpdateCustomer
{
  public class UpdateCustomerEffect : Effect<UpdateCustomerAction>
  {
    public ICustomerDataService CustomerDataService { get; set; }
    public UpdateCustomerEffect(ICustomerDataService customerDataService)
    {
      CustomerDataService = customerDataService;
    }

    public override async Task HandleAsync(UpdateCustomerAction action, IDispatcher dispatcher)
    {
      try
      {
        var updatedCustomer = await CustomerDataService.UpdateCustomer(action.CustomerToBeUpdateId, action.CustomerForModificationDto);
        dispatcher.Dispatch(new UpdateCustomerSuccessAction(updatedCustomer, updatedCustomer.Message));
        //Todo: Logging
      }
      catch (ServiceHttpRequestException<string> e)
      {
        dispatcher.Dispatch(new UpdateCustomerFailedAction(errorMessage: e.Message, e.Content));
      }     
      catch (Exception e)
      {
        dispatcher.Dispatch(new UpdateCustomerFailedAction(errorMessage: e.Message, e.InnerException?.Message));
      }     
    }
  }
}