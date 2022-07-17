using employee.skill.fe.Models.DTOs.Employees;

namespace employee.skill.fe.Store.Employees.Actions.CreateEmployee
{
  public class CreateEmployeeSuccessAction
  {
    public EmployeeDto EmployeeHaveBeenCreated { get; private set; }

    public CreateEmployeeSuccessAction(EmployeeDto employeeHaveBeenCreated)
    {
      EmployeeHaveBeenCreated = employeeHaveBeenCreated;
    }
  }
}