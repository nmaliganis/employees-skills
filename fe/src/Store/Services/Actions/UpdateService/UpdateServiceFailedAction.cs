namespace smarthotel.ui.Store.Services.Actions.UpdateService
{
  public class UpdateServiceFailedAction
  {
    public string ErrorMessage { get; private set; }
    public UpdateServiceFailedAction(string errorMessage)
    {
      ErrorMessage = errorMessage;
    }
  }
}