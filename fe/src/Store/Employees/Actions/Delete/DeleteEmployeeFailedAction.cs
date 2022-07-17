namespace employee.skill.fe.Store.Employees.Actions.DeleteEmployee
{
  public class DeleteEmployeeFailedAction
  {
    public string ErrorMessage { get; private set; }
    public DeleteEmployeeFailedAction(string errorMessage)
    {
      ErrorMessage = errorMessage;
    }
  }
}