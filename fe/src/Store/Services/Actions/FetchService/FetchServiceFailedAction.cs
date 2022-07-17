namespace smarthotel.ui.Store.Services.Actions.FetchService
{
  public class FetchServiceFailedAction
  {
    public string ErrorMessage { get; private set; }
    public FetchServiceFailedAction(string errorMessage)
    {
      ErrorMessage = errorMessage;
    }
  }
}