using System;
using System.Collections.Generic;
using employee.skill.fe.Models.DTOs.Employees;
using employee.skill.fe.Store.Statuses;
using Fluxor;

namespace employee.skill.fe.Store.Employees
{
  public class EmployeeFeature : Feature<EmployeeState>
  {
    public override string GetName() => "Employee";

    protected override EmployeeState GetInitialState() => new EmployeeState(
      new List<EmployeeDto>(), 
      "",
      true,
      new EmployeeDto(), 
      new EmployeeForCreationDto(), 
      new EmployeeForModificationDto(), 
      Guid.Empty,
      CreationStatus.Init,
      ModificationStatus.Init,
      DeletionStatus.Init
    );
  }
}