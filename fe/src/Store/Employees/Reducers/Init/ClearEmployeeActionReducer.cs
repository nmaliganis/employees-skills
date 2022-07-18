using employee.skill.fe.Models.DTOs.Employees;
using employee.skill.fe.Store.Employees.Actions.InitEmployee;
using Fluxor;

namespace employee.skill.fe.Store.Employees.Reducers.InitEmployee
{
  public class ClearEmployeeActionReducer : Reducer<EmployeeState, ClearEmployeeAction>
  {
    public override EmployeeState Reduce(EmployeeState state, ClearEmployeeAction action)
    {
      return new EmployeeState(
        state.EmployeeList,
        "",
        state.IsLoading,
        new EmployeeDto(), 
        state.EmployeeToBeCreatedPayload,
        state.EmployeeToBeUpdatePayload,
        state.EmployeeId,
        state.CreationStatus,
        state.ModificationStatus,
        state.DeletionStatus
      );
    }
  }
}