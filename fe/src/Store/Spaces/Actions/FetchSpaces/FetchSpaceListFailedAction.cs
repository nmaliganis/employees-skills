namespace smarthotel.ui.Store.Spaces.Actions.FetchSpaces
{
  public class FetchSpaceListFailedAction
  {
    public string ErrorMessage { get; private set; }
    public FetchSpaceListFailedAction(string errorMessage)
    {
      ErrorMessage = errorMessage;
    }
  }
}