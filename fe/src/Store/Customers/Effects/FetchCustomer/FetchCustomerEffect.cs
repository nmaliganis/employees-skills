using System;
using System.Threading.Tasks;
using Fluxor;
using smarthotel.ui.ServiceAgents.Contracts.Customers;
using smarthotel.ui.Store.Customers.Actions.FetchCustomer;

namespace smarthotel.ui.Store.Customers.Effects.FetchCustomer
{
  public class FetchCustomerEffect : Effect<FetchCustomerAction>
  {
    public ICustomerDataService CustomerDataService { get; set; }
    public FetchCustomerEffect(ICustomerDataService customerDataService)
    {
      CustomerDataService = customerDataService;
    }

    public override async Task HandleAsync(FetchCustomerAction action, IDispatcher dispatcher)
    {
      try
      {
        var customer = await CustomerDataService.GetCustomer(action.CustomerToBeFetchedId);
        dispatcher.Dispatch(new FetchCustomerSuccessAction(customer));
      }
      catch (Exception e)
      {
        dispatcher.Dispatch(new FetchCustomerFailedAction(e.Message));
      }     
    }
  }
}