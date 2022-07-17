using System;
using System.Threading.Tasks;
using Fluxor;
using smarthotel.ui.ServiceAgents.Contracts.Services;
using smarthotel.ui.Store.Services.Actions.FetchFameServices;
using smarthotel.ui.Store.Services.Actions.FetchServices;

namespace smarthotel.ui.Store.Services.Effects.FetchFameServices
{
  public class FetchFameServiceListEffect : Effect<FetchFameServiceListAction>
  {
    public IServiceDataService ServiceDataService { get; set; }
    public FetchFameServiceListEffect(IServiceDataService serviceDataService)
    {
      ServiceDataService = serviceDataService;
    }

    public override async Task HandleAsync(FetchFameServiceListAction action, IDispatcher dispatcher)
    {
      try
      {
        var fameServicesYear = await ServiceDataService.GetServiceFameList(true);
        var fameServicesMonth = await ServiceDataService.GetServiceFameList(false);
        dispatcher.Dispatch(new FetchFameServiceListSuccessAction(fameServicesYear, fameServicesMonth));
      }
      catch (Exception e)
      {
        dispatcher.Dispatch(new FetchFameServiceListFailedAction(e.Message));
      }      
    }
  }
}