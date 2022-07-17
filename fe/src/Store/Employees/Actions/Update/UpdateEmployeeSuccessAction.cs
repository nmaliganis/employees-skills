using employee.skill.fe.Models.DTOs.Employees;

namespace employee.skill.fe.Store.Employees.Actions.UpdateEmployee
{
  public class UpdateEmployeeSuccessAction
  {
    public EmployeeDto EmployeeHaveBeenUpdated { get; private set; }
    public string EmployeeUpdateStatus { get; private set; }

    public UpdateEmployeeSuccessAction(EmployeeDto employeeHaveBeenUpdated, string employeeUpdateStatus)
    {
      EmployeeHaveBeenUpdated = employeeHaveBeenUpdated;
      EmployeeUpdateStatus = employeeUpdateStatus;
    }
  }
}