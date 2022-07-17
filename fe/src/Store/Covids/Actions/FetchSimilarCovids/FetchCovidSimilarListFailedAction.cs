namespace smarthotel.ui.Store.Covids.Actions.FetchSimilarCovids
{
  public class FetchCovidSimilarListFailedAction
  {
    public string ErrorMessage { get; private set; }
    public FetchCovidSimilarListFailedAction(string errorMessage)
    {
      ErrorMessage = errorMessage;
    }
  }
}