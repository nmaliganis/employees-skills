namespace smarthotel.ui.Store.Services.Actions.FetchServiceTypes
{
  public class FetchServiceTypeListFailedAction
  {
    public string ErrorMessage { get; private set; }
    public FetchServiceTypeListFailedAction(string errorMessage)
    {
      ErrorMessage = errorMessage;
    }
  }
}