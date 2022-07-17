namespace smarthotel.ui.Store.Services.Actions.FetchFreqServices
{
  public class FetchFreqServiceListFailedAction
  {
    public string ErrorMessage { get; private set; }
    public FetchFreqServiceListFailedAction(string errorMessage)
    {
      ErrorMessage = errorMessage;
    }
  }
}