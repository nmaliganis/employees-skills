namespace smarthotel.ui.Store.Spaces.Actions.FetchCustomer
{
  public class FetchSpaceFailedAction
  {
    public string ErrorMessage { get; private set; }
    public FetchSpaceFailedAction(string errorMessage)
    {
      ErrorMessage = errorMessage;
    }
  }
}