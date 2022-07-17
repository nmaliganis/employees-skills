namespace smarthotel.ui.Store.Services.Actions.FetchFameServices
{
  public class FetchFameServiceListFailedAction
  {
    public string ErrorMessage { get; private set; }
    public FetchFameServiceListFailedAction(string errorMessage)
    {
      ErrorMessage = errorMessage;
    }
  }
}