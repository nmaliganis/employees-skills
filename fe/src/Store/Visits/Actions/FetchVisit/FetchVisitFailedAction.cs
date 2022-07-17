namespace smarthotel.ui.Store.Visits.Actions.FetchVisit
{
  public class FetchVisitFailedAction
  {
    public string ErrorMessage { get; private set; }
    public FetchVisitFailedAction(string errorMessage)
    {
      ErrorMessage = errorMessage;
    }
  }
}