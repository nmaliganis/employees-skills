namespace employee.skill.fe.Store.Employees.Actions.FetchEmployee
{
  public class FetchEmployeeFailedAction
  {
    public string ErrorMessage { get; private set; }
    public FetchEmployeeFailedAction(string errorMessage)
    {
      ErrorMessage = errorMessage;
    }
  }
}