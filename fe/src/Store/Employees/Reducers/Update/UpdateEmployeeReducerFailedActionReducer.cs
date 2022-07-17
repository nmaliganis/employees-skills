using employee.skill.fe.Store.Employees.Actions.UpdateEmployee;
using Fluxor;

namespace employee.skill.fe.Store.Employees.Reducers.UpdateEmployee
{
  public class UpdateEmployeeReducerFailedActionReducer : Reducer<EmployeeState, UpdateEmployeeFailedAction>
  {
    public override EmployeeState Reduce(EmployeeState state, UpdateEmployeeFailedAction action)
    {
      return new EmployeeState(
        state.EmployeeList,
        action.ErrorMessage,
        state.IsLoading,
        state.Employee,
        state.EmployeeToBeCreatedPayload,
        state.EmployeeToBeUpdatePayload,
        state.EmployeeId
        );
    }
  }
}