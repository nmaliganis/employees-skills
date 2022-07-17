using System;
using System.Threading.Tasks;
using Fluxor;
using smarthotel.ui.ServiceAgents.Contracts.Customers;
using smarthotel.ui.Store.Customers.Actions.FetchCustomers;

namespace smarthotel.ui.Store.Customers.Effects.FetchCustomers
{
  public class FetchCustomerListEffect : Effect<FetchCustomerListAction>
  {
    public ICustomerDataService CustomerDataService { get; set; }
    public FetchCustomerListEffect(ICustomerDataService customerDataService)
    {
      CustomerDataService = customerDataService;
    }

    public override async Task HandleAsync(FetchCustomerListAction action, IDispatcher dispatcher)
    {
      try
      {
        var customers = await CustomerDataService.GetCustomerList();
        dispatcher.Dispatch(new FetchCustomerListSuccessAction(customers));
      }
      catch (Exception e)
      {
        dispatcher.Dispatch(new FetchCustomerListFailedAction(e.Message));
      }      
    }
  }
}