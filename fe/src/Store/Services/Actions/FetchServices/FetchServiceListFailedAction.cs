namespace smarthotel.ui.Store.Services.Actions.FetchServices
{
  public class FetchServiceListFailedAction
  {
    public string ErrorMessage { get; private set; }
    public FetchServiceListFailedAction(string errorMessage)
    {
      ErrorMessage = errorMessage;
    }
  }
}