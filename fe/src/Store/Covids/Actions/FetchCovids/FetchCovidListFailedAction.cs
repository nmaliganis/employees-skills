namespace smarthotel.ui.Store.Covids.Actions.FetchCovids
{
  public class FetchCovidListFailedAction
  {
    public string ErrorMessage { get; private set; }
    public FetchCovidListFailedAction(string errorMessage)
    {
      ErrorMessage = errorMessage;
    }
  }
}