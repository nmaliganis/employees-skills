using employee.skill.fe.Store.Employees.Actions.FetchEmployee;
using Fluxor;

namespace employee.skill.fe.Store.Employees.Reducers.FetchEmployee
{
  public class FetchEmployeeReducerSuccessActionReducer : Reducer<EmployeeState, FetchEmployeeSuccessAction>
  {
    public override EmployeeState Reduce(EmployeeState state, FetchEmployeeSuccessAction action)
    {
      return new EmployeeState(
        state.EmployeeList,
        "",
        state.IsLoading,
        action.EmployeeToHaveBeenFetched,
        state.EmployeeToBeCreatedPayload,
        state.EmployeeToBeUpdatePayload,
        state.EmployeeId
      );
    }
  }
}