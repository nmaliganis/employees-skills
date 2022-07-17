using System;

namespace smarthotel.ui.Store.Services.Actions.DeleteService
{
  public class DeleteServiceAction
  {
    public Guid ServiceToBeDeletedId { get; private set; }

    public DeleteServiceAction(Guid serviceToBeDeletedId)
    {
      ServiceToBeDeletedId = serviceToBeDeletedId;
    }
  }
}