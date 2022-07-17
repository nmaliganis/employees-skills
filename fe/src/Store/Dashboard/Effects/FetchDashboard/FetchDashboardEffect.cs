using System;
using System.Threading.Tasks;
using Fluxor;
using smarthotel.ui.ServiceAgents.Contracts.Customers;
using smarthotel.ui.ServiceAgents.Contracts.Services;
using smarthotel.ui.ServiceAgents.Contracts.Spaces;
using smarthotel.ui.Store.Dashboard.Actions.FetchDashboards;

namespace smarthotel.ui.Store.Dashboard.Effects.FetchDashboard
{
  public class FetchDashboardEffect : Effect<FetchDashboardAction>
  {
    public ICustomerDataService CustomerDataService { get; set; }
    public ISpaceDataService SpaceDataService { get; }
    public IServiceDataService ServiceDataService { get; set; }
    public FetchDashboardEffect(ICustomerDataService customerDataService, ISpaceDataService spaceDataService, IServiceDataService serviceDataService)
    {
      CustomerDataService = customerDataService;
      SpaceDataService = spaceDataService;
      ServiceDataService = serviceDataService;
    }

    public override async Task HandleAsync(FetchDashboardAction action, IDispatcher dispatcher)
    {
      try
      {
        var customers = await CustomerDataService.GetTotalCustomerCount();
        var spaces = await SpaceDataService.GetTotalSpaceCount();
        var services = await ServiceDataService.GetTotalServiceCount();
        var totals = await ServiceDataService.GetTotalServiceTotals();

        dispatcher.Dispatch(new FetchDashboardSuccessAction(customers, spaces, services, totals));
      }
      catch (Exception e)
      {
        dispatcher.Dispatch(new FetchDashboardFailedAction(e.Message));
      }      
    }
  }
}