using System;
using System.Threading.Tasks;
using Fluxor;
using smarthotel.ui.ServiceAgents.Contracts.Services;
using smarthotel.ui.Store.Services.Actions.FetchServices;

namespace smarthotel.ui.Store.Services.Effects.FetchServices
{
  public class FetchServiceListEffect : Effect<FetchServiceListAction>
  {
    public IServiceDataService ServiceDataService { get; set; }
    public FetchServiceListEffect(IServiceDataService serviceDataService)
    {
      ServiceDataService = serviceDataService;
    }

    public override async Task HandleAsync(FetchServiceListAction action, IDispatcher dispatcher)
    {
      try
      {
        var services = await ServiceDataService.GetServiceList();
        dispatcher.Dispatch(new FetchServiceListSuccessAction(services));
      }
      catch (Exception e)
      {
        dispatcher.Dispatch(new FetchServiceListFailedAction(e.Message));
      }      
    }
  }
}