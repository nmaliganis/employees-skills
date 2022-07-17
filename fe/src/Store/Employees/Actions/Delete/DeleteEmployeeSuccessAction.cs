using System;

namespace employee.skill.fe.Store.Employees.Actions.Delete
{
  public class DeleteEmployeeSuccessAction
  {
    public Guid EmployeeHaveBeenDeletedId { get; private set; }
    public string EmployeeDeletionStatus { get; private set; }

    public DeleteEmployeeSuccessAction(Guid employeeHaveBeenDeletedId, string employeeDeletionStatus)
    {
      EmployeeHaveBeenDeletedId = employeeHaveBeenDeletedId;
      EmployeeDeletionStatus = employeeDeletionStatus;
    }
  }
}