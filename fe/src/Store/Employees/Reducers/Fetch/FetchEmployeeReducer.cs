using employee.skill.fe.Store.Employees.Actions.FetchEmployee;
using Fluxor;

namespace employee.skill.fe.Store.Employees.Reducers.FetchEmployee
{
  public class FetchEmployeeReducer : Reducer<EmployeeState, FetchEmployeeAction>
  {
    public override EmployeeState Reduce(EmployeeState state, FetchEmployeeAction action)
    {
      return new EmployeeState(
        state.EmployeeList,
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