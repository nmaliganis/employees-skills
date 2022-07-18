using employee.skill.fe.Store.Employees.Actions.FetchEmployees;
using Fluxor;

namespace employee.skill.fe.Store.Employees.Reducers.FetchEmployees
{
  public class FetchEmployeeListReducerFailedActionReducer : Reducer<EmployeeState, FetchEmployeeListFailedAction>
  {
    public override EmployeeState Reduce(EmployeeState state, FetchEmployeeListFailedAction action)
    {
      return new EmployeeState(
        state.EmployeeList,
        action.ErrorMessage,
        state.IsLoading,
        state.Employee,
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