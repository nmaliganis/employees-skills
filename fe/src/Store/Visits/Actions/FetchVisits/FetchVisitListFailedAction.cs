namespace smarthotel.ui.Store.Visits.Actions.FetchVisits
{
  public class FetchVisitListFailedAction
  {
    public string ErrorMessage { get; private set; }
    public FetchVisitListFailedAction(string errorMessage)
    {
      ErrorMessage = errorMessage;
    }
  }
}