using System;
using System.Threading.Tasks;
using Fluxor;
using smarthotel.ui.ServiceAgents.Contracts.Services;
using smarthotel.ui.Store.Services.Actions.FetchServiceTypes;

namespace smarthotel.ui.Store.Services.Effects.FetchServiceTypes
{
  public class FetchServiceTypeListEffect : Effect<FetchServiceTypeListAction>
  {
    public IServiceDataService ServiceDataService { get; set; }
    public FetchServiceTypeListEffect(IServiceDataService serviceDataService)
    {
      ServiceDataService = serviceDataService;
    }

    public override async Task HandleAsync(FetchServiceTypeListAction action, IDispatcher dispatcher)
    {
      try
      {
        var services = await ServiceDataService.GetServiceTypeList();
        dispatcher.Dispatch(new FetchServiceTypeListSuccessAction(services));
      }
      catch (Exception e)
      {
        dispatcher.Dispatch(new FetchServiceTypeListFailedAction(e.Message));
      }      
    }
  }
}