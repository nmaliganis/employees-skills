using System;

namespace smarthotel.ui.Store.Services.Actions.DeleteService
{
  public class DeleteServiceSuccessAction
  {
    public Guid ServiceHaveBeenDeletedId { get; private set; }
    public string ServiceDeletionStatus { get; private set; }

    public DeleteServiceSuccessAction(Guid serviceHaveBeenDeletedId, string serviceDeletionStatus)
    {
      ServiceHaveBeenDeletedId = serviceHaveBeenDeletedId;
      ServiceDeletionStatus = serviceDeletionStatus;
    }
  }
}