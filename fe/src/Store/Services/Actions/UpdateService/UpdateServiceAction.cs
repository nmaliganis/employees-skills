using System;

namespace smarthotel.ui.Store.Services.Actions.UpdateService
{
  public class UpdateServiceAction
  {
    public Guid ServiceToBeUpdateId { get; private set; }

    public UpdateServiceAction(Guid serviceToBeUpdateId)
    {
      ServiceToBeUpdateId = serviceToBeUpdateId;
    }
  }
}