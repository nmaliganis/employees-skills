using employee.skill.fe.Models.DTOs.Employees;

namespace employee.skill.fe.Store.Employees.Actions.FetchEmployee
{
  public class FetchEmployeeSuccessAction
  {
    public EmployeeDto EmployeeToHaveBeenFetched { get; private set; }

    public FetchEmployeeSuccessAction(EmployeeDto employeeToHaveBeenFetched)
    {
      EmployeeToHaveBeenFetched  = employeeToHaveBeenFetched;
    }
  }
}