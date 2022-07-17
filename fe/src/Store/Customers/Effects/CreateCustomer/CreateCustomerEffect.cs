using System;
using System.Threading.Tasks;
using Fluxor;
using smarthotel.ui.ServiceAgents.Base;
using smarthotel.ui.ServiceAgents.Contracts.Customers;
using smarthotel.ui.Store.Customers.Actions.CreateCustomer;

namespace smarthotel.ui.Store.Customers.Effects.CreateCustomer
{
  public class CreateCustomerEffect : Effect<CreateCustomerAction>
  {
    public ICustomerDataService CustomerDataService { get; set; }
    public CreateCustomerEffect(ICustomerDataService customerDataService)
    {
      CustomerDataService = customerDataService;
    }

    public override async Task HandleAsync(CreateCustomerAction action, IDispatcher dispatcher)
    {
      try
      {
        var createdCustomer = await CustomerDataService.CreateCustomer(action.CustomerToBeCreatedPayload);
        dispatcher.Dispatch(new CreateCustomerSuccessAction(createdCustomer));
        //Todo: Logging
      }
      catch (ServiceHttpRequestException<string> e)
      {
        dispatcher.Dispatch(new CreateCustomerFailedAction(errorMessage: e.Message, e.Content));
      }     
      catch (Exception e)
      {
        dispatcher.Dispatch(new CreateCustomerFailedAction(errorMessage: e.Message, e.InnerException?.Message));
      }     
    }
  }
}