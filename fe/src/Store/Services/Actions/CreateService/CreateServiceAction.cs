using smarthotel.ui.Models.DTOs.Services;

namespace smarthotel.ui.Store.Services.Actions.CreateService
{
  public class CreateServiceAction
  {
    public CreateServiceAction(ServiceForCreationDto serviceToBeCreatedPayload)
    {
      ServiceToBeCreatedPayload = serviceToBeCreatedPayload;
    }

    public ServiceForCreationDto ServiceToBeCreatedPayload { get; private set; }
  }
}