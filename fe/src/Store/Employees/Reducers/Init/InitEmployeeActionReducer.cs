using employee.skill.fe.Models.DTOs.Employees;
using employee.skill.fe.Store.Employees.Actions.InitEmployee;
using employee.skill.fe.Store.Statuses;
using Fluxor;

namespace employee.skill.fe.Store.Employees.Reducers.InitEmployee
{
  public class InitEmployeeActionReducer : Reducer<EmployeeState, InitEmployeeAction>
  {
    public override EmployeeState Reduce(EmployeeState state, InitEmployeeAction action)
    {
      return new EmployeeState(
        state.EmployeeList,
        "",
        state.IsLoading,
        new EmployeeDto(), 
        state.EmployeeToBeCreatedPayload,
        state.EmployeeToBeUpdatePayload,
        state.EmployeeId,
        CreationStatus.Init,
        ModificationStatus.Init,
        DeletionStatus.Init
      );
    }
  }
}