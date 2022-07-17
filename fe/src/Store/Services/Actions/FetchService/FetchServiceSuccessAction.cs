using smarthotel.ui.Models.DTOs.Services;

namespace smarthotel.ui.Store.Services.Actions.FetchService
{
  public class FetchServiceSuccessAction
  {
    public ServiceDto ServiceToHaveBeenFetched { get; private set; }

    public FetchServiceSuccessAction(ServiceDto serviceToHaveBeenFetched)
    {
      ServiceToHaveBeenFetched  = serviceToHaveBeenFetched;
    }
  }
}