using employee.skill.fe.Models.DTOs.Employees;

namespace employee.skill.fe.Store.Employees.Actions.Create
{
  public class CreateEmployeeAction
  {
    public CreateEmployeeAction(EmployeeForCreationDto employeeToBeCreatedPayload)
    {
      EmployeeToBeCreatedPayload = employeeToBeCreatedPayload;
    }

    public EmployeeForCreationDto EmployeeToBeCreatedPayload { get; private set; }
  }
}