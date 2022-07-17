using System;

namespace employee.skill.fe.Store.Employees.Actions.Delete
{
  public class DeleteEmployeeAction
  {
    public Guid EmployeeToBeDeletedId { get; private set; }

    public DeleteEmployeeAction(Guid EmployeeToBeDeletedId)
    {
      EmployeeToBeDeletedId = EmployeeToBeDeletedId;
    }
  }
}