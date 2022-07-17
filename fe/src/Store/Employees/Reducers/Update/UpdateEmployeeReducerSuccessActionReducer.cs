using employee.skill.fe.Store.Employees.Actions.UpdateEmployee;
using Fluxor;

namespace employee.skill.fe.Store.Employees.Reducers.UpdateEmployee
{
  public class UpdateEmployeeReducerSuccessActionReducer : Reducer<EmployeeState, UpdateEmployeeSuccessAction>
  {
    public override EmployeeState Reduce(EmployeeState state, UpdateEmployeeSuccessAction action)
    {
      return new EmployeeState(
        state.EmployeeList,
        action.EmployeeUpdateStatus,
        state.IsLoading,
        action.EmployeeHaveBeenUpdated,
        state.EmployeeToBeCreatedPayload,
        state.EmployeeToBeUpdatePayload,
        state.EmployeeId
      );
    }
  }
}