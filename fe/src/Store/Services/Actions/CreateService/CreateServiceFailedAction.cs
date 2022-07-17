namespace smarthotel.ui.Store.Services.Actions.CreateService
{
  public class CreateServiceFailedAction
  {
    public string ErrorMessage { get; private set; }
    public string ErrorContent { get; private set; }
    public CreateServiceFailedAction(string errorMessage, string errorContent)
    {
      ErrorMessage = errorMessage;
      ErrorContent = errorContent;
    }
  }
}