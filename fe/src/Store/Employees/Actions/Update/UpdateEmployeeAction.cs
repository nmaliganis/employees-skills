using System;
using employee.skill.fe.Models.DTOs.Employees;

namespace employee.skill.fe.Store.Employees.Actions.UpdateEmployee
{
  public class UpdateEmployeeAction
  {
    public Guid EmployeeToBeUpdateId { get; private set; }
    public EmployeeForModificationDto EmployeeForModificationDto { get; private set; }

    public UpdateEmployeeAction(Guid employeeToBeUpdateId, EmployeeForModificationDto employeeForModificationDto)
    {
      EmployeeToBeUpdateId = employeeToBeUpdateId;
      EmployeeForModificationDto = employeeForModificationDto;
    }
  }
}