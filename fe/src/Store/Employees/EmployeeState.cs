using System;
using System.Collections.Generic;
using employee.skill.fe.Models.DTOs.Employees;

namespace employee.skill.fe.Store.Employees
{
  public class EmployeeState
  {
    public bool IsLoading { get; private set; }
    public string ErrorMessage { get; private set; }
    public List<EmployeeDto> EmployeeList { get; private set; }
    public EmployeeDto Employee { get; private set; }
    public EmployeeForCreationDto EmployeeToBeCreatedPayload { get; private set; }
    public EmployeeForModificationDto EmployeeToBeUpdatePayload { get; }
    public Guid EmployeeId { get; }

    public EmployeeState(
      List<EmployeeDto> employeeList, 
      string errorMessage, 
      bool isLoading,
      EmployeeDto employee, 
      EmployeeForCreationDto employeeToBeCreatedPayload, 
      EmployeeForModificationDto employeeToBeUpdatePayload, 
      Guid employeeId
    )
    {
      EmployeeList  = employeeList;
      ErrorMessage = errorMessage;
      IsLoading = isLoading;
      Employee = employee;
      EmployeeToBeCreatedPayload = employeeToBeCreatedPayload;
      EmployeeToBeUpdatePayload = employeeToBeUpdatePayload;
      EmployeeId = employeeId;
    }
  }
}