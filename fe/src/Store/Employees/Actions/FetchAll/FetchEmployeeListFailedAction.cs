namespace employee.skill.fe.Store.Employees.Actions.FetchEmployees
{
  public class FetchEmployeeListFailedAction
  {
    public string ErrorMessage { get; private set; }
    public FetchEmployeeListFailedAction(string errorMessage)
    {
      ErrorMessage = errorMessage;
    }
  }
}