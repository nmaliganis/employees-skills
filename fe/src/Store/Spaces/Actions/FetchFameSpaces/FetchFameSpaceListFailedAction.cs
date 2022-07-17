namespace smarthotel.ui.Store.Spaces.Actions.FetchFameSpaces
{
  public class FetchFameSpaceListFailedAction
  {
    public string ErrorMessage { get; private set; }
    public FetchFameSpaceListFailedAction(string errorMessage)
    {
      ErrorMessage = errorMessage;
    }
  }
}