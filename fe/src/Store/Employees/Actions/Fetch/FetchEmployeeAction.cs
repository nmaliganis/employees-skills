using System;

namespace employee.skill.fe.Store.Employees.Actions.FetchEmployee
{
  public class FetchEmployeeAction
  {
    public Guid EmployeeToBeFetchedId { get; private set; }

    public FetchEmployeeAction(Guid employeeToBeFetchedId)
    {
      EmployeeToBeFetchedId = employeeToBeFetchedId;
    }
  }
}