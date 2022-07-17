using smarthotel.ui.Models.DTOs.Services;

namespace smarthotel.ui.Store.Services.Actions.CreateService
{
  public class CreateServiceSuccessAction
  {
    public ServiceDto ServiceHaveBeenCreated { get; private set; }

    public CreateServiceSuccessAction(ServiceDto serviceHaveBeenCreated)
    {
      ServiceHaveBeenCreated = serviceHaveBeenCreated;
    }
  }
}