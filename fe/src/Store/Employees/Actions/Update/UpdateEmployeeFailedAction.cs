namespace employee.skill.fe.Store.Employees.Actions.UpdateEmployee
{
  public class UpdateEmployeeFailedAction
  {
    public string ErrorMessage { get; private set; }
    public string ErrorContent { get; private set; }
    public UpdateEmployeeFailedAction(string errorMessage, string errorContent)
    {
      ErrorMessage = errorMessage;
    }
  }
}