using System;
using System.Threading.Tasks;
using Fluxor;
using smarthotel.ui.ServiceAgents.Contracts.Services;
using smarthotel.ui.Store.Services.Actions.FetchFreqServices;

namespace smarthotel.ui.Store.Services.Effects.FetchFreqServices
{
  public class FetchFreqServiceListEffect : Effect<FetchFreqServiceListAction>
  {
    public IServiceDataService ServiceDataService { get; set; }
    public FetchFreqServiceListEffect(IServiceDataService serviceDataService)
    {
      ServiceDataService = serviceDataService;
    }

    public override async Task HandleAsync(FetchFreqServiceListAction action, IDispatcher dispatcher)
    {
      try
      {
        var freqServicesYear = await ServiceDataService.GetServiceFreqList(true);
        var freqServicesMonth = await ServiceDataService.GetServiceFreqList(false);
        dispatcher.Dispatch(new FetchFreqServiceListSuccessAction(freqServicesYear, freqServicesMonth));
      }
      catch (Exception e)
      {
        dispatcher.Dispatch(new FetchFreqServiceListFailedAction(e.Message));
      }      
    }
  }
}