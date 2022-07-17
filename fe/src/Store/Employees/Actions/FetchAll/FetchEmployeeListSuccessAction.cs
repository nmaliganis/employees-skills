using System.Collections.Generic;
using employee.skill.fe.Models.DTOs.Employees;

namespace employee.skill.fe.Store.Employees.Actions.FetchEmployees
{
  public class FetchEmployeeListSuccessAction
  {
    public List<EmployeeDto> EmployeeList { get; private set; }

    public FetchEmployeeListSuccessAction(List<EmployeeDto> EmployeeList)
    {
      EmployeeList  = EmployeeList;
    }
  }
}