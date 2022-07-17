using System;

namespace smarthotel.ui.Store.Services.Actions.UpdateService
{
  public class UpdateServiceSuccessAction
  {
    public Guid ServiceHaveBeenUpdateId { get; private set; }
    public string ServiceDeletionStatus { get; private set; }

    public UpdateServiceSuccessAction(Guid serviceHaveBeenUpdateId, string serviceDeletionStatus)
    {
      ServiceHaveBeenUpdateId = serviceHaveBeenUpdateId;
      ServiceDeletionStatus = serviceDeletionStatus;
    }
  }
}