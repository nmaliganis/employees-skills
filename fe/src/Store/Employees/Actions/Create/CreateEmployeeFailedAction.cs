namespace employee.skill.fe.Store.Employees.Actions.CreateEmployee
{
  public class CreateEmployeeFailedAction
  {
    public string ErrorMessage { get; private set; }
    public string ErrorContent { get; private set; }
    public CreateEmployeeFailedAction(string errorMessage, string errorContent)
    {
      ErrorMessage = errorMessage;
      ErrorContent = errorContent;
    }
  }
}