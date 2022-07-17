using employee.skill.fe.Store.Employees.Actions.FetchEmployees;
using Fluxor;

namespace employee.skill.fe.Store.Employees.Reducers.FetchEmployees
{
  public class FetchEmployeeListReducerSuccessActionReducer : Reducer<EmployeeState, FetchEmployeeListSuccessAction>
  {
    public override EmployeeState Reduce(EmployeeState state, FetchEmployeeListSuccessAction action)
    {
      return new EmployeeState(
        action.EmployeeList,
        "",
        state.IsLoading,
        state.Employee,
        state.EmployeeToBeCreatedPayload,
        state.EmployeeToBeUpdatePayload,
        state.EmployeeId
      );
    }
  }
}