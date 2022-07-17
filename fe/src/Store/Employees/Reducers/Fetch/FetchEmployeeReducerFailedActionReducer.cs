using employee.skill.fe.Store.Employees.Actions.FetchEmployee;
using Fluxor;

namespace employee.skill.fe.Store.Employees.Reducers.FetchEmployee
{
  public class FetchEmployeeReducerFailedActionReducer : Reducer<EmployeeState, FetchEmployeeFailedAction>
  {
    public override EmployeeState Reduce(EmployeeState state, FetchEmployeeFailedAction action)
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