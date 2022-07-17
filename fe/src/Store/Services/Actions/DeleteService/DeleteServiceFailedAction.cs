namespace smarthotel.ui.Store.Services.Actions.DeleteService
{
  public class DeleteServiceFailedAction
  {
    public string ErrorMessage { get; private set; }
    public DeleteServiceFailedAction(string errorMessage)
    {
      ErrorMessage = errorMessage;
    }
  }
}